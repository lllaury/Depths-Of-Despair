using UnityEngine;

[CreateAssetMenu(fileName = "NewScriptableObject", menuName = "ScriptableObjects/PickupObject", order = 1)]
public class PickupObject : ScriptableObject
{
    public GameObject droppedItem;
    public GameObject equippedItem;
}
