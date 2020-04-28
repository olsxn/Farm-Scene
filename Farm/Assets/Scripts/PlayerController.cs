using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float moveSpeed;
    public float gravityScale;
    private float xOrigin;
    private float zOrigin;
    private float height;
    public float respawnLimit;

    public CharacterController controller;

    private Vector3 moveDirection;

    private void Awake()
    {
        // startpoint
        xOrigin = transform.position.x;
        zOrigin = transform.position.z;
        height = transform.position.y;

        // checking height is fine
        if (height < 0)
        {
            print("FALLPREVENT: Height is below zero! Setting secure height.");
            height += 3;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // get input for movement, store y vector to fix jumping
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));

        // normalize so that speed doesn't double when moving diagonally
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded) {
            Jump();
        }

        // add gravity to player, Time.deltaTime smooths it
        moveDirection.y += (Physics.gravity.y * gravityScale) * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        // player respawns to the starting point if they fall off edge (go past respawnLimit)
        if (transform.position.y < respawnLimit)
        {
            transform.position = new Vector3(xOrigin, height, zOrigin);
        }

    }

    void Jump()
    {
        moveDirection.y = jumpForce;
    }
}
