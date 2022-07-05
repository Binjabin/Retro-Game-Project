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
        if(runner.Topology == SimulationConfig.Topologies.Shared)
        {
            Debug.Log("Connected to server, starting player as local player.");
            
        }
        else
        {
            Debug.Log("Connected");
        }
    }

    public void PlayerJoined(PlayerRef player)
    {
        Runner.Spawn(playerPrefab, GetSpawnPoint(), Quaternion.identity, player);
    }
    public void PlayerLeft(PlayerRef player)
    {

    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            Debug.Log("Another Player Joined. Spawning Player");
            runner.Spawn(playerPrefab, GetSpawnPoint(), Quaternion.identity, player);
        }
        else Debug.Log("A player joined");
    }

    public void Spawned()
    {

        NetworkManager.Instance.Session.Map = this;
    }

    public void SpawnAvatar(NetworkPlayer player)
    {
        if (playerObjects.ContainsKey(player))
            return;
        if (player.Object.HasStateAuthority)
        {
            Debug.Log($"Spawning avatar for player {player.Name} with input auth {player.Object.InputAuthority}");

            PlayerMovement playerObject = Runner.Spawn(player.playerPrefab, Vector3.zero, Quaternion.identity, player.Object.InputAuthority);
            playerObjects[player] = playerObject;
        }
    }

}