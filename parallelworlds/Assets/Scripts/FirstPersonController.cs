using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 3f;


    [Header("Look Sensitivity")]
    [SerializeField] private float mouseSens = 2f;
    [SerializeField] private float upDownRange = 80f;
    [SerializeField] private float sprintMultiplier = 2f;
    

    [Header("Inputs Customisation")]
    [SerializeField] private string horizontalMoveInput = "Horizontal";
    [SerializeField] private string verticalMoveInput = "Vertical";
    [SerializeField] private string mouseXInput = "Mouse X";
    [SerializeField] private string mouseYInput = "Mouse Y";
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;


    [Header("Footstep Sounds")]
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private float walkStepInterval = 0.7f;
    [SerializeField] private float sprintStepInterval = 0.5f;
    [SerializeField] private float velocityThreshHold = 2f;

    private int LastPlayedIndex;
    private bool isMoving;
    private float nextStepTime;
    private Camera m_Camera;
    private float verticalRotation;

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        m_Camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleFootSteps();

    }

    private void HandleMovement()
    {
        float verticalInput = Input.GetAxis(verticalMoveInput);
        float horizontalInput = Input.GetAxis(horizontalMoveInput);
        float speedMultiplier = Input.GetKey(sprintKey) ? sprintMultiplier : 1f;
        float verticalSpeed = verticalInput * walkSpeed * speedMultiplier;
        float horizontalSpeed = horizontalInput * walkSpeed * speedMultiplier;

        Vector3 speed = new Vector3 (horizontalSpeed, 0, verticalSpeed);
        speed = transform.rotation * speed;

        characterController.SimpleMove(speed);

        isMoving = verticalInput !=0 || horizontalInput !=0;
        
    }

    private void HandleRotation()
    {
        float mouseXRotation = Input.GetAxis(mouseXInput) * mouseSens;
        transform.Rotate(0, mouseXRotation, 0);

        verticalRotation -= Input.GetAxis(mouseYInput) * mouseSens;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        m_Camera.transform.localRotation = Quaternion.Euler(verticalRotation, 0 ,0);
    }

    private void HandleFootSteps()
    {
        float currentStepInterval = (Input.GetKey(sprintKey) ? sprintStepInterval : walkStepInterval);

        if (isMoving && Time.time > nextStepTime && characterController.velocity.magnitude > velocityThreshHold)
        {
            PlayFootstepSounds();
            nextStepTime = Time.time + currentStepInterval;
        }
    }

    private void PlayFootstepSounds()
    {
        int randomIndex;

        if(footstepSounds.Length == 1)
        {
            randomIndex = 0;
        }
        else
        {
            randomIndex = Random.Range(0, footstepSounds.Length -1 );
            if(randomIndex >= LastPlayedIndex )
            {
                randomIndex++;
            }
            LastPlayedIndex = randomIndex;
            footstepSource.clip = footstepSounds[randomIndex];
            footstepSource.Play();
        }
    }
}
