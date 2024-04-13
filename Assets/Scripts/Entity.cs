using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum SpecificBodyPart
    {
        LeftLeg, RightLeg, LeftArm, RightArm, Body, Head
    }
    public Stats entityStats;
    [Serializable]
    private class PositionedBodyPart
    {
        public SpecificBodyPart bodyPosition;
        public BodyPartSO bodyPartSO;
    }
    [Serializable]
    private class BodySprite
    {
        public SpecificBodyPart bodyPosition;
        public SpriteRenderer spriteRenderer;
    }
    [SerializeField] private List<PositionedBodyPart> bodyParts = new();
    [SerializeField] private List<BodySprite> bodyPartSprites = new();
  
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
    
    void Start()
    {
        UpdateBodyGraphics();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
