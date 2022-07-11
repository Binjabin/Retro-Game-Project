using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Username : MonoBehaviour
{
    TMP_Text usernameText;
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = Vector3.up;
        Vector3 newPosition = new Vector3(playerMovement.transform.position.x, playerMovement.transform.position.y + 0.5f, 0f);
        transform.position = newPosition;
    }
}
