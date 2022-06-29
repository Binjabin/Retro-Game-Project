using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] TMP_Text usernameText;
    [SerializeField] float turnSpeed = 1f;
    [SerializeField] float thrustSpeed = 1f;
    Rigidbody2D rb;
    float rotation;
    [SerializeField] SpriteRenderer visuals;
    public float playerIndex;
    GameManager gameManager;


    void Start()
    {
        
        gameManager = FindObjectOfType<GameManager>();
        gameManager.players.Add(gameObject);
        rb = GetComponent<Rigidbody2D>();
        if(true)//view.IsMine)
        {
            SetUpCamera();
        }
    }

    void SetUpCamera()
    {
        var vcam = FindObjectOfType<CinemachineVirtualCamera>();
        vcam.LookAt = transform;
        vcam.Follow = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (true)//view.IsMine)
        {
            DoMovement();
        }
        else
        {
            visuals.color = Color.red;
        }

    }
    void DoMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.up * Time.fixedDeltaTime * thrustSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.eulerAngles += new Vector3(0f, 0f, turnSpeed * Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles -= new Vector3(0f, 0f, turnSpeed * Time.fixedDeltaTime);
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

}
