using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{
    // Variables that other script have access to (for AI Path Scripts)
    public AIDestinationSetter targetSystem;         // For changing Target
    public AIPath pathingSystem;                     // For changing Speed

    // Constant values to change the monster's behaviour patterns
    private const int PASSIVE_SPEED = 2;
    private const int AGGRO_SPEED = 3;
    private const float SECONDS_TO_LOSE_AGGRO = 5.0f;
    private const float DETECTION_DISTANCE = 2.5f;
    private const float SECONDS_TO_STALK = 1.0f;
    private const float SECONDS_TO_GUARD = 3.0f;

    // Variables used for the monster's behaviour
    private MonsterAttributes MAttributes;
    private float PassiveTimer = 0.0f;
    private bool TargetChangeNeeded = true;
    private bool Aggroed = false;
    private float AggroTimer = 0.0f;
    


    void Start()
    {
        // Randomly generate a new monster based on attributes
        MAttributes = new MonsterAttributes((MonsterType) UnityEngine.Random.Range(1, 2));
    }

    void FixedUpdate()
    {
        /**
         *        =============
         *        PASSIVE STATE
         *        =============
         */
        if (!Aggroed)
        {
            /**
             *      TARGET AND SPEED CHANGE FROM AGGRO OR STARTING OFF
             */
            // Change target off player and speed to calmer pace, when initiated
            if (TargetChangeNeeded)
            {
                targetSystem.target = null;

                pathingSystem.maxSpeed = PASSIVE_SPEED;

                TargetChangeNeeded = false;
            }


            /**
             *      PASSIVE BEHAVIOURS
             */
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




            // Detection System for when Monster is not Aggroed yet
            if (FoundPlayerOnSight())
            {
                // Change to Aggro state
                Aggroed = true;
                TargetChangeNeeded = true;
            }

        }
        /**         
         *        =============
         *        AGGROED STATE
         *        =============
         */
        else
        {
            /**
             *      TARGET AND SPEED CHANGE FROM PASSIVE
             */
            // Change target and speed to chase the player at a faster pace, when initiated
            if (TargetChangeNeeded)
            {
                targetSystem.target = GameObject.Find("Player").transform;

                pathingSystem.maxSpeed = AGGRO_SPEED;

                TargetChangeNeeded = false;
            }
            

            /**
             *      AGGRO BEHAVIOURS
             */ 
            if (MAttributes.MAggro == MonsterAggro.Chaser)
            {
                // Nothing needs to be done here as the speed and target being changed is the only thing needed to be a Chaser
            }



            if (FoundPlayerOnSight()) // Check if Monster can see Player (This should be its own method)
            {
                // Reset EscapeTimer 
            }
            else
            {
                // Decrement Timer 

                if (false) // Check if Timer is lesser than or equal to 0
                {
                    // Go back to Passive State
                    Aggroed = false;
                    TargetChangeNeeded = true;
                }
            }
        }

        
    }

    /**
     * Checks if the monster is able to see the player at a certain radius; Raycasts with walls as colliders
     */
    private bool FoundPlayerOnSight()
    {
        LayerMask walls = LayerMask.GetMask("Wall");
        LayerMask player = LayerMask.GetMask("Player");

        // First, check if the player is within the radius of detection 
        Collider2D playerNearby = Physics2D.OverlapCircle((Vector2) transform.position, DETECTION_DISTANCE, player);

        if (playerNearby != null)
        {
            // Then, check if the player is behind a wall
            Vector2 directionOfPlayer = (Vector2)playerNearby.transform.position - (Vector2)transform.position;
            RaycastHit2D wallInWay = Physics2D.Raycast((Vector2)transform.position, directionOfPlayer.normalized, DETECTION_DISTANCE, walls);

            // Returns true if there is no wall in the way, else false if there is
            return wallInWay.collider == null;
        }

        // Case in which the player is not in the radius of detection
        return false;
    }
}
