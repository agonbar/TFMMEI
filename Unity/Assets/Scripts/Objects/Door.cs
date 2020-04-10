using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    key,
    enemy,
    button
}

public class Door : Interactable
{
    [Header("Door variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public SpriteRenderer doorSpriteChild;
    public BoxCollider2D physicsCollider;
    public BoxCollider2D cueCollider;
    public BoolValue storedOpen;

    // Use this for initialization
    void Start () {
		if(storedOpen.RuntimeValue) {
            Open();
        }
	}
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(playerInRange && thisDoorType == DoorType.key)
            {
                //Does the player have a key?
                if(playerInventory.numberOfKeys > 0)
                {
                    //Remove a player key
                    playerInventory.numberOfKeys--;
                    //If so, then call the open method
                    Open();
                }
            }
        }
    }

    public void Open()
    {
        //Turn off the door's sprite renderer
        doorSprite.enabled = false;
        doorSpriteChild.enabled = false;
        //set open to true
        open = true;
        //turn off the door's box collider
        physicsCollider.enabled = false;
        cueCollider.enabled = false;
        storedOpen.RuntimeValue = open;
    }

    public void Close()
    {

    }

        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && playerInventory.numberOfKeys > 0)
        {
            context.Raise();
            playerInRange = true;
        }
    }

        private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && playerInventory.numberOfKeys > 0)
        {
            context.Raise();
            playerInRange = false;
        }
    }
}