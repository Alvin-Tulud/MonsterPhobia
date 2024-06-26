using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{

    private MonsterAttributes MAttributes;
    private bool Aggroed;
    private int EscapeTimer;


    void Start()
    {
        // Randomly generate a monsterStruct
        MAttributes = new MonsterAttributes((MonsterType) UnityEngine.Random.Range(1, 3));

        Aggroed = false;
    }

    void FixedUpdate()
    {   
        // Do behavior stuff with MonsterPassive
        if (!Aggroed)
        {
            if (MAttributes.MPassive == MonsterPassive.Wander)
            {
                // Change targets between corners for patrols
                // Change to High Speed
            }

            if (MAttributes.MPassive == MonsterPassive.Stalk)
            {
                // Change targets between corners for stalking
                // Change to Medium Speed
            }

            if (MAttributes.MPassive == MonsterPassive.Guard)
            {
                // Change targets to corners within the area being guarded
                // Change to Low Speed
            }
        }
        // Do chasing stuff with MonsterAggro
        else
        {
            if (MAttributes.MAggro == MonsterAggro.Rush)
            {
                // Change target to Player character
            }
        }
        



        

    }
}
