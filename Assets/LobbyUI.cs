using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerCountUI;
    [SerializeField] TextMeshProUGUI hostUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerCountUI.text = "Players: ";// + PhotonNetwork.CurrentRoom.PlayerCount;
        hostUI.text = "Host: ";// + PhotonNetwork.MasterClient.NickName;
    }
}
