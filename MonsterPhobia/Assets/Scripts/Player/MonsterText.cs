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
    private string realname;
    private List<string> monsternames = new List<string>();

    bool hasaddedcontent = false;

    // Start is called before the first frame update
    void Awake()
    {

    }

    private void Update()
    {
        if (!hasaddedcontent)
        {
            MonsterAttributes enemy = GameObject.FindWithTag("Enemy").GetComponent<MonsterBehaviour>().MAttributes;
            realname = randomName();
            monsternames.Add(realname);
            monsters.Add(realname, enemy);


            for (int i = 0; i < 3; i++)
            {
                enemy = new MonsterAttributes((MonsterType)Random.Range(1, 3));
                string name = randomName();
                monsternames.Add(name);
                monsters.Add(name, enemy);
            }

            System.Random rng = new System.Random();

            monsternames.OrderBy(x => rng.Next()).ToList();


            hasaddedcontent = true;
        }

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

        List<string> names = new List<string>() { "Knauk", "Crang", "Zuc", "Brik", "Zremkes", "Clebrag", "Ebbik", "Zrozrek", "Qheikarg", 
            "Ezdiad", "Stres", "Zasz", "Bhrobhruh", "Crinszi", "Akas", "Vrutte", "Zrilgis", "Umqeisz", "Scisz", "Dusz", "Xon", "Vusz", "Ekziz", "Eemgam",
            "Ghrezsias", "Snurdi", "Bhrulki", "Snagrus" };

        name = names[Random.Range(0, names.Count)];

        return name;
    }

    string interprettype(MonsterAttributes attributes)
    {
        string type;

        switch (attributes.MType)
        {
            case MonsterType.Axylotyl:
                type = "This creature likes to Wander";
                break;
            case MonsterType.Bill:
                type = "They are around you Stalking";
                break;
            case MonsterType.Garry:
                type = "This creature doesn't like us in its Territory";
                break;
            case MonsterType.None:
            default:
                type = "fuck you";
                break;
        }

        return type;
    }
}
