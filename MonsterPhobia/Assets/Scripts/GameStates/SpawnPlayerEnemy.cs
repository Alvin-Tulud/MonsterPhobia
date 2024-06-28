using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerEnemy : MonoBehaviour
{
    //spawn spots are placed in an ordered way such that opposite sides are x/2 spots from eachother
    Transform spawnpoints;
    List<Transform> spawnspots;

    public GameObject player;
    public GameObject enemy;
    // Start is called before the first frame update
    void Awake()
    {
        spawnspots = new List<Transform>();
        //grab spawn points and add all into the list
        spawnpoints = GameObject.FindWithTag("spawnpoint").transform;

        for (int i = 0; i < spawnpoints.childCount; i++)
        {
            spawnspots.Add(spawnpoints.GetChild(i));
        }


        //spawn enemy and player
        spawnBoth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnBoth()
    {
        int randSpotPlayer = Random.Range(0, spawnspots.Count);

        int SpotEnemy = (randSpotPlayer + spawnspots.Count / 2) % spawnspots.Count;

        GameObject entity;
        entity = Instantiate(player, spawnspots[randSpotPlayer]);

        entity.transform.SetParent(null);

        entity = Instantiate(enemy, spawnspots[SpotEnemy]);

        entity.transform.SetParent(null);
    }
}
