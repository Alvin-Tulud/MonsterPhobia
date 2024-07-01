using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GuessMonster : MonoBehaviour
{
    public TMP_InputField input;
    public TMP_Text narration;
    string realname;
    string guessname;
    bool isright;
    public bool canguess;
    bool hasseen;


    Transform playertransform;
    private const float DETECTION_DISTANCE = 7.0f;
    private const float WAIT_SECONDS = 15.0f;


    // Start is called before the first frame update
    void Awake()
    {
        canguess = false;
        isright = false;
        hasseen = false;
    }

    // Update is called once per frame
    void Update()
    {
        playertransform = GameObject.FindGameObjectWithTag("Player").transform;
        realname = GetComponent<MonsterText>().realname;

        if (FoundEnemyOnSight() && !hasseen)
        {
            canguess = true;
            hasseen = true;
        }
        else if (!hasseen)
        {
            narration.text = "I should find it before guessing";
            input.interactable = false;
        }
    }

    public void GuessCheck()
    {
        if (canguess)
        {
            guessname = input.text;

            if (guessname == realname) 
            {
                Debug.Log("win");
                narration.text = "That sounds about right";
                isright = true;
                playertransform.GetComponent<Collider2D>().enabled = false;

                //GameObject.FindWithTag("Event").SetActive(false);
                StartCoroutine(winWait());
            }
            else
            {
                narration.text = "No I need to find more clues";
                StartCoroutine(guesswait());
            }
        }
    }


    private bool FoundEnemyOnSight()
    {
        LayerMask walls = LayerMask.GetMask("Wall");
        LayerMask enemy = LayerMask.GetMask("Enemy");

        // First, check if the player is within the radius of detection 
        Collider2D enemyNearby = Physics2D.OverlapCircle((Vector2)playertransform.position, DETECTION_DISTANCE, enemy);

        if (enemyNearby != null)
        {
            // Then, check if the player is behind a wall
            Vector2 directionOfEnemy = (Vector2)enemyNearby.transform.position - (Vector2)playertransform.position;
            RaycastHit2D wallInWay = Physics2D.Raycast((Vector2)transform.position, directionOfEnemy.normalized, DETECTION_DISTANCE, walls);

            // Returns true if there is no wall in the way, else false if there is
            return wallInWay.collider == null;
        }

        // Case in which the player is not in the radius of detection
        return false;
    }

    IEnumerator guesswait()
    {
        input.interactable = false;
        yield return new WaitForSeconds(WAIT_SECONDS);
        input.interactable = true;
    }

    IEnumerator clearNarration()
    {
        yield return new WaitForSeconds(5);
        narration.text = "";
    }

    IEnumerator winWait()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}
