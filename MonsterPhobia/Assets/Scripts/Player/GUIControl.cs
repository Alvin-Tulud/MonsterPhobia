using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIControl : MonoBehaviour
{
    GameObject book;
    PlayerMove player;
    FlashLightMove flashLight;
    bool onoff;

    // Start is called before the first frame update
    void Start()
    {
        book = transform.GetChild(0).gameObject;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        flashLight = GameObject.FindGameObjectWithTag("FlashLight").GetComponent<FlashLightMove>();

        onoff = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("here");
            book.SetActive(!onoff);

            Debug.Log("here1");
            player.enabled = onoff;
            Debug.Log("here2");
            flashLight.enabled = onoff;

            Debug.Log("here3");
            onoff = !onoff;
        }
    }
}
