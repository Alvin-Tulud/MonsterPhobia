using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuessMonster : MonoBehaviour
{
    string realname;
    string guessname;
    bool isright;
    public bool canguess;


    // Start is called before the first frame update
    void Start()
    {
        realname = GetComponent<MonsterText>().realname;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GuessCheck()
    {

        if (canguess && guessname == realname)
        {
            Debug.Log("win");
        }
    }
}
