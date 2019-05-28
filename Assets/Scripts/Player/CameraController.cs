using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {



    //public variables
    public float minimumX = -60f;
    public float maximumX = 60f;
    public float minimumY = -360f;
    public float maximumY = 360;

    public float sensitivityX = 15f;
    public float sensitivityY = 15f;

    public Camera cam;
    public PlayerController player;

    //private variables
    float rotationY = 0f;
    float rotationX = 0f;

    private Vector3 offset;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        offset = cam.transform.position - player.transform.position;
    }


    void Update () {
        rotationY += Input.GetAxis("Mouse X") * sensitivityY;
        rotationX += Input.GetAxis("Mouse Y") * sensitivityX;

        rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);

        transform.localEulerAngles = new Vector3(0, rotationY, 0);

        cam.transform.localEulerAngles = new Vector3(-rotationX, rotationY, 0);

        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void LateUpdate()
    {
        cam.transform.position = player.transform.position + offset;
    }

}
