using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    bool canMove;
    Rigidbody2D rb;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Debug.Log("W");
                rb.velocity = Vector2.up * speed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                Debug.Log("S");
                rb.velocity = Vector2.down * speed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("D");
                rb.velocity = Vector2.right * speed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                Debug.Log("A");
                rb.velocity = Vector2.left * speed;
            }
        }
    }

    public void setCanMove(bool can) { canMove = can; }
}
