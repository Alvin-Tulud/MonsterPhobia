using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GUIControl : MonoBehaviour
{
    GameObject book;
    GameObject guess;
    PlayerMove player;
    FlashLightMove flashLight;
    bool onoff;

    // Start is called before the first frame update
    void Start()
    {
        guess = transform.GetChild(0).gameObject;
        book = transform.GetChild(1).gameObject;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        flashLight = GameObject.FindGameObjectWithTag("FlashLight").GetComponent<FlashLightMove>();

        onoff = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            book.SetActive(!onoff);
            guess.SetActive(!onoff);

            player.enabled = onoff;
            flashLight.enabled = onoff;

            onoff = !onoff;
        }
    }
}
