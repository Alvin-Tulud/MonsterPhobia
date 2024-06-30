using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    AudioSource footstepSource;
    public List<AudioClip> fsPlayer;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        footstepSource = GetComponent<AudioSource>();
        StartCoroutine(PlayFootsteps(.5f));
    }

    private void Update()
    {
    }


    int RandomFootstepSFX()
    {
        int index = Random.Range(0, fsPlayer.Count);
        return index;
    }

    bool GetIsMoving()
    {
        if (rb.velocity != Vector2.zero)
        {
            Debug.Log("SAM");
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator PlayFootsteps(float delay)
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                AudioClip clip = footstepSource.clip = fsPlayer[RandomFootstepSFX()];
                footstepSource.clip = clip;
                footstepSource.Play();
                yield return new WaitForSeconds(delay);
            }
            else
            {
                yield return null;
            }

        }


    }

}
