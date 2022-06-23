using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    [SerializeField] float turnSpeed = 1f;
    [SerializeField] float thrustSpeed = 1f;
    Rigidbody2D rb;
    float rotation;
    PhotonView view;
    [SerializeField] SpriteRenderer visuals;
    public float playerIndex;

    void Start()
    {
        FindObjectOfType<GameManager>().players.Add(gameObject);
        playerIndex = PhotonNetwork.CurrentRoom.PlayerCount;
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(view.IsMine)
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

}
