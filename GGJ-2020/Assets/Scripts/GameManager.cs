using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //Pairs values
    private int correctPairs;

    [SerializeField] PairObject[] pairs;


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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            pairs[0].Match(pairs[1]);
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
}
