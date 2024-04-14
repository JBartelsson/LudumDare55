using System;

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
    public Stats(Stats otherStats)
    {
        HP = otherStats.HP;
        Attack = otherStats.Attack;
        Block = otherStats.Block;
        Crit = otherStats.Crit;
        Dodge = otherStats.Dodge;
    }

    public void Add(Stats otherStats)
    {
        HP += otherStats.HP;
        Attack += otherStats.Attack;
        Block += otherStats.Block;
        Crit += otherStats.Crit;
        Dodge += otherStats.Dodge;
    }
    
}
