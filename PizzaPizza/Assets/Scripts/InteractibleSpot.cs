using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleSpot : MonoBehaviour
{
    [Tooltip("interact popup ui")]
    public GameObject popupUI;
    [Tooltip("if the player is currently touching this object")]
    private bool interacting = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrent(bool thisActive) {
        if(popupUI){
            popupUI.SetActive(thisActive);
        }
        interacting = thisActive;
        
    }

    public virtual void Interact(){
        if(interacting){
            Debug.LogWarning("interacted with an object, but it had no interaction set! be sure you aren't using the default script, and that your subclass has overridden the \"Interact\" method");
        }
    }
}
