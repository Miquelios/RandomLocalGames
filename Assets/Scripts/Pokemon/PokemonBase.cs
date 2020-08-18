using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Aixo permet que a UNITY, al fer click dret aparegui l'opció de crear un pokemon.
[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new pokemon" )]
public class PokemonBase : ScriptableObject
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    
    [SerializeField] PokemonType type1;
    [SerializeField] PokemonType type2;

    //BaseStats
    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;

    [SerializeField] List<LearnableMove> learnableMoves;


    //aixo es una manera de tenir acces a una private variable fora de la seva classe
    public string Name {
        get { return name; }
    }

    public string Description {
        get { return description; }
    }

    public Sprite FrontSprite {
        get { return frontSprite; }
    }

    public Sprite BackSprite {
        get { return backSprite; }
    }

    public PokemonType Type1  {
        get { return type1; }
    }

    public PokemonType Type2  {
        get { return type2; }
    }

    public int MaxHp  {
        get { return maxHp; }
    }
    public int Attack
    {
        get { return attack; }
    }
    public int Defense
    {
        get { return defense; }
    }
    public int SpAttack
    {
        get { return spAttack; }
    }
    public int SpDefense
    {
        get { return spDefense; }
    }
    public int Speed
    {
        get { return speed; }
    }
    public List<LearnableMove> LearnableMoves
    {
        get { return learnableMoves; }
    }
}

[System.Serializable]
public class LearnableMove
{
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;

    public MoveBase Base
    {
        get { return moveBase; }
    }
    
    public int Level
    { get { return level; } 
    }
}

public enum PokemonType
{
    None,
    Normal,
    Fire,
    Water,
    Electric,
    Grass,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dragon

}

public class TypeChart
{
    static float[][] chart =
    {
        //                    NOR   FIR     WAT     ELE     GRA     ICE     FIG     POI     GRO     FLY     PSY     BUG     ROC     GHO     DRA
        /*NOR*/ new float [] { 1f,  1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,    0.5f,    0f,      1f  },
        /*FIR*/ new float [] { 1f,  0.5f,   0.5f,   1f,     2f,     2f,     1f,     1f,     0.5f,   1f,     1f,     2f,    0.5f,    1f,     0.5f },
        /*WAT*/ new float [] { 1f,  2f,     0.5f,   1f,    0.5f,    1f,     1f,     1f,     2f,     1f,     1f,     1f,     2f,     1f,     0.5f },
        /*ELE*/ new float [] { 1f,  1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,      1f },
        /*GRA*/ new float [] { 1f,  0.5f,   2f,     1f,     0.5f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,      1f },
        /*ICE*/ new float [] { 1f,  1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,      1f },
        /*FIG*/ new float [] { 1f,  1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,      1f },
        /*POI*/ new float [] { 1f,  1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,      1f },
        /*GRO*/ new float [] { 1f,  1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,      1f },
        /*FLY*/ new float [] { 1f,  1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,      1f },
        /*PSY*/ new float [] { 1f,  1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,      1f },
        /*BUG*/ new float [] { 1f,  1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,      1f },
        /*ROC*/ new float [] { 1f,  1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,      1f },
        /*GHO*/ new float [] { 1f,  1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,      1f },
        /*DRA*/ new float [] { 1f,  1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,      1f },
    };

    public static float GetEffectiveness (PokemonType attackType, PokemonType defenseType)
    {
        if (attackType == PokemonType.None || defenseType == PokemonType.None)
            return 1;

        int row = (int)attackType - 1;
        int col = (int)defenseType - 1;

        return chart[row][col];
    }
}
