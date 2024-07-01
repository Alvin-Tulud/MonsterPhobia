using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GUIControl : MonoBehaviour
{
    GameObject book;
    GameObject guess;
    GameObject narration;
    PlayerMove player;
    FlashLightMove flashLight;
    bool onoff;

    // Start is called before the first frame update
    void Start()
    {
        guess = transform.GetChild(0).gameObject;
        book = transform.GetChild(1).gameObject;
        narration = transform.GetChild(2).gameObject;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        flashLight = GameObject.FindGameObjectWithTag("FlashLight").GetComponent<FlashLightMove>();

        onoff = false;

        book.SetActive(false);
        guess.SetActive(false);
        narration.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            book.SetActive(!onoff);
            guess.SetActive(!onoff);
            narration.SetActive(!onoff);

            player.enabled = onoff;
            flashLight.enabled = onoff;

            onoff = !onoff;
        }
    }
}
