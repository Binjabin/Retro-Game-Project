using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;
using System.Linq;

public class LobbyUI : SimulationBehaviour
{
    [SerializeField] TextMeshProUGUI playerCountUI;
    [SerializeField] TextMeshProUGUI hostUI;
    NetworkRunner runner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerCountUI.text = "Players: " + NetworkManager.Instance.Players.Count();
        
        
        hostUI.text = "Host: " + NetworkManager.Instance.hostNetworkPlayer.Name;
    }
}
