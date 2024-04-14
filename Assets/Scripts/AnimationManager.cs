using CartoonFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager Instance { get; private set; }
    [SerializeField] private Transform effectParent;
    [SerializeField] GameObject attackEffect;
    [SerializeField] GameObject attackTextEffect;
    [SerializeField] GameObject blockEffect;
    [SerializeField] GameObject blockTextEffect;
    [SerializeField] GameObject critEffect;
    [SerializeField] GameObject critTextEffect;
    [SerializeField] GameObject dodgeEffect;
    [SerializeField] GameObject dodgeTextEffect;
    [SerializeField] Transform blockedPosition;
    [Header("Texts")]
    [SerializeField] GameObject attackNumberText;
    [SerializeField] GameObject blockNumberText;
    [SerializeField] GameObject critNumberText;

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



    public void AttackEffect(Transform animateTarget, Vector3 attackNumberPosition, bool dodged, int damage, bool crit)
    {
        if (!dodged) { 
            if (!crit)
            {
                GameObject effectTextNumberGameObject = Instantiate(attackNumberText, effectParent);
                effectTextNumberGameObject.GetComponent<CFXR_ParticleText>().UpdateText(damage.ToString());
                effectTextNumberGameObject.transform.position = attackNumberPosition;
                GameObject effectTextGameObject = Instantiate(attackTextEffect, effectParent);
                effectTextGameObject.transform.position = animateTarget.position;
                AudioManager.Instance.PlayHitSound();

            } else
            {
                GameObject effectTextNumberGameObject2 = Instantiate(critNumberText, effectParent);
                effectTextNumberGameObject2.GetComponent<CFXR_ParticleText>().UpdateText(damage.ToString());
                effectTextNumberGameObject2.transform.position = attackNumberPosition;

                GameObject effectTextGameObject = Instantiate(critTextEffect, effectParent);
                effectTextGameObject.transform.position = animateTarget.position;

                GameObject effectGameObject2 = Instantiate(critEffect, effectParent);
                effectGameObject2.transform.position = animateTarget.position;
                effectGameObject2.GetComponent<CFXR_Effect>().Animate(effectTime);

            }
        

        }
        

        GameObject effectGameObject = Instantiate(attackEffect, effectParent);
        effectGameObject.transform.position = animateTarget.position;
        effectGameObject.GetComponent<CFXR_Effect>().Animate(effectTime);

        
    }

    public void BlockEffect(Transform animateTarget, Vector3 blockPosition, Vector3 blockTextPosition, int damage)
    {
        GameObject effectNumberGameObject = Instantiate(blockNumberText, effectParent);
        effectNumberGameObject.transform.position = blockTextPosition;
        effectNumberGameObject.GetComponent<CFXR_ParticleText>().UpdateText("-"+damage.ToString());

        GameObject effectTextGameObject = Instantiate(blockTextEffect, effectParent);
        effectTextGameObject.transform.position = blockPosition;

        GameObject effectGameObject = Instantiate(attackEffect, effectParent);
        effectGameObject.transform.position = animateTarget.position;
        effectGameObject.GetComponent<CFXR_Effect>().Animate(effectTime);

        GameObject effectGameObject2 = Instantiate(blockEffect, effectParent);
        effectGameObject2.transform.position = animateTarget.position;
        effectGameObject2.GetComponent<CFXR_Effect>().Animate(effectTime);

    }

    public void DodgeEffect(Transform animateTarget)
    {
        GameObject effectTextGameObject = Instantiate(dodgeTextEffect, effectParent);
        effectTextGameObject.transform.position = animateTarget.position;

        GameObject effectGameObject = Instantiate(dodgeEffect, effectParent);
        effectGameObject.transform.position = animateTarget.position;
        effectGameObject.GetComponent<CFXR_Effect>().Animate(effectTime);

        


    }

    public void CritEffect(Transform animateTarget)
    {
        
    }
}
