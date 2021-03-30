using UnityEngine;
using System.Collections;

public class playerSlaughtered : MonoBehaviour
{
    public static bool attacked;
    public Camera deathCam;
    public Animator anim;
    public Light deathFlahslight;
    Camera cam;
    GameObject[] playerRespawnPoints;
    GameObject currentPoint;
    int index;
    void Start()
    {
        deathFlahslight.enabled = false;
        deathCam.enabled = false;
        cam = Camera.main;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log(PlayerController.currentPlayerHealth);
            deathCam.enabled = false;
            StartCoroutine(animationer());
        }

        if (attacked)
        {
            StartCoroutine(animationer());
        }
    }

    IEnumerator animationer()
    {
        KO();     
        yield return new WaitForSeconds(2);
        deathFlahslight.intensity = Mathf.Lerp(deathFlahslight.intensity, 0f, Time.deltaTime * 2);
        yield return new WaitForSeconds(2);
        if (PlayerController.currentPlayerHealth > 0)
        {
            respawn();
            yield return new WaitForSeconds(.2f);

            anim.SetTrigger("wake");
            yield return new WaitForSeconds(1.5f);
            wake();
        }
        else
        {
            
        }
    }

    void KO()
    {
        if (PlayerController.torchOn)
            deathFlahslight.enabled = true;
        cam.enabled = false;
        deathCam.enabled = true;
        anim.SetTrigger("dead");
    }
    
    void wake()
    {
        deathCam.enabled = false;
        cam.enabled = true;
        attacked = false;
    }

    void respawn()
    {
        playerRespawnPoints = GameObject.FindGameObjectsWithTag("playerpoint");
        index = Random.Range(0, playerRespawnPoints.Length);
        currentPoint = playerRespawnPoints[index];
        this.gameObject.transform.position = currentPoint.transform.position;

        PlayerController.currentPlayerHealth -= 1;
    }
}

