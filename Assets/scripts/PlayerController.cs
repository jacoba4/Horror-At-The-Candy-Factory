using System.Collections;
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
    private bool startText = false;
    private bool startText2 = false;
    private bool startText3 = false;
    private bool startText4 = false;
    private bool startText5 = false;
    private bool startText6 = false;
    private Vector3 startPos;
    private Vector3 currCheckpoint;
    private Animator anim;
    private Rigidbody rb;
    private bool alreadyHit = false;
    private int whipHash = 1968340083;
    private string[] introText = new string[6] {"Wait...", "Where am I!?!", "What am I!?!", "What is this ribbon on my head?",
        "What is going on in here?", "I got to get out of this room..." };
    private string[] textRoom2 = new string[2] { "Why is it that as soon as I manage to get out of one room,", "I enter another room." };
    private string[] mainText2;
    private string[] mainText3;
    private string[] mainText4;
    private string[] mainText5;
    private string[] mainText6;
    private int currTextIndex = 0;
    private float inputtedTextDelay;
    public bool isWhipping = false;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        currCheckpoint = transform.position;
        anim = gameObject.GetComponent<Animator>();
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
            Debug.Log("checkpoint: " +  currCheckpoint);
            Debug.Log("current position: " + transform.position);
            transform.position = currCheckpoint;
        }

        if(hit.gameObject.layer == LayerMask.NameToLayer("Checkpoint")) {
            if(hit.gameObject.tag == "textCP") {
                startText = true;
                canMove = false;
            }
            currCheckpoint = hit.transform.position;
            Destroy(hit.gameObject);

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
        if (controller.isGrounded)
        {
            if (!alreadyHit)
            {
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

        isWhipping = anim.GetCurrentAnimatorStateInfo(0).shortNameHash == whipHash;



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
        if (textDelay < 0f && startText)
        {
            if (currTextIndex < textRoom2.Length)
            {
                storyText.text = textRoom2[currTextIndex];
                currTextIndex++;
                textDelay = inputtedTextDelay;
            }
            else
            {
                startText = false;
                storyText.text = "";
                canMove = true;
            }
        }
    }
}
