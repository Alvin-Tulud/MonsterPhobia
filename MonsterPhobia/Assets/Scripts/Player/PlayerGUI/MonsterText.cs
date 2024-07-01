using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class MonsterText : MonoBehaviour
{
    TextMeshProUGUI name_left;
    TextMeshProUGUI name_right;
    TextMeshProUGUI trait_left;
    TextMeshProUGUI trait_right;


    int pageAmount = 4; //2 sets of pages therefore 4 monsters
    int currentpage = 2;




    //need to grab values from spawner game object
    private Dictionary<string, MonsterAttributes> monsters = new Dictionary<string, MonsterAttributes>();
    public string realname;
    private List<string> monsternames = new List<string>();
    private List<int> monstersounds = new List<int>();


    List<string> names = new List<string>() { "Knauk", "Crang", "Zuc", "Brik", "Zremkes", "Clebrag", "Ebbik", "Zrozrek", "Qheikarg",
            "Ezdiad", "Stres", "Zasz", "Bhrobhruh", "Crinszi", "Akas", "Vrutte", "Zrilgis", "Umqeisz", "Scisz", "Dusz", "Xon", "Vusz", "Ekziz", "Eemgam",
            "Ghrezsias", "Snurdi", "Bhrulki", "Snagrus" };


    bool hasaddedcontent = false;

    // Start is called before the first frame update
    void Awake()
    {

    }

    private void Update()
    {
        if (!hasaddedcontent)
        {
            //add in real monster with real audio attatched
            GameObject monster = GameObject.FindWithTag("Enemy");
            MonsterAttributes monstertrait = monster.GetComponent<MonsterBehaviour>().MAttributes;
            realname = randomName();
            monsternames.Add(realname);
            monsters.Add(realname, monstertrait);



            //add in other random monsters
            for (int i = 0; i < 3; i++)
            {
                monstertrait = new MonsterAttributes((MonsterType)Random.Range(1, 4));

                string name = randomName();
                monsternames.Add(name);
                monsters.Add(name, monstertrait);
            }



            //place the real name at a random spot in the list
            int randomSpot = Random.Range(0, monsternames.Count);
            string temp = monsternames[randomSpot];
            monsternames[randomSpot] = realname;
            monsternames[0] = temp;
            


            //add in aduio to the correct spots for the monsters
            for (int i = 0; i < monsternames.Count; i++)
            {
                if (monsternames[i] == realname)
                {
                    //add audio trait
                    monstersounds.Add(monster.GetComponent<MonsterAudio>().listSelection);
                }
                else
                {
                    //add random audio
                    monstersounds.Add(Random.Range(0,5));
                }
            }


            hasaddedcontent = true;
        }


        //for page turning and adding text to it
        if (GameObject.FindGameObjectWithTag("leftname") != null)
        {
            name_left = GameObject.FindGameObjectWithTag("leftname").GetComponent<TextMeshProUGUI>();
            name_right = GameObject.FindGameObjectWithTag("rightname").GetComponent<TextMeshProUGUI>();
            trait_left = GameObject.FindGameObjectWithTag("lefttrait").GetComponent<TextMeshProUGUI>();
            trait_right = GameObject.FindGameObjectWithTag("righttrait").GetComponent<TextMeshProUGUI>();

            

            int pageopenleft = currentpage - 2;
            int pageopenright = currentpage - 1;


            name_left.text = monsternames[pageopenleft];
            name_right.text = monsternames[pageopenright];


            MonsterAttributes leftMA;
            MonsterAttributes rightMA;

            monsters.TryGetValue(monsternames[pageopenleft], out leftMA);
            monsters.TryGetValue(monsternames[pageopenright], out rightMA);

            trait_left.text = interprettype(leftMA);
            trait_right.text = interprettype(rightMA);

            trait_left.text += "\n" + interpretsound(monstersounds[pageopenleft]);
            trait_right.text += "\n" + interpretsound(monstersounds[pageopenright]);
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

    string randomName() //https://www.fantasynamegenerators.com/kobold-names.php
    {
        string name;

        name = names[Random.Range(0, names.Count)];

        names.Remove(name);

        return name;
    }

    string interprettype(MonsterAttributes attributes)
    {
        string type;

        switch (attributes.MType)
        {
            case MonsterType.Axylotyl:
                type = " - Likes to Wander";
                break;
            case MonsterType.Bill:
                type = " - They are around you Stalking";
                break;
            case MonsterType.Garry:
                type = " - Doesn't like us in its Territory";
                break;
            case MonsterType.None:
            default:
                type = "fuck you";
                break;
        }

        return type;
    }

    string interpretsound(int clip)
    {
        string sound;

        switch(clip)
        {
            case 0:
                sound = " - Sounds Crunchy";
                break;
            case 1:
                sound = " - Has a Heavy Step";
                break;
            case 2:
                sound = " - really Slimy and Wet";
                break;
            case 3:
                sound = " - Drags its limbs across the floor";
                break;
            case 4:
                sound = " - has long claws Scraping";
                break;
            default:
                sound = "fuck you";
                break;
        }

        return sound;
    }
}


