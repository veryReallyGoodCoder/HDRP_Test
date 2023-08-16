using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YaironPlayerMovementScript : MonoBehaviour
{

    public CharacterController controller;
    public Transform groundCheck;

    public float moveSpeed = 20f;
    public float jumpHeight = 10f;

    public float gravity = -9.81f;
    public float groundDistance = 0.5f;
    public LayerMask groundMask;
    private bool isGrounded;

    public LayerMask endMask;
    private bool isFinished;

    Vector3 velocity;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        MenuScreen();

        EndCondition();

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        Vector3 movePlayer = transform.right * xMove + transform.forward * zMove;
        
        controller.Move(movePlayer * moveSpeed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void MenuScreen()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ;
        }


    }

    private void EndCondition()
    {
        isFinished = Physics.CheckSphere(groundCheck.position, groundDistance, endMask);
        if (isFinished)
        {
            SceneManager.LoadScene("welcomeScene");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Taking Damage");
        }
    }
}
