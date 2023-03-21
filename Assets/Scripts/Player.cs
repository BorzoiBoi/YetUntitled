using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float groundDrag;

    public static Action shootInput;
    private bool shootKeyPressed;
    private bool jumpKeyPressed;
    private float xInput;
    private float zInput;
    private Vector3 moveDirection;
    private Rigidbody rigidbodyComponent;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    private bool onGround() => !(Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0);

    private void getInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyPressed = true;
        }
        if (Input.GetMouseButton(0))
        {
            shootKeyPressed = true;
        }
    }

    private void movePlayer()
    {
        moveDirection = transform.forward * zInput + transform.right * xInput;

        if (onGround())
        {
            rigidbodyComponent.drag = groundDrag;
            rigidbodyComponent.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rigidbodyComponent.drag = 0;
            rigidbodyComponent.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
        } 

        if (onGround() && jumpKeyPressed)
        {
            rigidbodyComponent.velocity = new Vector3(rigidbodyComponent.velocity.x, 0, rigidbodyComponent.velocity.z);
            rigidbodyComponent.AddForce(0, 50, 0, ForceMode.Impulse);
            jumpKeyPressed = false;
        }

        jumpKeyPressed = false;
    }

    private void limitThatSpeedyBoy()
    {
        Vector3 curVel = new Vector3(rigidbodyComponent.velocity.x, 0, rigidbodyComponent.velocity.z);

        if(curVel.magnitude > moveSpeed)
        {
            Vector3 maxSpeed = curVel.normalized * moveSpeed;
            rigidbodyComponent.velocity = new Vector3(maxSpeed.x, rigidbodyComponent.velocity.y, maxSpeed.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
        limitThatSpeedyBoy();
    }

    // FixedUpdate is called once every physics update (prevents lag from slowing the gameworld down)
    private void FixedUpdate()
    {
        if (shootKeyPressed)
        {
            shootInput?.Invoke();
            shootKeyPressed = false;
        }
        movePlayer();
    }
}