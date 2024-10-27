using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance {
        get {
            return _instance;
        }
    }

    private PlayerControls playerControls;

    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this;

        playerControls = new PlayerControls();
    }

    private void OnEnable() {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetPlayerMovement() {
        return playerControls.Player.Movement.ReadValue<Vector2>();   
    }

    public bool GetPlayerPressedEquip() {
        return playerControls.Player.Equip.triggered;
    }

    public bool GetPlayerPressedDrop()
    {
        return playerControls.Player.Drop.triggered;
    }

    public bool GetPressedInteract() {
        return playerControls.Player.Interact.triggered;
    }
}
