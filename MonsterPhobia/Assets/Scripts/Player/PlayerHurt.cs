using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHurt : MonoBehaviour
{
    public bool isHit;

    public GameObject player;
    public GameObject mainCamera;
    Vector3 cameraEndPosition;
    Vector3 cameraStartPosition;
    Vector3 cameraCurrentPosition;
    float cameraMax;
    float cameraMin;
    float cameraCurrentSize;
    float time;
    float speed;
    bool doneZooming;
    bool gotPosition;
    public AudioClip playerDeathSFX;
    // Start is called before the first frame update
    void Start()
    {
        isHit = false;

        player = GameObject.FindWithTag("Player");
        mainCamera = GameObject.FindWithTag("MainCamera");
        cameraMax = mainCamera.GetComponent<Camera>().orthographicSize;
        cameraMin = 1f;
        time = 0f;
        speed = 50f;
        doneZooming = false;
        gotPosition = false;
    }

    private void FixedUpdate()
    {
        if (isHit)
        {
            //zoom camera into player
            if (!gotPosition)
            {
                cameraEndPosition = player.transform.position;
                cameraStartPosition = mainCamera.transform.position;


                GetComponent<PlayerMove>().enabled = false;
                GameObject.FindWithTag("GUI").SetActive(false);
                GetComponent<PlayerAudio>().enabled = false;
                GetComponent<Rigidbody2D>().isKinematic = true;
                GameObject.FindWithTag("FlashLight").SetActive(false);


                gotPosition = true;
            }


            if (!doneZooming)
            {
                if (mainCamera.GetComponent<Camera>().orthographicSize != cameraMin)
                {
                    cameraCurrentSize = Mathf.Lerp(cameraMax, cameraMin, time / speed);

                    mainCamera.GetComponent<Camera>().orthographicSize = cameraCurrentSize;


                    cameraCurrentPosition.x = Mathf.Lerp(cameraStartPosition.x, cameraEndPosition.x, time / speed);
                    cameraCurrentPosition.y = Mathf.Lerp(cameraStartPosition.y, cameraEndPosition.y, time / speed);
                    cameraCurrentPosition.z = -10f;

                    mainCamera.transform.position = cameraCurrentPosition;

                    time++;
                }

                else
                {
                    time = 0f;

                    mainCamera.GetComponent<Camera>().orthographicSize = cameraMin;

                    mainCamera.transform.position = new Vector3(cameraEndPosition.x, cameraEndPosition.y, -10f);

                    doneZooming = true;
                }
            }



            if (cameraCurrentSize == cameraMin)
            {

                //if (SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
                //{
                //    SceneManager.LoadScene(0);
                //}
                //else
                //{
                //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                //}
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            isHit = true;
            gameObject.GetComponent<AudioSource>().PlayOneShot(playerDeathSFX,2f);

        }
    }
}
