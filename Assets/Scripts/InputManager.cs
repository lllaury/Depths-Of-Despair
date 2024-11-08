using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    private bool wasInteractPressedLastFrame = false; // Not serialized as it doesn’t need to be set in the Inspector

    public static InputManager Instance
    {
        get { return _instance; }
    }

    private PlayerControls playerControls;

    // Initialize PlayerControls in Awake
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Keeps the instance alive across scenes

            playerControls = new PlayerControls();
            playerControls.Enable();
        }
    }

    private void OnEnable()
    {
        playerControls?.Enable(); // Ensure playerControls is not null
    }

    private void OnDisable()
    {
        playerControls?.Disable(); // Ensure playerControls is not null
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }

    public bool GetPlayerPressedEquip()
    {
        return playerControls.Player.Equip.triggered;
    }

    public bool GetPlayerPressedDrop()
    {
        return playerControls.Player.Drop.triggered;
    }

    public bool GetPressedInteract()
    {
        if (playerControls.Player.Interact.triggered) Debug.Log("Interact was pressed");
        return playerControls.Player.Interact.triggered;
    }

    public bool GetReleasedInteract()
    {
        bool isInteractPressed = playerControls.Player.Interact.ReadValue<float>() > 0f;
        bool isReleased = !isInteractPressed && wasInteractPressedLastFrame;
        if (isReleased) Debug.Log("Interact was released");
        return isReleased;
    }

    public bool GetHoldingInteract()
    {
        return playerControls.Player.Interact.ReadValue<float>() > 0f;
    }

    public bool GetHoldingSprint() {
        return playerControls.Player.Sprint.ReadValue<float>() > 0f;
    }
    private void LateUpdate()
    {
        // Update the state of the interact button to check for released state in the next frame
        wasInteractPressedLastFrame = playerControls.Player.Interact.ReadValue<float>() > 0f;
    }
}
