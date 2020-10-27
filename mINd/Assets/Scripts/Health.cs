using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    private float healthPoints;
    private float maxHealthPoints;

    public float Min { set; get; }
    public float Max
    {
        set
        {
            if (value > Min)
                maxHealthPoints = value;
            else
            {
                maxHealthPoints = Min + 1;
            }
        }

        get
        {
            return maxHealthPoints;
        }
    }
    public float Current
    {
        set
        {
            value = Mathf.Clamp(value, Min, Max);
            healthPoints = value;
        }
        get
        {
            return healthPoints;
        }
    }

    public Health()
    {
        Min = 0;
        Max = 100;
        Current = Max;
    }

    public Health(int maximal)
    {
        Min = 0;
        Max = maximal;
        Current = Max;
    }

    public Health(int maximal, int current)
    {
        Min = 0;
        Max = maximal;
        Current = current;
    }

    public Health(int maximal, int current, int minimal)
    {
        Min = minimal;
        Max = maximal;
        Current = current;
    }

    public void Damage(float value)
    {
        Current -= value;
    }

    public void Heal(float value)
    {
        Current += value;
    }
}
