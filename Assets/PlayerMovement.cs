using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float turnSpeed = 1f;
    [SerializeField] float thrustSpeed = 1f;
    Rigidbody2D rb;
    float rotation;
    PhotonView view;
    [SerializeField] SpriteRenderer visuals;

    void Start()
    {
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
}
