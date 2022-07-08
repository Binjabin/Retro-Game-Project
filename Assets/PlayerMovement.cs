using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;
using Cinemachine;

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
    public NetworkPlayer networkPlayer;

    public override void Spawned()
    {
        networkPlayer = NetworkManager.Instance.GetPlayer(Object.InputAuthority);
        networkPlayer.playerMovement = this;
        networkPlayer.playerInput = GetComponent<PlayerInputHandler>();
        
    }

    void Start()
    {

        //gameManager = FindObjectOfType<GameManager>();
        //gameManager.players.Add(gameObject);
        rb = GetComponent<Rigidbody2D>();
        SetUpCamera();
    }

    void SetUpCamera()
    {
        if(Object.HasInputAuthority)
        {
            var vcam = FindObjectOfType<CinemachineVirtualCamera>();
            vcam.LookAt = transform;
            vcam.Follow = transform;
        }
        
    }

    public override void FixedUpdateNetwork()
    {
        usernameText.text = networkPlayer.Name.Value;
        if (GetInput(out NetworkInputData data))
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

    public void SetInputData(bool inThrusting, float inTurn)
    {
        thrusting = inThrusting;
        turnDirection = inTurn;
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

    

}
