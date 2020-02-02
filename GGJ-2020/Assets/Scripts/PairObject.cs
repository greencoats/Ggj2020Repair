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
    private float clickYMod = 5;
    private float zMod = 1; // make one object slightly in front of the other

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
        pair.transform.position = new Vector3(transform.position.x + 0.2f, 
                transform.position.y - ((transform.localScale.y - pair.transform.localScale.y) * clickYMod), 
                transform.position.z + (transform.localScale.x > pair.transform.localScale.x? zMod : -1));
        GameManager.Instance.AddPair();
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
