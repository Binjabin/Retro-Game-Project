using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;
public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] TMP_Text usernameText;
    [SerializeField] float turnSpeed = 1f;
    [SerializeField] float thrustSpeed = 1f;
    Rigidbody2D rb;
    float rotation;
    [SerializeField] SpriteRenderer visuals;
    public float playerIndex;
    GameManager gameManager;
    bool thrusting;
    float turnDirection;

    void Start()
    {
        
        //gameManager = FindObjectOfType<GameManager>();
        //gameManager.players.Add(gameObject);
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetLocalInput();
    }

    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetworkInputData data))
        {
            thrusting = data.thrustingNet;
            turnDirection = data.turnInputNet;
            //Debug.Log("Getting input from network!");
        }
        DoMovement();

    }
    void DoMovement()
    {
        if (thrusting)
        {
            rb.AddForce(transform.up * Time.fixedDeltaTime * thrustSpeed);
        }
        transform.eulerAngles += new Vector3(0f, 0f, turnSpeed * Time.fixedDeltaTime * turnDirection);
    }

    void GetLocalInput()
    {
        thrusting = Input.GetKey(KeyCode.W);
        Debug.Log(thrusting + " " + turnDirection + " from get local input");
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
    }
    void DoWeaponry()
    {
        //if mouse button pressed:
        //if energy is more than 0:
        //turn particle system on
        //else:
        //turn particle system off
        //else 
        //turn particle system off


        //update energy bar ui with energy value
    }

    public void StopMovement()
    {
        thrustSpeed = 0f;
        turnSpeed = 0f;
        rb.angularVelocity = 0f;
        rb.velocity = Vector2.zero;

    }

    void InitPlayer(int index)
    {
        var playerObject = gameObject;
        PlayerMovement movementScript = playerObject.GetComponent<PlayerMovement>();
        FindObjectOfType<Border>().Radius = gameManager.GetSpawnCircleRadius() + 4;
        float degreesSoFar = gameManager.GetRadPerPlayer() * index;
        float thisPlayerX = Mathf.Cos(degreesSoFar) * gameManager.GetSpawnCircleRadius();
        float thisPlayerY = Mathf.Sin(degreesSoFar) * gameManager.GetSpawnCircleRadius();
        Vector3 newPos = new Vector3(thisPlayerX, thisPlayerY, 0f);

        playerObject.transform.position = newPos;
        playerObject.transform.rotation = Quaternion.LookRotation(transform.forward, -newPos);
        Debug.Log(degreesSoFar);
        movementScript.StopMovement();

    }

    public NetworkInputData GetNetworkInput()
    {
        Debug.Log("sending" + thrusting + " " + turnDirection + " from get net input");
        NetworkInputData networkInputData = new NetworkInputData();
        networkInputData.thrustingNet = thrusting;
        networkInputData.turnInputNet = turnDirection;

        return networkInputData;
    }

}
