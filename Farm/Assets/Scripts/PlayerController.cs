using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public Rigidbody rigidBody;
    public float jumpForce;
    public float moveSpeed;
    public float gravityScale;
    private float xOrigin;
    private float zOrigin;
    private float height;
    public float respawnLimit;
    public float fallMulti = 2.5f;
    public float lowJumpMulti = 2f;

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
        //rigidBody = GetComponent<Rigidbody>(); REPLACED BY CHARACTERCONTROLLER
    }

    // Update is called once per frame
    void Update()
    {
        // get input for movement, store y vector to fix jumping
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
        // normalize so that speed doesn't double when moving diagonally
        moveDirection = moveDirection.normalized * moveSpeed; 
        moveDirection.y = yStore;

        //get input for jumping, if player is touching the ground
        if (controller.isGrounded)
        {
            moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }

        // add gravity to player, Time.deltaTime smooths it out for any user
        moveDirection.y += (Physics.gravity.y * gravityScale) * Time.deltaTime;   
        controller.Move(moveDirection * Time.deltaTime);

        // player respawns to the starting point if they fall off edge (go past respawnLimit)
        if (transform.position.y < respawnLimit)
        {
            transform.position = new Vector3(xOrigin, height, zOrigin);
        }
    }
}
