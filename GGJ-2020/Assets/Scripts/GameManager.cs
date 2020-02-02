using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //Zooming variables
    private bool inZoom;
    private Camera mainCam;
    private GameObject mainZoom;
    private GameObject currentZoomCam;

    //pair tracking
    private int correctPairs;

    //player variables
    private GameObject player;

    //singleton variable
    public static GameManager Instance;

    //Blur Variables
    [SerializeField] private Canvas[] blurCanvas;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        } else
        {
            Destroy(this);
        }

        correctPairs = 0;
        inZoom = false;
        mainCam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (inZoom && Input.GetKeyDown(KeyCode.E))
        {
            MainCamOn();
            mainZoom.GetComponent<BoxCollider>().enabled = true;
            mainZoom = null;
        }
        StartCoroutine(ShrinkCanvas(1));
    }

    public void AddPair()
    {
        correctPairs++;
        if(correctPairs == 1) {
            StartCoroutine(ShrinkCanvas(1));
        }
    }

    public void RemovePair()
    {
        correctPairs--;
    }

    public void MainCamOff(GameObject zoomBox, GameObject currentCam)
    {
        player.SetActive(false);
        mainZoom = zoomBox;
        currentZoomCam = currentCam;
        FreeClickMode();
        StartCoroutine(SwitchZoom());
    }

    public void MainCamOn()
    {
        currentZoomCam.SetActive(false);
        currentZoomCam = null;
        player.SetActive(true);
        LockedClickMode();
        StartCoroutine(SwitchZoom());
    }

    IEnumerator SwitchZoom()
    {
        yield return new WaitForSeconds(0.1f);
        inZoom = !inZoom;
    }

    private void FreeClickMode()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void LockedClickMode()
    {
        Cursor.visible = false;
    }

    //Shrink blur canvas
    private IEnumerator ShrinkCanvas(int canvasNo) {

        Canvas currentCanvas = blurCanvas[canvasNo];
        float timer = 0.0f;

        while (timer < 5) {
            timer += Time.deltaTime;
            float t = timer / 5;
            //smoother step algorithm
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            currentCanvas.transform.localScale = Vector3.Lerp(new Vector3(1, 1, 1), new Vector3(0, 0, 0), t);
            yield return null;
        }
    }
}
