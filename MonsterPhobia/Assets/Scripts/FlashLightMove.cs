using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightMove : MonoBehaviour
{
    Transform flashlight;
    
    void Start()
    {
        flashlight = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 playerPos = flashlight.parent.transform.position;

        mousePos.x -= playerPos.x;
        mousePos.y -= playerPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        flashlight.rotation = Quaternion.Euler(0,0,angle);
    }
}
