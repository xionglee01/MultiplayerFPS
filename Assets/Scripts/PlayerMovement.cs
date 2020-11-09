using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 playerVelocity;
    public float gravity = -9.81f;
    public float moveSpeed = 5f;
    public float jumpSpeed = 5f;
    public bool groundedPlayer;

    private float yVelocity = 0;



    // Start is called before the first frame update
    void Start()
    {
        gravity *= Time.fixedDeltaTime * Time.fixedDeltaTime;
        moveSpeed *= Time.fixedDeltaTime;
        jumpSpeed *= Time.fixedDeltaTime;

    }

    // Update is called once per frame
 
    public void FixedUpdate()
    {
        if (groundedPlayer && playerVelocity.y < 0)
        {

            playerVelocity.y = 0f;
        }

        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        move *= moveSpeed;

        if (controller.isGrounded) {
            yVelocity = 0f;
            if (Input.GetButtonDown("Jump")) {
                yVelocity = jumpSpeed;

            }
        }

        yVelocity += gravity;

        move.y = yVelocity;

        controller.Move(move);

    }
}
