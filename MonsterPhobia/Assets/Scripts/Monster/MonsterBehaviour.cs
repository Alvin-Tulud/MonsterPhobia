using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{
    // Variables that other script have access to (for AI Path Scripts)
    public Transform CurrentTarget = null;
    public int currentSpeed = 0;

    private const int LOW_SPEED = 2;
    private const int MEDIUM_SPEED = 3;
    private const int HIGH_SPEED = 4;
    private const int TIMER_MAX_IN_SECONDS = 5;

    private MonsterAttributes MAttributes;
    private bool Aggroed = false;
    private float EscapeTimer = 0.0f;


    void Start()
    {
        // Randomly generate a monsterStruct
        MAttributes = new MonsterAttributes((MonsterType) UnityEngine.Random.Range(1, 2));
    }

    void FixedUpdate()
    {   
        // Do behavior stuff with MonsterPassive
        if (!Aggroed)
        {
            if (MAttributes.MPassive == MonsterPassive.Wanderer)            // If Random rolled a 1
            {
                // Change targets between corners for patrols
                // Change to High Speed
            }

            if (MAttributes.MPassive == MonsterPassive.Stalker)             // If Random rolled a 2
            {
                // Change targets between corners for stalking
                // Change to Medium Speed
            }

            if (MAttributes.MPassive == MonsterPassive.Territorial)         // If Random rolled a 3
            {
                // Change targets to corners within the area being guarded
                // Change to Low Speed
            }
        }
        // Do chasing stuff with MonsterAggro
        else
        {
            if (MAttributes.MAggro == MonsterAggro.Chaser)
            {
                // Change target to Player character 

                if (true) // Check if Monster can see Player (This should be its own method)
                {
                    // Reset EscapeTimer 
                } else
                {
                    // Decrement Timer 

                    if (false) // Check if Timer is lesser than or equal to 0
                    {
                        // Go back to passive
                    }   
                }

            }
        }
    }

    /**
     * Checks if the monster is able to see the player at a certain radius; Raycasts with walls as colliders
     */
    private bool FoundPlayerOnSight()
    {
        return true;
    }
}
