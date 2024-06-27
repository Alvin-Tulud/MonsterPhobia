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
    private Queue<GameObject> ExcludedCorners = new Queue<GameObject>();
    private bool CornerTargetChange = true;
    private float PassiveTimer = 0.0f;
    private bool ChangedMonsterState = true;
    private bool Aggroed = false;
    private float EvasionTimer = 0.0f;
    


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
             *      ++ TARGET AND SPEED CHANGE FROM AGGRO OR STARTING OFF ++ 
             */
            // Change target off player and speed to calmer pace, when initiated
            if (ChangedMonsterState)
            {
                targetSystem.target = null;

                pathingSystem.maxSpeed = PASSIVE_SPEED;

                ChangedMonsterState = false;
            }


            /**
             *      ++ PASSIVE BEHAVIOURS ++
             */
            if (MAttributes.MPassive == MonsterPassive.Wanderer)            // If Random rolled a 1
            {
                WandererAlgorithm();
            }

            if (MAttributes.MPassive == MonsterPassive.Stalker)             // If Random rolled a 2
            { 
                WandererAlgorithm();
            }

            if (MAttributes.MPassive == MonsterPassive.Territorial)         // If Random rolled a 3
            {
                WandererAlgorithm();
            }


            /**
             *      ++ GAINING AGGRO / DETECTION SYSTEM ++
             */
            // Detection System for when Monster is not Aggroed yet
            if (FoundPlayerOnSight())
            {
                // Change to Aggro state
                Aggroed = true;
                ChangedMonsterState = true;
                // Clear out any passive variables used by the monster behaviours
                clearExcludedCorners();
                PassiveTimer = 0.0f;
                CornerTargetChange = true;
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
             *      -- TARGET AND SPEED CHANGE FROM PASSIVE --
             */
            // Change target and speed to chase the player at a faster pace, when initiated; Also initializes the timer for Aggro State
            if (ChangedMonsterState)
            {
                targetSystem.target = GameObject.Find("Player").transform;

                pathingSystem.maxSpeed = AGGRO_SPEED;

                EvasionTimer = 0.0f;

                ChangedMonsterState = false;
            }
            

            /**
             *      -- AGGRO BEHAVIOURS --
             */ 
            if (MAttributes.MAggro == MonsterAggro.Chaser)
            {
                // Nothing needs to be done here as the speed and target being changed is the only thing needed to be a Chaser
            }


            /**
             *      -- LOSING AGGRO / EVASION SYSTEM --
             */
            // Detection System while Monster is Aggroed
            if (FoundPlayerOnSight()) // Resets timer if the monster can see the player
            {
                EvasionTimer = 0.0f;
            }
            else                     // Decrement the timer if the monster is unable to see the player
            {
                EvasionTimer -= Time.deltaTime;

                if (EvasionTimer >= SECONDS_TO_LOSE_AGGRO) // Check if Player has evaded the monster for SECONDS_TO_LOSE_AGGRO seconds
                {
                    // Go back to Passive State
                    Aggroed = false;
                    ChangedMonsterState = true;
                }
            }
        }

        
    }






    /**
     * 
     *  HELPER METHODS FOR AGGRO STATE
     * 
     */ 


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




    /**
     * 
     *  HELPER METHODS FOR AGGRO STATE
     * 
     */


    /**
     * Deals with the queue for ExcludedCorners, from adding them and changing their tag to "cornerExcluded_h" or "cornerExcluded_p" to removing them and restoring their original tag name
     */
    private void addToExcludedCorners(GameObject newCorner)
    {
        if (newCorner.CompareTag("cornerhide"))
        {
            newCorner.tag = "cornerExcluded_h";
        }
        else if (newCorner.CompareTag("cornerpeek"))
        {
            newCorner.tag = "cornerExcluded_p";
        }

        ExcludedCorners.Enqueue(newCorner);
    }
    private void removeHeadOfExcludedCorners()
    {
        GameObject corner = ExcludedCorners.Dequeue();

        if (corner.CompareTag("cornerExcluded_h"))
        {
            corner.tag = "cornerhide";
        }
        else if (corner.CompareTag("cornerExcluded_p"))
        {
            corner.tag = "cornerpeek";
        }
    }
    private void clearExcludedCorners()
    {
        while (ExcludedCorners.Count > 0)
        {
            removeHeadOfExcludedCorners();
        }
    }

    /**
     * Deals with the passive behaviour of a Wanderer monster
     */
    private void WandererAlgorithm()
    {
        // TODO: Implement a system to
        // 1. Find a nearby patrol corner, that is not in ExcludedCorners
        // 2. Set the targeting system to that nearby corner
        // 3. Add that nearby corner to ExcludedCorners, and remove any excess corners if Queue is greater than 10
        // 4. Avoid running this algo again until the monster reached the new corner
    }

    /**
     * Deals with the passive behaviour of a Stalker monster
     */
    private void StalkerAlgorithm()
    {
        // TODO: Implement a system to
        // 1. Find a nearby hide corner, that is not in ExcludedCorners
        // 2. Set the targeting system to that nearby corner
        // 3. Add that nearby corner to ExcludedCorners, and remove any excess corners if Queue is greater than 10
        // 4. Avoid running this algo again until the monster reached the new corner, AND has waiting for SECONDS_TO_STALK
    }

    /**
     * Deals with the passive behaviour of a Territorial monster
     */
    private void TerritorialAlgorithm()
    {
        // TODO: Implement a system to
        // 1. Find a corner with the monster's territory, that is not in ExcludedCorners
        // 2. Set the targeting system to that corner in the area
        // 3. Add that corner to ExcludedCorners, and remove any excess corners if Queue is greater than 10 OR if all corners are explored then clear the queue
        // 4. Avoid running this algo again until the monster reached the new corner, AND has waiting for SECONDS_TO_GUARD
    }
}
