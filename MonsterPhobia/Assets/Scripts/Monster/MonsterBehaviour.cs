using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class MonsterBehaviour : MonoBehaviour
{
    // Variables that other script have access to (for AI Path Scripts)
    public AIDestinationSetter targetSystem;         // For changing Target
    public AIPath pathingSystem;                     // For changing Speed

    // Constant values to change the monster's behaviour patterns
    private const int PASSIVE_SPEED = 3;
    private const int AGGRO_SPEED = 3;
    private const int MAX_EXCLUDED_CORNERS = 10;
    private const float SECONDS_TO_LOSE_AGGRO = 15.0f;
    private const float DETECTION_DISTANCE = 5.0f;
    private const float SECONDS_TO_STALK = 3.0f;
    private const float SECONDS_TO_GUARD = 4.0f;

    // Variables used for the monster's behaviour
    public MonsterAttributes MAttributes;
    private Queue<GameObject> ExcludedCorners;
    private int QueueLength = 0;
    private bool CornerTargetChange = true;
    private bool WaitingAtTarget = false;
    private bool ChangedMonsterState = true;
    public bool Aggroed = false;
    private float EvasionTimer = 0.0f;
    


    void Start()
    {
        // Randomly generate a new monster based on attributes
        MAttributes = new MonsterAttributes((MonsterType) UnityEngine.Random.Range(1, 3));
        // Start up ExcludedCorners Queue
        ExcludedCorners = new Queue<GameObject>();

        // Converts nearby corners to territory when spawned as Territorial
        if (MAttributes.MPassive == MonsterPassive.Territorial)
        {
            float RADIUS_FOR_TERRITORY = 25.0f;
            LayerMask cornerMask = LayerMask.GetMask("Corner");
            Vector2 locationOfMonster = (Vector2)transform.position;

            Collider2D[] allCornersNearby = Physics2D.OverlapCircleAll(locationOfMonster, RADIUS_FOR_TERRITORY, cornerMask);

            foreach (Collider2D corner in allCornersNearby)
            {
                corner.tag = "cornerterritory";
            }
        }

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
                StalkerAlgorithm();
            }

            if (MAttributes.MPassive == MonsterPassive.Territorial)         // If Random rolled a 3
            {
                TerritorialAlgorithm();
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
                pathingSystem.canMove = true;
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
                targetSystem.target = GameObject.FindGameObjectWithTag("Player").transform;

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
                EvasionTimer += Time.deltaTime;

                if (EvasionTimer >= SECONDS_TO_LOSE_AGGRO) // Check if Player has evaded the monster for SECONDS_TO_LOSE_AGGRO seconds
                {
                    // Go back to Passive State
                    Aggroed = false;
                    ChangedMonsterState = true;
                    // Reset timer for next time
                    EvasionTimer = 0.0f;
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
     *  HELPER METHODS FOR PASSIVE STATE
     * 
     */


    /**
     * Deals with the queue for ExcludedCorners, from adding them and changing their tag to "cornerExcluded_h" or "cornerExcluded_p" to removing them and restoring their original tag name
     */
    private void addToExcludedCorners(GameObject newCorner)
    {
        if (newCorner == null) { return; }


        // Changes tag to excluded variant
        if (newCorner.CompareTag("cornerhide"))
        {
            newCorner.tag = "cornerExcluded_h";
        }
        else if (newCorner.CompareTag("cornerpeek"))
        {
            newCorner.tag = "cornerExcluded_p";
        }
        else if (newCorner.CompareTag("cornerterritory"))
        {
            newCorner.tag = "cornerExcluded_t";
        }

        ExcludedCorners.Enqueue(newCorner);

        // Makes sure the queue does not go over the size of MAX_EXCLUDED_CORNERS
        if (ExcludedCorners.Count > MAX_EXCLUDED_CORNERS)
        {
            removeHeadOfExcludedCorners();
        }

        QueueLength = ExcludedCorners.Count;
    }
    private GameObject removeHeadOfExcludedCorners()
    {
        GameObject corner = ExcludedCorners.Dequeue();

        if (corner == null) { return null; }

        // Changes tag back to original variant
        if (corner.CompareTag("cornerExcluded_h"))
        {
            corner.tag = "cornerhide";
        }
        else if (corner.CompareTag("cornerExcluded_p"))
        {
            corner.tag = "cornerpeek";
        }
        else if (corner.CompareTag("cornerExcluded_t"))
        {
            corner.tag = "cornerterritory";
        }

        QueueLength = ExcludedCorners.Count;

        return corner;
    }
    private void clearExcludedCorners()
    {
        while (ExcludedCorners.Count > 0)
        {
            removeHeadOfExcludedCorners();
        }
    }

    /**
     *  Deals with using the exlcuded corners and the tag needed to find a specific kind of corner (string cornerType = null)
     */ 
    private GameObject nearestCornerToEntity(Transform entity, string cornerType = null)
    {
        const float RADIUS_FOR_SEARCH = 50.0f;
        LayerMask cornerMask = LayerMask.GetMask("Corner");
        Vector2 locationOfEntity = (Vector2) entity.position;
        
        Collider2D[] allCorners = Physics2D.OverlapCircleAll(locationOfEntity, RADIUS_FOR_SEARCH, cornerMask);

        if (allCorners.Length == 0) { return null; }

        //Debug.DrawLine(locationOfEntity, allCorners[0].transform.position,Color.red,2.0f);

        // Remove corners from ExcludedCorners
        allCorners = allCorners.Where(corner => !corner.CompareTag("cornerExcluded_p") && !corner.CompareTag("cornerExcluded_h") && !corner.CompareTag("cornerExcluded_t") ).ToArray();

        // Remove all corner that DON'T use the same tag, or do nothing if no tag is specified
        if (cornerType != null)
        {
            allCorners = allCorners.Where(corner => corner.CompareTag(cornerType)).ToArray();
        }

        // Sort allCorners by distance from the Entity
        allCorners = allCorners.OrderBy(corner => (((Vector2) corner.transform.position) - locationOfEntity).magnitude).ToArray();

        //Debug.DrawLine(locationOfEntity, allCorners[0].transform.position, Color.blue, 2.0f);


        if (allCorners.Length > 0)
        {
            //Debug.Log(allCorners[0].gameObject);
            return allCorners[0].gameObject;
        }
        return null;
    }

    /**
     *  Checks if the monster is near the target corner
     */
    private bool nearTargetCorner()
    {
        const float RADIUS_FOR_CORNER = 0.7f;

        Vector2 locationOfMonster = (Vector2) transform.position;
        GameObject targetCorner = targetSystem.target.gameObject;

        // Detects the corner with a circular hitbox that is 1.5 units in length, centered at the Monster
        return Physics2D.CircleCastAll(locationOfMonster, RADIUS_FOR_CORNER, Vector2.zero).Where(hit => hit.transform.gameObject == targetCorner).ToArray().Length > 0;
    }


    /**
     * Deals with the passive behaviour of a Wanderer monster
     */
    private void WandererAlgorithm()
    {   
        if (CornerTargetChange)
        {   
            // Find new Corner to go to
            GameObject newCorner = nearestCornerToEntity(transform,"cornerhide");
            // Add new Corner to queue
            addToExcludedCorners(newCorner);
            // Set target to the new Corner
            targetSystem.target = newCorner.transform;
              
            // Start doing NOT this algo
            CornerTargetChange = false;
        } 
        else
        {
            // Make new target, or immediately go to the next corner, after reaching the current target corner
            if (nearTargetCorner())
            {
                CornerTargetChange = true;
            }
        }
    }

    /**
     *  Coroutine method to allow for waiting before changing the target corner
     */ 
    IEnumerator WaitAfterReachingTarget(float seconds)
    {

        yield return new WaitForSeconds(seconds);

        CornerTargetChange = true;
    }

    /**
     * Deals with the passive behaviour of a Stalker monster
     */
    private void StalkerAlgorithm()
    {
        if (CornerTargetChange)
        {
            // Makes Stalker not wait at previous target
            WaitingAtTarget = false;

            // Find new Corner to go to
            GameObject newCorner = nearestCornerToEntity(GameObject.FindGameObjectWithTag("Player").transform, "cornerpeek");
            // Add new Corner to queue
            addToExcludedCorners(newCorner);
            // Set target to the new Corner
            targetSystem.target = newCorner.transform;

            // Start doing NOT this algo
            CornerTargetChange = false;
        }
        else
        {
            // Make new Target after reaching current target corner
            if (nearTargetCorner() && !WaitingAtTarget)
            {
                WaitingAtTarget = true;
                StartCoroutine(WaitAfterReachingTarget(SECONDS_TO_STALK));
            }
        }
    }


    

    /**
     * Deals with the passive behaviour of a Territorial monster
     */
    private void TerritorialAlgorithm()
    {
        if (CornerTargetChange)
        {
            // Makes Stalker not wait at previous target
            WaitingAtTarget = false;

            // Find new Corner to go to
            GameObject newCorner = nearestCornerToEntity(transform, "cornerterritory");

            // Deals with the case of when the number of corners in the territory is less than MAX_EXCLUDED_CORNERS
            if (newCorner == null)
            {
                newCorner = removeHeadOfExcludedCorners();
            }

            // Add new Corner to queue
            addToExcludedCorners(newCorner);
            // Set target to the new Corner
            targetSystem.target = newCorner.transform;

            // Start doing NOT this algo
            CornerTargetChange = false;
        }
        else
        {
            // Make new Target after reaching current target corner
            if (nearTargetCorner() && !WaitingAtTarget)
            {
                WaitingAtTarget = true;
                StartCoroutine(WaitAfterReachingTarget(SECONDS_TO_GUARD));
            }
        }
    }

    public bool GetAggroState()
    {
        return Aggroed;
    }
}
