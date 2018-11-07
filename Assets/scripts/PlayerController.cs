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
    private bool startText1 = false;
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
    private string[] textRoom3 = new string[2] { "Something is definitely fishy here,", "I just want to go back to my 9-5 job." };
    private string[] textRoom4 = new string[1] { "Well I'm too deep to return now, I must go on" };
    private string[] textRoom5 = new string[1] { "Why is it so hot in here all of a sudden?" };
    private string[] textRoom6 = new string[2] { "What is it that I'm not supposed to see?", "This can't end well at all." };
    private string[] textRoom7 = new string[1] { "Oh no, what is that grinding noise???" };

    private int currTextIndex = 0;
    private float inputtedTextDelay;
    public bool isWhipping = false;
    private AudioSource audio_whip;

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
        audio_whip = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        updateText();
        float yStore = moveDir.y;
        //only move if bool is true (if text isn't currently displayed)
        if (canMove)
        {
            moveDir = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDir = moveDir.normalized * moveSpeed;
        }
        
        moveDir.y = yStore;

        //if player is ony ground and canMove
        if (controller.isGrounded && canMove)
        {
            moveDir.y = 0f;
            //if space bar, then jump
            if (Input.GetButtonDown("Jump"))
            {
                moveDir.y = jumpForce;
                anim.Play("jumpUp");
                alreadyHit = false;
            }
        }

        moveDir.y = moveDir.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        moveDir = moveDir + (3.0f * conveyorVec);
        //if can't Move, set player velocity to 0
        if (!canMove)
        {
            moveDir = new Vector3(0f, 0f, 0f);
            //if left click when text is up, skip to next line
            if (Input.GetMouseButtonDown(0))
            {
                textDelay = -1f;
            }
        }
        controller.Move(moveDir * Time.deltaTime);

        //if left click and canMove --> whip
        if (Input.GetMouseButtonDown(0) && canMove)
        {
            anim.Play("whip");
            audio_whip.Play(0);
        }

        if(transform.position.y < -100) {
            controller.Move(currCheckpoint - transform.position);
        }
        //sets the players foward movement to the direction the camera is facing
        
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        conveyorVec = Vector3.zero;

        if(hit.gameObject.layer == LayerMask.NameToLayer("KillFloor")) {
            controller.Move(currCheckpoint - transform.position);
        }

        //if player hits a checkpoint, set corresponding bool to true and canMove to false so proper text can be displayed
        if(hit.gameObject.layer == LayerMask.NameToLayer("Checkpoint")) {
            if(hit.gameObject.tag == "Checkpoint1") {
                startText1 = true;
                canMove = false;
            }
            else if (hit.gameObject.tag == "Checkpoint2")
            {
                startText2 = true;
                canMove = false;
            }
            else if (hit.gameObject.tag == "Checkpoint3")
            {
                startText3 = true;
                canMove = false;
            }
            else if (hit.gameObject.tag == "Checkpoint4")
            {
                startText4 = true;
                canMove = false;
            }
            else if (hit.gameObject.tag == "Checkpoint5")
            {
                startText5 = true;
                canMove = false;
            }
            else if (hit.gameObject.tag == "Checkpoint6")
            {
                startText6 = true;
                canMove = false;
            }
            currCheckpoint = hit.transform.position;
            Destroy(hit.gameObject);

        }
        //if collided with a key
        if (hit.transform.tag == "Collectible")
        {
            hit.transform.SendMessage("Collided");
        }

        //if player is on a conveyor belt, change velocity/ velocity direction
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
        //display the proper text from an array of text based on what checkpoint was reached
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
        else if (textDelay < 0f && startText1)
        {
            if (currTextIndex < textRoom2.Length)
            {
                storyText.text = textRoom2[currTextIndex];
                currTextIndex++;
                textDelay = inputtedTextDelay;
            }
            else
            {
                startText1 = false;
                storyText.text = "";
                canMove = true;
                currTextIndex = 0;
            }
        }
        else if (textDelay < 0f && startText2)
        {
            if (currTextIndex < textRoom3.Length)
            {
                storyText.text = textRoom3[currTextIndex];
                currTextIndex++;
                textDelay = inputtedTextDelay;
            }
            else
            {
                startText2 = false;
                storyText.text = "";
                canMove = true;
                currTextIndex = 0;
            }
        }
        else if (textDelay < 0f && startText3)
        {
            if (currTextIndex < textRoom4.Length)
            {
                storyText.text = textRoom4[currTextIndex];
                currTextIndex++;
                textDelay = inputtedTextDelay;
            }
            else
            {
                startText3 = false;
                storyText.text = "";
                canMove = true;
                currTextIndex = 0;
            }
        }
        else if (textDelay < 0f && startText4)
        {
            if (currTextIndex < textRoom5.Length)
            {
                storyText.text = textRoom5[currTextIndex];
                currTextIndex++;
                textDelay = inputtedTextDelay;
            }
            else
            {
                startText4 = false;
                storyText.text = "";
                canMove = true;
                currTextIndex = 0;
            }
        }
        else if (textDelay < 0f && startText5)
        {
            if (currTextIndex < textRoom6.Length)
            {
                storyText.text = textRoom6[currTextIndex];
                currTextIndex++;
                textDelay = inputtedTextDelay;
            }
            else
            {
                startText5 = false;
                storyText.text = "";
                canMove = true;
                currTextIndex = 0;
            }
        }
        else if (textDelay < 0f && startText6)
        {
            if (currTextIndex < textRoom7.Length)
            {
                storyText.text = textRoom7[currTextIndex];
                currTextIndex++;
                textDelay = inputtedTextDelay;
            }
            else
            {
                startText6 = false;
                storyText.text = "";
                canMove = true;
                currTextIndex = 0;
            }
        }
    }
}
