using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f;
    [SerializeField] private LayerMask groundMask;
    private CharacterController controller;
    private bool grounded;
    private float gravity = -30f;
    private float jumpHeight = 1.5f;
    private float groundDistance = 0.4f;
    [SerializeField] private Transform groundCheck;
    private Vector3 velocity;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        grounded = true;
    }

    
    void Update()
    {
        /* Skapar sf�r vid positionen av groundcheck som �r placerad vid spelarens fot. GroundDistance �r radiusen av sf�ren. 
         * Den kollar om det finns n�got objekt p� lagret ground (groundMask) som kolliderar med sf�ren och returnerar en bool. 
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
        controller.Move(moveSpeed * Time.deltaTime * move);
        

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            velocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
