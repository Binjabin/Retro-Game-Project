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
        usernameText = GetComponent<TMP_Text>();
        usernameText.text = "name";// playerView.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = Vector3.up;
        Vector3 newPosition = new Vector3(playerMovement.transform.position.x, playerMovement.transform.position.y + 1, 0f);
        transform.position = newPosition;
    }
}
