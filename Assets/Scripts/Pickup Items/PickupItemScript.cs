using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItemScript : MonoBehaviour
{
    [SerializeField] PickupObject pickup;
    [SerializeField] bool isDropped = false;
    private InputManager inputManager;
    private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.Instance;
        playerStats = PlayerStats.Instance;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            if (inputManager.GetPlayerPressedEquip()) {
                playerStats.SetEquippedItem(isDropped ? pickup.equippedItem : pickup.droppedItem);
                Destroy(gameObject);
            }
        }
    }
}
