using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum SpecificBodyPart
    {
        LeftLeg, RightLeg, LeftArm, RightArm, Body, Head
    }
    public Stats entityBaseStats;
    public Stats entityFightingStats;
    [Serializable]
    public class PositionedBodyPart
    {
        public SpecificBodyPart bodyPosition;
        public BodyPartSO bodyPartSO;
    }
    [Serializable]
    public class BodySprite
    {
        public SpecificBodyPart bodyPosition;
        public SpriteRenderer spriteRenderer;
    }
    [SerializeField] public List<PositionedBodyPart> bodyParts = new();
    [SerializeField] public List<BodySprite> bodyPartSprites = new();
    [SerializeField] private GameObject animationPoint;
    [SerializeField] private GameObject animationBackPoint;
    [SerializeField] private GameObject animationFrontPoint;
    [SerializeField] private GameObject blockNumberAnimationPosition;
    [SerializeField] private GameObject blockAnimationPosition;
    [SerializeField] private GameObject attackNumberAnimationPosition;
    [SerializeField] private Animator attackAnimator;
    [SerializeField] bool isPlayer = false;

    public Stats EntityFightingStats { get => entityFightingStats; set => entityFightingStats = value; }

    // Start is called before the first frame update

    public int GetAmountOfItemsOfType(BodyPartSO.Type type)
    {
        int amount = 0;
        foreach (var item in bodyParts)
        {
            if (item.bodyPartSO.type == type)
            {
                amount++;
            }
        }
        return amount;
    }

    public void SwitchBodyPart(SpecificBodyPart bodyPosition, BodyPartSO newBodyPart)
    {
        bodyParts.Where((x) => x.bodyPosition == bodyPosition).First().bodyPartSO = newBodyPart;
        UpdateBodyGraphics();
    }

    private void UpdateBodyGraphics()
    {
        foreach (var bodyPartSprite in bodyPartSprites)
        {
            BodyPartSO temp = bodyParts.First((x) => x.bodyPosition == bodyPartSprite.bodyPosition).bodyPartSO;
            bodyPartSprite.spriteRenderer.sprite = temp.sprite;
        }
    }

    public bool GetAttackDamage(out int damage)
    {
        float critChance = entityFightingStats.Crit * BodyPartManager.Instance.CritChanceBalancingMultiplier;
        float attack = entityFightingStats.Attack;
        bool crit = false;
        if (UnityEngine.Random.Range(0, 101) < critChance)
        {
            attack = attack * BodyPartManager.Instance.CritChanceMultiplier;
            crit = true;
            Debug.Log($"{gameObject.name} CRITS!!!");
        }
        Debug.Log($"{gameObject.name} Preparing to Attack for {attack}");
        damage = Mathf.FloorToInt(attack);
        return crit;
    }

    public bool IsDodging()
    {
        float dodgeChance = entityFightingStats.Dodge * BodyPartManager.Instance.DodgeBalancingMultiplier;
        if (UnityEngine.Random.Range(0, 101) < dodgeChance)
        {
            Debug.Log($"{gameObject.name} dodged the attack!");

            DodgeAnimation();
            return true;
        }
        //Debug.Log($"{gameObject.name} will not dodge the attack!");

        return false;
    }

    public void HitAnimation(bool dodged, bool crit, int damage = 0)
    {
        AnimationManager.Instance.AttackEffect(animationPoint.transform, attackNumberAnimationPosition.transform.position, dodged, damage, crit);
    }

    public void DodgeAnimation()
    {
        HitAnimation(true, false);
        AnimationManager.Instance.DodgeEffect(animationPoint.transform);

    }

    public void BlockAnimation(int damage)
    {
        AnimationManager.Instance.BlockEffect(animationPoint.transform, blockAnimationPosition.transform.position, blockNumberAnimationPosition.transform.position, damage);

    }

    public float AttackAnimation()
    {
        float animationBackDuration = .33f;
        float animationFrontDuration = .3f;
        Vector3 originalPosition = transform.position;
            transform.DOMove(animationBackPoint.transform.position, animationBackDuration).OnComplete(() =>
            {
                transform.DOMove(animationFrontPoint.transform.position, animationFrontDuration).OnComplete(() =>
                {
                    transform.DOMove(originalPosition, animationBackDuration);
                });
            });
        return 0f;
    }

    public int Block(int damage)
    {
        //Debug.Log($"{gameObject.name} Trying to block {damage} damage");
        //Debug.Log($"{gameObject.name} having {entityFightingStats.Block} block");
        if (entityFightingStats.Block > 0)
        {
            int blockedDamage = 0;

            if (entityFightingStats.Block - damage > 0)
            {
                Debug.Log($"{gameObject.name} Blocked {damage} Damage!");
                blockedDamage = damage;
            }
            else
            {
                Debug.Log($"{gameObject.name} Blocked {entityFightingStats.Block} Damage!");
                blockedDamage = entityFightingStats.Block;
            }
            entityFightingStats.Block -= damage;

            BlockAnimation(blockedDamage);
            return -entityFightingStats.Block;
        }
        return damage;
    }

    public void Hit(int damage, bool crit)
    {
        entityFightingStats.HP -= damage;
        Debug.Log($"{gameObject.name} hit by {damage} Damage!");
        HitAnimation(false,crit, damage);


    }

    private void CritAnimation()
    {

        AnimationManager.Instance.CritEffect(animationPoint.transform);

    }

    public bool IsDefeated()
    {
        return entityFightingStats.HP <= 0;
    }

    void Start()
    {
        UpdateBodyGraphics();
    }

    public void ResetStats()
    {
        Debug.Log($"Resetting {gameObject.name} Stats!");
        entityFightingStats = new Stats(entityBaseStats);
        foreach (var bodyParts in bodyParts)
        {
            entityFightingStats.Add(bodyParts.bodyPartSO.stats);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
