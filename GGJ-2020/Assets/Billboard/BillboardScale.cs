using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Billboard Scale
//20/10/19
//Oliver Taylor
//Version 1.0
//Gets scale of object

public class BillboardScale : MonoBehaviour
{
    public Vector4 objScale;


    private void Awake() {

        //Get the scale of the object the billboard shader is on
        objScale = transform.localScale;
        Shader.SetGlobalFloat("_Scale X", objScale.x);
        Shader.SetGlobalFloat("_Scale Y", objScale.y); 
        //transform.
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
