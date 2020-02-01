using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightArea : MonoBehaviour
{

    [SerializeField] private GameObject[] highlightObjects;

    private void Start()
    {
    }

    void OnMouseEnter()
    {

        for (int i = 0; i < highlightObjects.Length; i++)
        {
            highlightObjects[i].GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.yellow);
            highlightObjects[i].GetComponent<Renderer>().material.SetColor("tintColor", new Color(1f, 1f, 0.4901f, 1f));
            highlightObjects[i].GetComponent<Renderer>().material.SetFloat("_Outline", 0.03f);
        }
    }

    void OnMouseExit()
    {

        for (int i = 0; i < highlightObjects.Length; i++)
        {
            highlightObjects[i].GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.black);
            highlightObjects[i].GetComponent<Renderer>().material.SetColor("tintColor", Color.white);
            highlightObjects[i].GetComponent<Renderer>().material.SetFloat("_Outline", 0.02f);
        }
    }
}
