using CartoonFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager Instance { get; private set; }
    [SerializeField] private Transform effectParent;
    [SerializeField] GameObject attackEffect;
    [SerializeField] GameObject blockEffect;
    [SerializeField] GameObject critEffect;
    [SerializeField] GameObject dodgeEffect;
    [SerializeField] float effectTime = 1f;

    public enum AnimationType
    {
        Attack, Block, Dodge, Crit
    }
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
    }

   

    public void AttackEffect(Transform animateTarget)
    {
        GameObject effectGameObject = Instantiate(attackEffect, effectParent);
        effectGameObject.transform.position = animateTarget.position;
        effectGameObject.GetComponent<CFXR_Effect>().Animate(effectTime);
    }

    public void BlockEffect(Transform animateTarget)
    {
        GameObject effectGameObject = Instantiate(blockEffect, effectParent);
        effectGameObject.transform.position = animateTarget.position;
        effectGameObject.GetComponent<CFXR_Effect>().Animate(effectTime);
    }

    public void DodgeEffect(Transform animateTarget)
    {
        GameObject effectGameObject = Instantiate(dodgeEffect, effectParent);
        effectGameObject.transform.position = animateTarget.position;
        effectGameObject.GetComponent<CFXR_Effect>().Animate(effectTime);
    }

    public void CritEffect(Transform animateTarget)
    {
        GameObject effectGameObject = Instantiate(critEffect, effectParent);
        effectGameObject.transform.position = animateTarget.position;
        effectGameObject.GetComponent<CFXR_Effect>().Animate(effectTime);
    }
}
