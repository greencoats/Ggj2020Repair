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
    [SerializeField] private int correctPairs;
    private PairObject currentObject;
    [SerializeField] private Transform objectHold;

    //player variables
    private GameObject player;

    //singleton variable
    public static GameManager Instance;

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
        currentObject = null;
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
    }

    public void AddPair()
    {
        correctPairs++;
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
        MoveSpriteToPlayer();
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

    public void ClickObject(PairObject newObject)
    {
        if (!currentObject)
        {
            currentObject = newObject;
            currentObject.UnpairItems();
            currentObject.transform.SetParent(currentZoomCam.GetComponentInChildren<SpritePos>().transform);
            currentObject.transform.localPosition = new Vector3(0, 0, 0);
        } else
        {
            newObject.UnpairItems();
            currentObject.transform.parent = null;
            newObject.PairItems(currentObject);
            currentObject = null;
        }
    }

    private void MoveSpriteToPlayer()
    {
        if (currentObject)
        {
            currentObject.transform.SetParent(objectHold);
            currentObject.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}
