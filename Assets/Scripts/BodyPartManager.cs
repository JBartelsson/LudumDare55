using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartManager : MonoBehaviour
{
   public static BodyPartManager Instance { get; private set; }
    public List<BodyPartSO> bodyParts;

    private void Awake()
    {
        Instance = this;
    }
}
