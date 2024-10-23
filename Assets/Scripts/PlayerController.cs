using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private CinemachineCamera fpCam;
    private Vector3 playerVelocity;
    [SerializeField] private float playerSpeed = 2f;
    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, fpCam.transform.eulerAngles.y, transform.eulerAngles.z);

        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = transform.right * movement.x + transform.forward * movement.y;

        controller.Move(move * Time.deltaTime * playerSpeed);
    }
}
