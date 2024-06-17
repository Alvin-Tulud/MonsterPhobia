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
        flashLight = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<FlashLightMove>();

        onoff = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            book.SetActive(!onoff);


            player.enabled = onoff;
            flashLight.enabled = onoff;


            onoff = !onoff;
        }
    }
}