using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    bool canMove;
    Rigidbody2D rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
    }

    private Vector2 PlayerDirection = Vector2.zero;

    private void FixedUpdate()
    {
        if (canMove)
        {   
            // Check for W and S input (Goes nowhere if both are pressed)
            if (Input.GetKey(KeyCode.W))
            {
                //Debug.Log("W");
                PlayerDirection.y += 1f;
            } 
            if (Input.GetKey(KeyCode.S))
            {
                //Debug.Log("S");
                PlayerDirection.y -= 1f;
            }


            // Check for A and D input (Goes nowhere if both are pressed)
            if (Input.GetKey(KeyCode.D))
            {
                //Debug.Log("D");
                PlayerDirection.x += 1f;
            } 
            if (Input.GetKey(KeyCode.A))
            {
                //Debug.Log("A");
                PlayerDirection.x -= 1f;
            }
            

            // Convert the vector to normalized form and change player's velocity
            PlayerDirection.Normalize();

            rb.velocity = PlayerDirection * speed;

            // Reset PlayerDirection for next iteration of FixedUpdate
            PlayerDirection = Vector2.zero;
        }
    }

    public void setCanMove(bool can) { canMove = can; }

    public bool getCanMove() { return canMove; }


}
