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
    
    public List<AudioClip> sfxScreech;

    List<List<AudioClip>> clipLists = new List<List<AudioClip>>();
    List<AudioClip> mainFSClipList;

    public int listSelection;

    bool initialAggro = true;


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
        mainFSClipList = clipLists[listSelection];
        StartCoroutine(PlayFootsteps(fsDelay));
        StartCoroutine(MonsterScreench());

    }


    AudioClip RandomSFX(List<AudioClip> clipList)
    {
        int index = Random.Range(0, clipList.Count);
        return clipList[index];
    }

    IEnumerator PlayFootsteps (float delay)
    {
        while (true)
        {
            AudioClip clip = RandomSFX(mainFSClipList);
            footstepSource.clip = clip;
            footstepSource.Play();
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator MonsterScreench()
    {
        while (true)
        {
            if (gameObject.GetComponent<MonsterBehaviour>().GetAggroState())
            {
                if (initialAggro)
                {
                    sfxSource.clip = RandomSFX(sfxScreech);
                    sfxSource.Play();
                    initialAggro = false;

                }
                float rand = Random.Range(0.0f, 1.0f);
                if (rand <= 0.80f)
                {
                    sfxSource.clip = RandomSFX(sfxScreech);
                    sfxSource.Play();
                }

                yield return new WaitForSeconds(Random.Range(6f, 9.5f));

            }
            else
            {
                if (!initialAggro)
                {
                    initialAggro = true;
                }
                yield return null;
            }

        }


    }
}
