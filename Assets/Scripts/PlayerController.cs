using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed;
    private float walkSpeed = 5f;
    private float runSpeed = 8f;
    [SerializeField] private LayerMask groundMask;
    private CharacterController controller;
    private bool grounded;
    [HideInInspector] public bool falling;
    private float gravity = -30f;
    private float jumpHeight = 1.5f;
    private float groundDistance = 0.3f;
    [SerializeField] private Transform groundCheck;
    private Vector3 velocity;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        grounded = true;
        moveSpeed = walkSpeed;
    }

    
    void Update()
    {
        /* Skapar sfär vid positionen av groundcheck som är placerad vid spelarens fot. GroundDistance är radiusen av sfären. 
         * Den kollar om det finns något objekt på lagret ground (groundMask) som kolliderar med sfären och returnerar en bool. 
         */
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (grounded && velocity.y < 0f)
        {
            velocity.y = -3f;
        }
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * hInput + transform.forward * vInput;
        move.Normalize();
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            moveSpeed = runSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKey(KeyCode.S) || Input.GetKeyUp(KeyCode.W))
        {
            moveSpeed = walkSpeed;
        }
        controller.Move(moveSpeed * Time.deltaTime * move);
        

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            velocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        falling = velocity.y < -3f && !grounded;
        
    }
}
