using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpriteGlow;

public class PairObject : MonoBehaviour
{
    [SerializeField] private Pair[] pairs;
    [SerializeField] private Pair thisObject;
    private PairObject pairedObject;
    private bool highlighted;
    private float clickXMod = 10;
    private float clickYMod = 0;
    private float zMod = 0.1f; // make one object slightly in front of the other

    private void Awake()
    {
        highlighted = false;
    }

    private void Update()
    {
        if (highlighted && Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.ClickObject(this);
        }
    }

    public void Match(PairObject pair)
    {
        for (int i = 0; i < pairs.Length; i++)
        {
            if (pairs[i] == pair.GetPairObject())
            {
                PairItems(pair);
                i = pairs.Length;
            }
        }
    }

    public Pair GetPairObject()
    {
        return thisObject;
    }

    public void PairItems(PairObject pair)
    {
        pairedObject = pair;
        pairedObject.SetPair(this);
        pair.transform.position = new Vector3(transform.position.x + 0.1f,
                transform.position.y + (GetComponent<Renderer>().material.GetFloat("_ScaleX") < pair.GetComponent<Renderer>().material.GetFloat("_ScaleX") ? zMod : -zMod),
                transform.position.z + ((GetComponent<Renderer>().material.GetFloat("_ScaleX")  - pair.GetComponent<Renderer>().material.GetFloat("_ScaleX")) * clickYMod));
        GameManager.Instance.AddPair();
    }

    public void SetPair(PairObject pair = null)
    {
        if (pair)
        {
            pairedObject = pair;
        } else
        {
            UnpairItems();
        }
    }

    public void UnpairItems()
    {
        pairedObject.SetPair(null);
        GameManager.Instance.RemovePair();
        pairedObject = null;
    }

    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.SetColor("tintColor", new Color(1f, 1f, 0.4901f, 1f));
        highlighted = true;
    }
    void OnMouseExit()
    {
        GetComponent<Renderer>().material.SetColor("tintColor", Color.white);
        highlighted = false;
    }
}
