using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class SC_TPSController : MonoBehaviour
{
    public UIScript UI;

    public float speed = 15f;
    public float baseSpeed = 15f;
    public float sprint = 15f;
    public float jumpSpeed = 30.0f;
    public float gravity = 20.0f;
    public Transform playerCameraParent;
    public Transform firePoint;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    public float sprintTimer = 1000.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        UI = GameObject.FindGameObjectWithTag("UI").GetComponent<UIScript>();
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Debug.Log("Boost: " + sprintTimer);
        
        if (UI.isPaused == false)
        {
            Cursor.visible = false;
            if (characterController.isGrounded)
            {
                // We are grounded, so recalculate move direction based on axes
                Vector3 forward = transform.TransformDirection(Vector3.forward);
                Vector3 right = transform.TransformDirection(Vector3.right);
                float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
                float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;
                moveDirection = (forward * curSpeedX) + (right * curSpeedY);

                //Speed up movement when shift key held
                if (Input.GetKey(KeyCode.LeftShift) && canMove && sprintTimer > 0)
                {
                    speed = baseSpeed + sprint;
                    sprintTimer--;
                }
                else
                {
                    speed = baseSpeed;
                    if(sprintTimer < 101)
                        sprintTimer++;
                }


                Debug.Log("Speed: " + speed);

                //if (Input.GetButton("Jump") && canMove)
                //{
                //    moveDirection.y = jumpSpeed;
                //}
            }

            moveDirection.y -= gravity * Time.deltaTime;

            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);

            // Player and Camera rotation
            if (canMove)
            {
                rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
                rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
                playerCameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0);
                transform.eulerAngles = new Vector2(0, rotation.y);
            }
        }
        else
            Cursor.visible = true;
    }

    public Quaternion GetRotation()
    {
        return this.transform.rotation;
    }
}
