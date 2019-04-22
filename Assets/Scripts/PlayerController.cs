using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //https://www.mvcode.com/lessons/first-person-camera-and-controller-jamie

    //Public variables
    public float walkSpeed;

    //Private variables
    Rigidbody rb;
    Vector3 moveDirection;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }



    void Update () {
        //Get directional input from the user
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = (horizontalMovement * transform.right + verticalMovement * transform.forward).normalized;
	}

    void FixedUpdate()
    {
        //Call the move function
        Move();
    }



    void Move()
    {
        Vector3 yVelFix = new Vector3(0, rb.velocity.y, 0);
        rb.velocity = moveDirection * walkSpeed * Time.deltaTime;
        rb.velocity += yVelFix;
    }


}
