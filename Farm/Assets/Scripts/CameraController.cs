using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public Transform pivot;

    public Vector3 offset;

    public float rotateSpeed;
    public float maxViewAngle;
    public float minViewAngle;

    public bool invertY;
    public bool useOffsetValues;
    // Start is called before the first frame update
    void Start()
    {
        // get rid of cursor when playing
        Cursor.lockState = CursorLockMode.Locked;

        if(useOffsetValues)
        {
            // keeps camera distance from player
            offset = target.position - transform.position;  
        }

        // pivot is moved to player and made a child of the player
        pivot.position = target.position;
        pivot.parent = target.transform;
    }

    void LateUpdate()
    {
        // get x pos of mouse and rotate target
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0, horizontal, 0);
        // get y pos of mouse and rotate pivot
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        if (invertY)
        {
            pivot.Rotate(vertical, 0, 0);
        }
        else
        {
            pivot.Rotate(-vertical, 0, 0);
        }

        // add limits to how far up and down camera can go
        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }
        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }

        // move the camera based on the current rotation of the target and the orig offset
        float desiredY = target.eulerAngles.y;
        float desiredX = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredX, desiredY, 0);
        transform.position = target.position - (rotation * offset);

        // if camera is below player
        if(transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y - .5f, transform.position.z);
        }

        // transform.position = target.position - offset; // camera moves when player moves
        // lock camera to player
        transform.LookAt(target); 
    }
}
