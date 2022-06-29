using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class SpawnPlayerNetwork : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] NetworkPlayer playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    Vector3 GetSpawnPoint()
    {
        return Vector3.zero;
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        if(runner.Topology == SimulationConfig.Topologies.Shared)
        {
            Debug.Log("Connected to server, starting player as local player.");
            runner.Spawn(playerPrefab, GetSpawnPoint(), Quaternion.identity, runner.LocalPlayer);
        }
        else
        {
            Debug.Log("Connected");
        }
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if(runner.IsServer)
        {
            Debug.Log("");
        }
    }
}
