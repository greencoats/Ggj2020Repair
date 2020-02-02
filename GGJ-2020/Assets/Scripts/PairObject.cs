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
    private bool pairMatch;

    private void Awake()
    {
        highlighted = false;
        pairMatch = false;
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

        pairMatch = false;

        for (int i = 0; i < pairs.Length; i++)
        {
            print(pairs[i]);
            if (pairs[i] == pair.GetPairObject())
            {
                i = pairs.Length;
                pairMatch = true;
                GameManager.Instance.AddPair();
            }
        }

        if (!pairMatch)
        {
            StartCoroutine(UnpopPair());
        }


        PairItems(pair);
    }

    public Pair GetPairObject()
    {
        return thisObject;
    }

    public void PairItems(PairObject pair)
    {
        //Play audio---------------------
        gameObject.GetComponent<ClickCall>().ClickSound();
        pairedObject = pair;
        pairedObject.SetPair(this);
        pair.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    public void SetPair(PairObject pair = null)
    {
        if (pair)
        {
            pairedObject = pair;
        } else
        {
            pairedObject = null;
            if (pairMatch)
            {
                GameManager.Instance.RemovePair();
            }
        }
    }

    public void UnpairItems()
    {
        if (pairedObject)
        {
            //Play audio-------------------
            gameObject.GetComponent<ClickCall>().PopSound();
            pairedObject.SetPair(null);
            pairedObject = null;
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }
    }

    void OnMouseEnter()
    {
        if (GameManager.Instance.currentObject != this) {
            GetComponent<Renderer>().material.SetColor("tintColor", new Color(1f, 1f, 0.4901f, 1f));
            highlighted = true;
        }
    }
    void OnMouseExit()
    {
        GetComponent<Renderer>().material.SetColor("tintColor", Color.white);
        highlighted = false;
    }

    IEnumerator UnpopPair()
    {
        yield return new WaitForSecondsRealtime(60);

        UnpairItems();
    }
}
