using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    PlayerMovement playerMovement;
    bool thrusting;
    float turnDirection;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        thrusting = Input.GetKey(KeyCode.W);
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            turnDirection = 0;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            turnDirection = 1;

        }
        else if (Input.GetKey(KeyCode.D))
        {
            turnDirection = -1;

        }
        else
        {
            turnDirection = 0;
        }
        playerMovement.SetInputData(thrusting, turnDirection);
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData();
        networkInputData.thrustingNet = thrusting;
        networkInputData.turnInputNet = turnDirection;

        return networkInputData;
    }

    public void SetInput(bool inThrusting, float inTurnDirection)
    {
        thrusting = inThrusting;
        turnDirection = inTurnDirection;
    }
}
