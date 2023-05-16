using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInteract : MonoBehaviour
{
    [Tooltip("the interactible object that is currently being looked at")]
    public InteractibleSpot currentInteract;
    [Tooltip("the other interactible objects you are in contact with")]
    public List<InteractibleSpot> contactSpots;
    [Tooltip("if the button for interactions is being pressed")]
    public bool interactPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        contactSpots = new List<InteractibleSpot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(CallbackContext ctx){
        if(ctx.ReadValue<float>() <= 0.5f){
            interactPressed = false;
            return;
        }
        if(interactPressed){
            return;
        }
        interactPressed = true;
        if(currentInteract){
            currentInteract.Interact();
        }
    }

    private void OnTriggerEnter(Collider col) {
        
        InteractibleSpot ins = col.gameObject.GetComponent<InteractibleSpot>();
        if(ins == null){
            return;
        }
        Debug.Log("Logging interactible collision");
        if(currentInteract){
            if(contactSpots.Contains(ins)){
                return;
            }
            contactSpots.Add(ins);
        }
        else{
            currentInteract = ins;
            currentInteract.SetCurrent(true);
        }
    }

    private void OnTriggerExit(Collider col) {
        InteractibleSpot ins = col.gameObject.GetComponent<InteractibleSpot>();
        if(!ins){
            return;
        }
        Debug.Log("leaving interactible collision");
        if(currentInteract == ins){
            currentInteract.SetCurrent(false);
            if(contactSpots.Count == 0){
                currentInteract = null;
                return;
            }
            currentInteract = contactSpots[0];
            currentInteract.SetCurrent(true);
            contactSpots.RemoveAt(0);
        }
        else if(contactSpots.Contains(ins)){
            contactSpots.Remove(ins);
        }
    }
}
