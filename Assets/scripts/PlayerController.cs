﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float jumpForce;
    public CharacterController controller;
    private Vector3 moveDir;
    public float gravityScale;
    public Vector3 conveyorVec = Vector3.zero;
    public Text storyText;
    public float textDelay;

    private bool canMove = false;
    private bool doneWithIntroText = false;
    private bool startText2 = false;
    private bool startText3 = false;
    private Vector3 startPos;
    private Vector3 currCheckpoint;
    private Animator anim;
    private bool alreadyHit = false;
    private SystemManager manager;
    private int whipHash = 1968340083;
    private string[] introText = new string[6] {"Wait...", "Where am I!?!", "What am I!?!", "What is this ribbon on my head?",
        "What is going on in here?", "I got to get out of this room..." };
    private string[] textRoom2 = new string[2] { "Why is it that as soon as I manage to get out of one room,", "I enter another room." };
    private int currTextIndex = 0;
    private float inputtedTextDelay;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        startPos = transform.position;
        currCheckpoint = transform.position;
        anim = gameObject.GetComponent<Animator>();
        manager = GetComponent<SystemManager>();
        inputtedTextDelay = textDelay;
        storyText.text = introText[currTextIndex];
        currTextIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        updateText();
        float yStore = moveDir.y;
        if (canMove)
        {
            moveDir = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDir = moveDir.normalized * moveSpeed;
        }
        
        moveDir.y = yStore;

        if (controller.isGrounded && canMove)
        {
            moveDir.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                moveDir.y = jumpForce;
                anim.Play("jumpUp");
                alreadyHit = false;
            }
        }

        moveDir.y = moveDir.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        moveDir = moveDir + (3.0f * conveyorVec);
        if (!canMove)
        {
            moveDir = new Vector3(0f, 0f, 0f);
        }
        controller.Move(moveDir * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            anim.Play("whip");
        }
        //sets the players foward movement to the direction the camera is facing
        
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        conveyorVec = Vector3.zero;

        if(hit.gameObject.layer == LayerMask.NameToLayer("KillFloor")) {
            Debug.Log("oof you dead");
            manager.LoadGivenScene("GameOver");
        }
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
        if(hit.transform.tag == "Checkpoint1")
        {
            currCheckpoint = hit.transform.position;
            Destroy(hit.gameObject);
            startText2 = true;
            canMove = false;
        }

        if (controller.isGrounded)
        {
            if (!alreadyHit)
            {
                Debug.Log("hit");
                anim.Play("jumpDown");
            }
            alreadyHit = true;
        }
        else
        {
            alreadyHit = false;
        }

        if (hit.transform.tag == "Bottle" && anim.GetCurrentAnimatorStateInfo(0).shortNameHash == whipHash)
        {
            Destroy(hit.gameObject);
        }

        
    }

    void updateText()
    {
        textDelay -= Time.deltaTime;
        if (textDelay < 0f && !doneWithIntroText)
        {
            if(currTextIndex < introText.Length)
            {
                storyText.text = introText[currTextIndex];
                currTextIndex++;
                textDelay = inputtedTextDelay;
            }
            else
            {
                doneWithIntroText = true;
                storyText.text = "";
                canMove = true;
                currTextIndex = 0;
            }
        }
        if (textDelay < 0f && startText2)
        {
            if (currTextIndex < textRoom2.Length)
            {
                storyText.text = textRoom2[currTextIndex];
                currTextIndex++;
                textDelay = inputtedTextDelay;
            }
            else
            {
                startText2 = false;
                storyText.text = "";
                canMove = true;
            }
        }
    }
}
