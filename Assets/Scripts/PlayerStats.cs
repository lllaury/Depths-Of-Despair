using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton cuz I'm bad at programming
public class PlayerStats : MonoBehaviour
{
    private static PlayerStats _instance;
    private GameObject equippedItem = null;

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

    }

    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null && _instance != this) Destroy(gameObject);
        else _instance = this;
    }
}
