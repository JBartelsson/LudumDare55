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
        FairyTale, Underground, Food
    }
    public enum Rarity
    {
        Common, Uncommon, Rare, Legendary
    }
    [Header("Sound & Graphic Setting")]

    [SerializeField] public Type type;
    [Header("Game Setting")]
    [SerializeField] public bool isDefault = false;
    [SerializeField] public Entity.SpecificBodyPart bodyPosition;
    [SerializeField] public Rarity rarity;
    [Header("Stats")]
    [SerializeField] private Stats stats;
    [Header("Graphic")]
    [SerializeField] public Sprite sprite;
}
[Serializable]
public class Stats
{
    public int HP;
    public int Attack;
    public int Block;
    public int Crit;
    public int Dodge;

    public Stats(int hP, int attack, int block, int crit, int dodge)
    {
        HP = hP;
        Attack = attack;
        Block = block;
        Crit = crit;
        Dodge = dodge;
    }

    public Stats()
    {
        HP = 0;
        Attack = 0;
        Block = 0;
        Crit = 0;
        Dodge = 0;
    }
}
