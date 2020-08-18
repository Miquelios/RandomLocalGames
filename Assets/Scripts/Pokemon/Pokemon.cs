using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class Pokemon
{

    [SerializeField] PokemonBase _base;
    [SerializeField] int level;
    public PokemonBase Base { 
        get
        {
            return _base;
        }
    }
    public int Level { 
        get
        {
            return level;
        }
    }

    public int HP { get; set; }

    public List<Move> Moves { get; set; }

    public void Init()
    {
        
        HP = MaxHp;


        //Generates moves based on pokemons level
        Moves = new List<Move>();
        foreach (var move in Base.LearnableMoves)
        {
            if (move.Level <= Level)
                Moves.Add(new Move(move.Base));

            if (Moves.Count >= 4)
                break;
        }
    }

    public int Attack
    {
        get { return Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5; } //formula del joc de pokemon per calcular l'atac
    }
    public int Defense
    {
        get { return Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5; } //formula del joc de pokemon per calcular l'atac
    }
    public int SpAttack
    {
        get { return Mathf.FloorToInt((Base.SpAttack * Level) / 100f) + 5; } //formula del joc de pokemon per calcular l'atac
    }
    public int SpDefense
    {
        get { return Mathf.FloorToInt((Base.SpDefense * Level) / 100f) + 5; } //formula del joc de pokemon per calcular l'atac
    }
    public int Speed
    {
        get { return Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5; } //formula del joc de pokemon per calcular l'atac
    }
    public int MaxHp
    {
        get { return Mathf.FloorToInt((Base.MaxHp * Level) / 100f) + 10; } //formula del joc de pokemon per calcular l'atac
    }

    public DamageDetails TakeDamage (Move move, Pokemon attacker)
    {
        float critical = 1f;
        if (UnityEngine.Random.value * 100f <= 6.25f)
            critical = 2f;

        float type = TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type1) * TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type2);

        var damageDetails = new DamageDetails()
        {
            Type = type,
            Critical = critical,
            Fainted = false
        };

        float attack = (move.Base.IsSpecial) ? attacker.SpAttack : attacker.Attack; // aixo es com un if else pero mes curt
        float defense = (move.Base.IsSpecial) ? SpDefense : Defense;

        float modifiers = UnityEngine.Random.Range(0.85f, 1f) * type * critical;
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * move.Base.Power * ((float)attack / defense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            damageDetails.Fainted = true; //pokemon = fainted
        }
        return damageDetails;
    }

    public Move GetRandomMove()
    {
        int r = UnityEngine.Random.Range(0, Moves.Count);
        return Moves[r];
    }
}

public class DamageDetails
{
    public bool Fainted { get; set;  }
    public float Critical { get; set; }
    public float Type { get; set;  }
}