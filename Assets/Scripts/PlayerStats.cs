using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

// Singleton cuz I'm bad at programming
public class PlayerStats : MonoBehaviour
{
    private static PlayerStats _instance;
    private GameObject equippedItem = null;
    [SerializeField] private TextMeshProUGUI pickupText;
    private InputManager inputManager;

    public static PlayerStats Instance {
        get {
            return _instance;
        }
    }

    public void SetEquippedItem(GameObject newItem) {
        if (equippedItem != null) Destroy(equippedItem);

        GameObject obj = Instantiate(newItem);
        equippedItem = obj;
        equippedItem.transform.parent = gameObject.transform;
        equippedItem.transform.localPosition = new Vector3(0.45f, 0.35f, 0.72f);
        pickupText.enabled = false;

    }

    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null && _instance != this) Destroy(gameObject);
        else _instance = this;
    }

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Hitting some trigger.");
        print(other.gameObject.tag);

        if (other.gameObject.CompareTag("PickupItem"))
        {
            // Display the text
            pickupText.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PickupItem"))
        {
            // Bunch of problems if pickups overlap, so worry about that later
            pickupText.enabled = false;
        }
    }

    // Interactions, when press button use item if usable

    private void Update()
    {
        if (inputManager.GetPressedInteract()) {
            // Interact with equipped item
            // Machines will handle it from their side
            try
            {
                if (equippedItem != null)
                {
                    IInteractableItem itemScript = equippedItem.GetComponent<IInteractableItem>();
                    itemScript.UseItem();
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Item was not interactable: " + e.Message);
            }

        }
    }
}
