using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartManager : MonoBehaviour
{
   public static BodyPartManager Instance { get; private set; }
    public List<BodyPartSO> bodyParts;

    public float HPBalancingMultiplier = 1f;
    public float DMGBalancingMultiplier = 1f;
    public float BlockBalancingMultiplier = 1f;
    public float CritChanceBalancingMultiplier = 1f;
    public float DodgeBalancingMultiplier = 1f;


    public float CritChanceMultiplier = 2f;
    private void Awake()
    {
        Instance = this;
    }
}
