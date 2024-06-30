using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAudio : MonoBehaviour
{
    AudioSource footstepSource;
    AudioSource sfxSource;
    public float fsDelay;

    public List<AudioClip> fsCrunch;
    public List<AudioClip> fsThud;
    public List<AudioClip> fsSquish;
    public List<AudioClip> fsDrag;
    public List<AudioClip> fsScrape;

    List<List<AudioClip>> clipLists = new List<List<AudioClip>>();
    List<AudioClip> mainClipList;

    public int listSelection;

    void Start()
    {
        footstepSource = GetComponents<AudioSource>()[0];
        sfxSource = GetComponents<AudioSource>()[1];

        clipLists.Add(fsCrunch);
        clipLists.Add(fsThud);
        clipLists.Add(fsSquish);
        clipLists.Add(fsDrag);
        clipLists.Add(fsScrape);

        listSelection = Random.Range(0, clipLists.Count);
        mainClipList = clipLists[listSelection];
        StartCoroutine(PlayFootsteps(fsDelay));


    }

    int RandomFootstepSFX()
    {
        int index = Random.Range(0, mainClipList.Count);
        return index;
    }

    IEnumerator PlayFootsteps (float delay)
    {
        while (true)
        {
            AudioClip clip = footstepSource.clip = mainClipList[RandomFootstepSFX()];
            footstepSource.clip = clip;
            footstepSource.Play();
            yield return new WaitForSeconds(delay);
        }


    }
}
