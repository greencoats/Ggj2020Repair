using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightArea : MonoBehaviour
{

    [SerializeField] private GameObject[] highlightObjects;
    [SerializeField] private GameObject camera;
    private bool highlighted;

    private void Awake()
    {
        highlighted = false;
    }

    private void Update()
    {
        if (highlighted && Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<BoxCollider>().enabled = false;
            camera.SetActive(true);
            GameManager.Instance.MainCamOff(gameObject, camera);
        }
    }

    void OnMouseEnter()
    {
        //highlight all objects in the area
        for (int i = 0; i < highlightObjects.Length; i++)
        {
            highlightObjects[i].GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.yellow);
            highlightObjects[i].GetComponent<Renderer>().material.SetColor("tintColor", new Color(1f, 1f, 0.4901f, 1f));
            highlightObjects[i].GetComponent<Renderer>().material.SetFloat("_Outline", 0.03f);
            highlighted = true;
        }
    }

    void OnMouseExit()
    {
        //unhighlight all objects in the area
        for (int i = 0; i < highlightObjects.Length; i++)
        {
            highlightObjects[i].GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.black);
            highlightObjects[i].GetComponent<Renderer>().material.SetColor("tintColor", Color.white);
            highlightObjects[i].GetComponent<Renderer>().material.SetFloat("_Outline", 0.02f);
            highlighted = false;
        }
    }
}
