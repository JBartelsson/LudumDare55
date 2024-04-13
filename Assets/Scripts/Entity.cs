using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum SpecificBodyPart
    {
        LeftLeg, RightLeg, LeftArm, RightArm, Body, Head
    }
    public Stats entityStats;
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
  
    // Start is called before the first frame update
    
    public void SwitchBodyPart(SpecificBodyPart bodyPosition, BodyPartSO newBodyPart)
    {
        bodyParts.Where((x) => x.bodyPosition == bodyPosition).First().bodyPartSO = newBodyPart;
    }

    private void UpdateBodyGraphics()
    {
        foreach (var bodyPartSprite in bodyPartSprites)
        {
            BodyPartSO temp = bodyParts.Where((x) => )
            bodyPartSprite
        }
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
