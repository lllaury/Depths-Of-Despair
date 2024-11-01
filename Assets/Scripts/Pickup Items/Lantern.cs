using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour, IInteractableItem
{
    public static bool lanternOn = false;
    [SerializeField] private Material offMaterial; 
    [SerializeField] private Material onMaterial;
    private MeshRenderer meshRenderer;
    private new Light light;

    public void UseItem() {
        // Turn the lantern on or off
        List<Material> mats = new List<Material>();
        meshRenderer.GetMaterials(mats);

        if (lanternOn) mats[1] = offMaterial;
        else mats[1] = onMaterial;

        meshRenderer.SetMaterials(mats);
        lanternOn = !lanternOn;
        light.enabled = lanternOn;
    }

    // Start is called before the first frame update
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        light = GetComponent<Light>();
        lanternOn = !lanternOn;
        UseItem();
    }
}
