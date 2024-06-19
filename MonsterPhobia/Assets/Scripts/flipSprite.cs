using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flipSprite : MonoBehaviour
{
    Transform spritetoflip;
    Vector2 pathing;
    void Start()
    {
        spritetoflip = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.GetComponent<AIPath>() != null)
        {
            pathing = transform.parent.GetComponent<AIPath>().velocity;
        }
        else if (transform.parent.GetComponent<Rigidbody2D>() != null)
        {
            pathing = transform.parent.GetComponent<Rigidbody2D>().velocity;
        }



        if (pathing.x < 0)
        {
            //Debug.Log(pathing + " : " + transform.parent.name + " : flip");
            spritetoflip.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (pathing.x > 0)
        {
            //Debug.Log(pathing + " : " + transform.parent.name);
            spritetoflip.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
