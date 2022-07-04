using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System;
using System.Linq;



public class NetworkRunnerHandler : MonoBehaviour
{
    NetworkRunner networkRunner;

    // Start is called before the first frame update
    private void Awake()
    {
        networkRunner = GetComponent<NetworkRunner>();
    }

    public void JoinGameWithName(String roomName)
    {
        //InitialiseNetworkRunner(networkRunner, roomName, GameMode.AutoHostOrClient, NetAddress.Any(),  null);
    }

    public void JoinAnyGame()
    {
        InitialiseNetworkRunnerForAny(networkRunner, GameMode.AutoHostOrClient, NetAddress.Any(),  null);
    }

    private async void InitialiseNetworkRunnerForAny(NetworkRunner runner, GameMode gameMode, NetAddress address,  Action<NetworkRunner> initialized)
    {
        
    }
}
