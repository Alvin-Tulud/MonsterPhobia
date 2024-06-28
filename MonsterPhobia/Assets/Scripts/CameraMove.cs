using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform cam;
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();

        if (player.position.x > cam.position.x + 9)
        {
            //Debug.Log("right");
            cam.position = new Vector3(cam.position.x + 18, cam.position.y, cam.position.z);
        }
        else if (player.position.x < cam.position.x - 9)
        {
            //Debug.Log("left");
            cam.position = new Vector3(cam.position.x - 18, cam.position.y, cam.position.z);
        }
        else if (player.position.y > cam.position.y + 5)
        {
            //Debug.Log("up");
            cam.position = new Vector3(cam.position.x, cam.position.y + 10, cam.position.z);
        }
        else if (player.position.y < cam.position.y - 5)
        {
            //Debug.Log("down");
            cam.position = new Vector3(cam.position.x, cam.position.y - 10, cam.position.z);
        }
    }
}
