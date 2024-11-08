using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private CinemachineCamera fpCam;
    [SerializeField] private float playerSpeed = 2f;
    private InputManager inputManager;
    [SerializeField] private float sprintMultiplier = 1.5f;
    private PlayerStats ps;

    private bool isSprinting = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        ps = PlayerStats.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, fpCam.transform.eulerAngles.y, transform.eulerAngles.z);

        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = transform.right * movement.x + transform.forward * movement.y;

        if (inputManager.GetHoldingSprint() && ps.HasStamina())
        {
            controller.Move(move * Time.deltaTime * playerSpeed * sprintMultiplier);
            ps.SubtractStamina();
            isSprinting = true;
        }
        else
        {
            controller.Move(move * Time.deltaTime * playerSpeed);
            isSprinting = false;
        }

        // Regenerate stamina only when not sprinting
        if (!isSprinting)
        {
            ps.AddStamina();
        }
    }
}
