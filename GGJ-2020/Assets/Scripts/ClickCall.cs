using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;

public class ClickCallScript : MonoBehaviour
{
    //Create UI usage and management of events??
    [FMODUnity.EventRef]
    public string PlayerStateEvent = "";
    FMOD.Studio.EventInstance playerState;


    //One shot sound setup
    [FMODUnity.EventRef]
    public string itemClick = "";
    [FMODUnity.EventRef]
    public string itemUnclick = "";
    [FMODUnity.EventRef]
    public string itemPop = "";

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    //Make the item click sound
    public void ClickSound() {
        FMODUnity.RuntimeManager.PlayOneShot(itemClick, transform.position);
    }

    //Make the item unclick sound
    public void UnclickSound() {
        FMODUnity.RuntimeManager.PlayOneShot(itemUnclick, transform.position);
    }

    //Make the item pop sound
    public void PopSound() {
        FMODUnity.RuntimeManager.PlayOneShot(itemPop, transform.position);
    }
}
