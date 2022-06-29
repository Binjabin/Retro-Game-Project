using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class Username : MonoBehaviour
{
    TMP_Text usernameText;
    PhotonView playerView;

    // Start is called before the first frame update
    void Start()
    {
        playerView = GetComponentInParent<PhotonView>();
        usernameText = GetComponent<TMP_Text>();
        usernameText.text = playerView.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = Vector3.up;
        Vector3 newPosition = new Vector3(playerView.transform.position.x, playerView.transform.position.y + 1, 0f);
        transform.position = newPosition;
    }
}
