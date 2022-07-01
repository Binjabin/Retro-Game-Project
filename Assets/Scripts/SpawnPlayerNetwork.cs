using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class SpawnPlayerNetwork : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] NetworkPlayer playerPrefab;

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

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            Debug.Log("Another Player Joined. Spawning Player");
            runner.Spawn(playerPrefab, GetSpawnPoint(), Quaternion.identity, player);
        }
        else Debug.Log("A player joined");
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        
        if(localPlayerInput == null && NetworkPlayer.Local != null)
        {
            localPlayerInput = NetworkPlayer.Local.GetComponent<PlayerInputHandler>();
        }

        if(localPlayerInput != null)
        {
            input.Set(localPlayerInput.GetNetworkInput());
            //Debug.Log("Sending Input " + localPlayerInput.GetNetworkInput().thrusting);
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        //throw new NotImplementedException();
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        //throw new NotImplementedException();
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        Debug.Log("Shut Down");
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        Debug.Log("Disconnected");
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        Debug.Log("Connect Request");
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.Log("Failed to connect");
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        //throw new NotImplementedException();
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        //throw new NotImplementedException();
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        //throw new NotImplementedException();
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        //throw new NotImplementedException();
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        //throw new NotImplementedException();
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        //throw new NotImplementedException();
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        //throw new NotImplementedException();
    }

    public void SpawnLocalPlayer(NetworkRunner runner)
    {
        runner.Spawn(playerPrefab, GetSpawnPoint(), Quaternion.identity, runner.LocalPlayer);
    }

}
