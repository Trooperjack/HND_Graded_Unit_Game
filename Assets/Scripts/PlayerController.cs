using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    //https://www.mvcode.com/lessons/first-person-camera-and-controller-jamie

    //Public variables
    public float walkSpeed;

    //Private variables
    Rigidbody rb;
    Vector3 moveDirection;
    CapsuleCollider col;
    Camera cam;

    Text messageText;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        cam = Camera.main;

        messageText = GameObject.Find("Canvas/MessageText").GetComponent<Text>();
    }



    void Update () {
        CheckInteraction();
        
        //Get directional input from the user
        float horizontalMovement = 0;
        float verticalMovement = 0;

        if (CanMove(transform.right * Input.GetAxisRaw("Horizontal")))
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal");
        }

        if (CanMove(transform.forward * Input.GetAxisRaw("Vertical")))
        {
            verticalMovement = Input.GetAxisRaw("Vertical");
        }

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


    bool CanMove(Vector3 dir)
    {
        float distanceToPoints = col.height / 2 - col.radius;

        Vector3 point1 = transform.position + col.center + Vector3.up * distanceToPoints;
        Vector3 point2 = transform.position + col.center - Vector3.up * distanceToPoints;

        float radius = col.radius * 0.95f;
        float castDistance = 0.5f;

        RaycastHit[] hits = Physics.CapsuleCastAll(point1, point2, radius, dir, castDistance);

        foreach (RaycastHit objectHit in hits)
        {
            if (objectHit.transform.tag == "Wall")
            {
                return false;
            }
        }

        return true;
    }



    void CheckInteraction()
    {
        messageText.text = "";

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 4f))
        {
            if (hit.transform.tag == "Door")
            {
                messageText.text = "Press E to open";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.gameObject.GetComponent<doorOpen>().enabled = true;
                }
            }
        }
    }

}
