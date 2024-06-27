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

    // Start is called before the first frame update
    void Awake()
    {
        name_left = GameObject.FindGameObjectWithTag("leftname").GetComponent<TextMeshProUGUI>();
        name_right = GameObject.FindGameObjectWithTag("rightname").GetComponent<TextMeshProUGUI>();
        trait_left = GameObject.FindGameObjectWithTag("lefttrait").GetComponent<TextMeshProUGUI>();
        trait_right = GameObject.FindGameObjectWithTag("righttrait").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
