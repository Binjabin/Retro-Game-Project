using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class LevelManager : SimulationBehaviour, ISpawned
{ 
    [SerializeField] NetworkPlayer playerPrefab;
    Dictionary<NetworkPlayer, PlayerMovement> playerObjects = new Dictionary<NetworkPlayer, PlayerMovement>();
    PlayerInputHandler localPlayerInput;

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
        if (runner.Topology == SimulationConfig.Topologies.Shared)
        {
            Debug.Log("Connected to server, starting player as local player.");

        }
        else
        {
            Debug.Log("Connected");
        }
    }

    public void Spawned()
    {
        Debug.Log("Map Spawned");
        NetworkManager.Instance.Session.Map = this;
        foreach (NetworkPlayer player in NetworkManager.Instance.Players)
        {
            SpawnAvatar(player);
        }
    }

    public void SpawnAvatar(NetworkPlayer player)
    {
        if (playerObjects.ContainsKey(player))
            return;
        if (player.Object.HasStateAuthority)
        {
            Debug.Log($"Spawning avatar for player {player.Name} with input auth {player.Object.InputAuthority}");
            Debug.Log(player.Object);
            Debug.Log(player.Object.InputAuthority);
            Debug.Log(player.playerPrefab);
            
            PlayerMovement playerObject = Runner.Spawn(player.playerPrefab, Vector3.zero, Quaternion.identity, player.Object.InputAuthority);
            Debug.Log("done spawn");
            //playerObjects[player] = playerObject;
        }
    }

}