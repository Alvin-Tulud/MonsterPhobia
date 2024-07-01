using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialBook : MonoBehaviour
{
    TextMeshProUGUI name_left;
    TextMeshProUGUI name_right;
    TextMeshProUGUI trait_left;
    TextMeshProUGUI trait_right;


    int pageAmount = 4; //2 sets of pages therefore 4 monsters
    int currentpage = 2;


    private List<string> title = new List<string>();
    private List<string> text = new List<string>();


    public GameObject playbutton;

    // Start is called before the first frame update
    void Start()
    {
        playbutton.SetActive(false);

        title.Add("Movement");
        text.Add(" - Use WASD to Move around");


        title.Add("FlashLight");
        text.Add(" - Use your Mouse to Move around the Flashlight");


        title.Add("Book");
        text.Add(" - Press Space to Open and Close the Book\n - Under the Book is Hints");


        title.Add("Guessing");
        text.Add(" - You can Type in the Area marked Guess Here\n - You Win if you guess Correct");

    }

    // Update is called once per frame
    void Update()
    {
        name_left = GameObject.FindGameObjectWithTag("leftname").GetComponent<TextMeshProUGUI>();
        name_right = GameObject.FindGameObjectWithTag("rightname").GetComponent<TextMeshProUGUI>();
        trait_left = GameObject.FindGameObjectWithTag("lefttrait").GetComponent<TextMeshProUGUI>();
        trait_right = GameObject.FindGameObjectWithTag("righttrait").GetComponent<TextMeshProUGUI>();



        int pageopenleft = currentpage - 2;
        int pageopenright = currentpage - 1;


        name_left.text = title[pageopenleft];
        name_right.text = title[pageopenright];

        trait_left.text = text[pageopenleft];
        trait_right.text = text[pageopenright];



        if (currentpage == pageAmount)
        {
            StartCoroutine(wait());
        }
    }

    public void flipleft()
    {
        if (currentpage - 2 > 0)
        {
            currentpage -= 2;
        }
    }

    public void flipright()
    {
        if (currentpage + 2 <= pageAmount)
        {
            currentpage += 2;
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(3);
        playbutton.SetActive(true);
    }
}
