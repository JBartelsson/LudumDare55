using System;
using System.Collections;
using System.Collections.Generic;
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

    public Stats EntityFightingStats { get => entityFightingStats; set => entityFightingStats = value; }

    // Start is called before the first frame update


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

    public bool IsDodging() { 
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

    public void HitAnimation(bool dodged)
    {
        AnimationManager.Instance.AttackEffect(animationPoint.transform, dodged);
    }

    public void DodgeAnimation()
    {
        HitAnimation(true);
        AnimationManager.Instance.DodgeEffect(animationPoint.transform);

    }

    public void BlockAnimation()
    {
        AnimationManager.Instance.BlockEffect(animationPoint.transform);

    }

    public int Block(int damage)
    {
        //Debug.Log($"{gameObject.name} Trying to block {damage} damage");
        //Debug.Log($"{gameObject.name} having {entityFightingStats.Block} block");
        if (entityFightingStats.Block > 0)
        {
            if (entityFightingStats.Block - damage > 0)
            {
                Debug.Log($"{gameObject.name} Blocked {damage} Damage!");
            } else
            {
                Debug.Log($"{gameObject.name} Blocked {entityFightingStats.Block} Damage!");

            }
            entityFightingStats.Block -= damage;
            
            BlockAnimation();
            return -entityFightingStats.Block;
        }
        return damage;
    }

    public void Hit(int damage, bool crit)
    {
        entityFightingStats.HP -= damage;
        Debug.Log($"{gameObject.name} hit by {damage} Damage!");

        if (!crit)
        {
            HitAnimation(false);
        } else
        {
            CritAnimation();
        }
        

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
