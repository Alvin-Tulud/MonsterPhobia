using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAudio : MonoBehaviour
{
    AudioSource footstepSource;
    AudioSource sfxSource;
    public float fsDelay = .5f;

    public List<AudioClip> footsteps;
    // Start is called before the first frame update
    void Start()
    {
        footstepSource = GetComponents<AudioSource>()[0];
        sfxSource = GetComponents<AudioSource>()[1];
        StartCoroutine(PlayFootsteps(fsDelay));

    }

    private void Awake()
    {

    }

    int RandomFootstepSFX()
    {
        int index = Random.Range(0, footsteps.Count);
        return index;
    }

    IEnumerator PlayFootsteps (float delay)
    {
        while (true)
        {
            //Debug.Log("FOOTSTEP");
            AudioClip clip = footstepSource.clip = footsteps[RandomFootstepSFX()];
            footstepSource.clip = clip;
            footstepSource.Play();
            yield return new WaitForSeconds(delay);
        }


    }
}
