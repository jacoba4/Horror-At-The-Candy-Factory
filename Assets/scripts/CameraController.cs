using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public Vector3 offset;
    public bool useOffset;
    public float cameraRotateSpeed;
    public Transform pivot;
    public float maxViewAngle;
    public float minViewAngle;
    public LayerMask collectibleLayer;

    private Quaternion rotation;
    private Vector3 startPos;
    private Vector3 targetPosition;

    // Use this for initialization
    void Start()
    {
        if (!useOffset)
        {
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        //hides the cursor
        //Cursor.lockState = CursorLockMode.Locked;
        startPos = transform.position;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //gets X of mouse and roatates the target
        float horizontal = Input.GetAxis("Mouse X") * cameraRotateSpeed;
        target.Rotate(0, horizontal, 0);

        //gets Y of mouse and roatates the target
        float vertical = Input.GetAxis("Mouse Y") * cameraRotateSpeed;
        pivot.Rotate(-vertical, 0, 0);

        if(pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }

        if(pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }

        //moves the camera based on current rotation of target
        float yAngle = target.eulerAngles.y;
        float xAngle = pivot.eulerAngles.x;
        rotation = Quaternion.Euler(xAngle, yAngle, 0);

        transform.position = target.position - (rotation * offset);

        RaycastHit wallHit = new RaycastHit();
        if (Physics.Linecast(target.position, transform.position, out wallHit))
        {
            Debug.DrawLine(transform.position, target.position, Color.green);
            if(wallHit.collider.tag != "Player" && wallHit.collider.tag != "Collectible")
            {
                transform.position = new Vector3(wallHit.point.x, transform.position.y, wallHit.point.z);
                Debug.Log(wallHit.collider.tag);
            }
        }

        transform.LookAt(target);
    }
}
