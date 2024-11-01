using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerInteraction : MonoBehaviour
{
    private InputManager inputManager;
    public GameObject equippedItem = null;
    [SerializeField] private TextMeshProUGUI pickupText;

    private Collider currentInteractable;

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    public void SetEquippedItem(GameObject newItem)
    {
        if (equippedItem != null)
        {
            // Unequip current item
            PickupItemScript pis = equippedItem.GetComponent<PickupItemScript>();
            pis.DropItem();
            Destroy(equippedItem);
        }

        GameObject obj = Instantiate(newItem);
        equippedItem = obj;
        equippedItem.transform.parent = gameObject.transform;
        equippedItem.transform.localPosition = new Vector3(0.45f, 0.35f, 0.72f);
        pickupText.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickupItem"))
        {
            // Display the text
            pickupText.enabled = true;
            currentInteractable = other;
        }

        if (other.gameObject.CompareTag("Machine"))
        {
            currentInteractable = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == currentInteractable)
        {
            // Disable the text
            if (other.gameObject.CompareTag("PickupItem"))
            {
                pickupText.enabled = false;
            }

            currentInteractable = null;
        }
    }

    private void Update()
    {
        if (currentInteractable != null)
        {
            if (currentInteractable.gameObject.CompareTag("Machine"))
            {
                AbstractMachine machine = currentInteractable.GetComponent<AbstractMachine>();
                if (machine != null && machine.IsMachineBroken())
                {
                    if (inputManager.GetPressedInteract() && machine.CheckCorrectTool(equippedItem.name))
                    {
                        Debug.Log("Tried to start fixing machine");
                        machine.FixMachine();
                    }
                    if (inputManager.GetReleasedInteract())
                    {
                        Debug.Log("Stop fixing machine");
                        machine.StopRepairing();
                    }
                }
            }

            if (currentInteractable.gameObject.CompareTag("PickupItem") && inputManager.GetPlayerPressedEquip())
            {
                PickupItemScript pis = currentInteractable.gameObject.GetComponent<PickupItemScript>();
                SetEquippedItem(pis.GetEquippedVersion());
                Destroy(currentInteractable.gameObject);
                currentInteractable = null; // Clear currentInteractable after picking up
            }
        }

        if (inputManager.GetPressedInteract() && equippedItem != null)
        {
            // Interact with equipped item
            try
            {
                IInteractableItem itemScript = equippedItem.GetComponent<IInteractableItem>();
                itemScript.UseItem();
            }
            catch (Exception) { }
        }

        if (inputManager.GetPlayerPressedDrop() && equippedItem != null)
        {
            // Unequip current item
            equippedItem.GetComponent<PickupItemScript>().DropItem();
            Destroy(equippedItem);
        }
    }
}
