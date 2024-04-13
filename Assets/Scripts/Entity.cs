using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum SpecificBodyPart
    {
        LeftLeg, RightLeg, LeftArm, RightArm, Body, Head
    }
    public Stats entityStats;
    public Dictionary<SpecificBodyPart, BodyPartSO> bodyParts = new();
    // Start is called before the first frame update
    
    public void SwitchBodyPart(SpecificBodyPart bodyPart, BodyPartSO newBodyPart)
    {
        bodyParts[bodyPart] = newBodyPart;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
