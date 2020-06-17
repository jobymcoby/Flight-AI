using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightControllerHuman : MonoBehaviour
{
    #region Move Varibles
    public float movementSpeed = 100f;
    public float resetSpeed = 30f;
    public float boostSpeed = 90f;
    public float turnSpeed = 50f;
    #endregion 
    #region Camera Varibles
    public float horizontalSensitivity = 3f;
    public float verticalSensitivity = 3f;
    #endregion
    #region Mouse Inputs
    private float yaw = 0f;
    private float pitch = 0f;
    #endregion




    // Start is called before the first frame update
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        // move plane
        transform.position += transform.forward * Time.deltaTime * movementSpeed;
        transform.Rotate(Input.GetAxis("Vertical")/verticalSensitivity , 0.0f, -Input.GetAxis("Horizontal")/horizontalSensitivity);

        // Move Camera
        Vector3 moveCamTo = transform.position - transform.forward * 10f + Vector3.up * 5f;
        Camera.main.transform.position = moveCamTo;
        Camera.main.transform.LookAt(transform.position + transform.forward * 30f);

        // Change speed on Pitch
        movementSpeed -= transform.forward.y * Time.deltaTime *25f;
        if (movementSpeed < 35f)
        {
            movementSpeed = 35f;
        }
        if (movementSpeed > 220f)
        {
            movementSpeed = 220f;
        }

        // Crash Check
        float groundHeight = Terrain.activeTerrain.SampleHeight(transform.position);

        if (groundHeight > transform.position.y)
        {
            Debug.Log("plane crashed :(");
            transform.position = new Vector3(
                transform.position.x,
                groundHeight,
                transform.position.z
            );
        }

    }
}