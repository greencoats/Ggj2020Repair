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

        bool gotPair = false;

        for (int i = 0; i < pairs.Length; i++)
        {
            if (pairs[i] == pair.GetPairObject())
            {
                i = pairs.Length;
                gotPair = true;
                GameManager.Instance.AddPair();
            }
        }

        if (!gotPair)
        {
            
        }


        PairItems(pair);
    }

    public Pair GetPairObject()
    {
        return thisObject;
    }

    public void PairItems(PairObject pair)
    {
        pairedObject = pair;
        pairedObject.SetPair(this);
        pair.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameManager.Instance.AddPair();
    }

    public void SetPair(PairObject pair = null)
    {
        if (pair)
        {
            pairedObject = pair;
        } else
        {
            pairedObject = null;
            GameManager.Instance.RemovePair();
        }
    }

    public void UnpairItems()
    {
        if (pairedObject)
        {
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

    }
}
