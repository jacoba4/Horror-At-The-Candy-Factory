using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float jumpForce;
    public CharacterController controller;
    private Vector3 moveDir;
    public float gravityScale;
    public Vector3 conveyorVec = Vector3.zero;

    private Vector3 startPos;
    private Vector3 currCheckpoint;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        startPos = transform.position;
        currCheckpoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //sets the players foward movement to the direction the camera is facing
        float yStore = moveDir.y;
        moveDir = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
        moveDir = moveDir.normalized * moveSpeed;
        moveDir.y = yStore;

        if (controller.isGrounded)
        {
            moveDir.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                moveDir.y = jumpForce;
            }
        }

        moveDir.y = moveDir.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        moveDir = moveDir + (3.0f * conveyorVec);
        controller.Move(moveDir * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        conveyorVec = Vector3.zero;
        if (hit.transform.tag == "Collectible")
        {
            hit.transform.SendMessage("Collided");
        }

        if (hit.transform.tag == "Conveyer")
        {
            Vector3 dir = Vector3.right;
            dir = Quaternion.Euler(0, hit.transform.eulerAngles.y, 0) * dir;
            dir.Normalize();
            conveyorVec = dir;
        }
        if(hit.transform.tag == "Checkpoint")
        {
            currCheckpoint = hit.transform.position;
            Destroy(hit.gameObject);
        }
    }
}
