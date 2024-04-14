using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

[CreateAssetMenu(menuName = "Scriptable Objects/BodyPart", fileName = "BodyPart")]
public class BodyPartSO : ScriptableObject
{
    
    public enum Type
    {
        FairyTale, Underground, Food, None
    }
    public enum Rarity
    {
        Common, Rare, Legendary
    }
    [Header("Sound & Graphic Setting")]

    [SerializeField] public Type type;
    [Header("Game Setting")]
    [SerializeField] public bool isDefault = false;
    [SerializeField] public string bodyPartname = "";
    [SerializeField] public Entity.SpecificBodyPart bodyPosition;
    [SerializeField] public Rarity rarity;
    [Header("Stats")]
    [SerializeField] public Stats stats;
    [Header("Graphic")]
    [SerializeField] public Sprite sprite;

    
}

