﻿using System.Collections;
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

    private Quaternion rotation;
    private Vector3 startPos;

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

        //only move camera when right clicking
        if (Input.GetMouseButton(1))
        {
            //moves the camera based on current rotation of target
            float yAngle = target.eulerAngles.y;
            float xAngle = pivot.eulerAngles.x;
            rotation = Quaternion.Euler(xAngle, yAngle, 0);
        }

        transform.position = target.position - (rotation * offset);

        //stops camera from going through floor
        if (transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }

        transform.LookAt(target);
    }
}
