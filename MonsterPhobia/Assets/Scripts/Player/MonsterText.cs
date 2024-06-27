using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterText : MonoBehaviour
{
    TextMeshProUGUI name_left;
    TextMeshProUGUI name_right;
    TextMeshProUGUI trait_left;
    TextMeshProUGUI trait_right;


    int pageAmount = 2; //2 sets of pages therefore 4 monsters
    int currentpage = 0;


    //need to grab values from spawner game object

    // Start is called before the first frame update
    void Awake()
    {
        name_left = GameObject.FindGameObjectWithTag("leftname").GetComponent<TextMeshProUGUI>();
        name_right = GameObject.FindGameObjectWithTag("rightname").GetComponent<TextMeshProUGUI>();
        trait_left = GameObject.FindGameObjectWithTag("lefttrait").GetComponent<TextMeshProUGUI>();
        trait_right = GameObject.FindGameObjectWithTag("righttrait").GetComponent<TextMeshProUGUI>();
    }

    public void flipleft()
    {
        if (currentpage - 1 >= 0)
        {
            currentpage--;


            //change the page's contents of left and right side
        }
    }

    public void flipright()
    {
        if (currentpage + 1 < pageAmount)
        {
            currentpage++;


            //change the page's contents of left and right side
        }
    }
}
