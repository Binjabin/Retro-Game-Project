using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NetworkSceneManagerBase))]
public class NetworkManager : MonoBehaviour, INetworkRunnerCallbacks
{
    NetworkRunner networkRunner;
    Dictionary<PlayerRef, NetworkPlayer> players = new Dictionary<PlayerRef, NetworkPlayer>();
    [SerializeField] NetworkPlayer networkPlayerPrefab;
    [SerializeField] private NetworkSession sessionPrefab;
    PlayerInputHandler localPlayerInput;
    public static NetworkManager instance;
    NetworkSession session;
    NetworkSceneManagerBase loader;
    string currentLobbyID;

    public bool IsMaster => networkRunner != null && (networkRunner.IsServer || networkRunner.IsSharedModeMasterClient);

    public static NetworkManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<NetworkManager>();
            return instance;
        }
    }

    public NetworkSession Session
    {
        get => session;
        set
        {
            session = value;
            session.transform.SetParent(networkRunner.transform);
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            Destroy(gameObject);
        }
        else if (loader == null)
        {
            loader = GetComponent<NetworkSceneManagerBase>();

            DontDestroyOnLoad(gameObject);
            SceneManager.LoadSceneAsync("Loading");
        }
    }

    public void Start()
    {
        EnterLobby("MainLobby");
    }

    public void Connect()
    {
        if(networkRunner == null)
        {
            GameObject sessionObject = new GameObject("SessionObject");
            sessionObject.transform.SetParent(transform);

            players.Clear();

            networkRunner = sessionObject.AddComponent<NetworkRunner>();
            networkRunner.AddCallbacks(this);

        }
    }

    public void SetUpPlayer(PlayerRef playerRef, NetworkPlayer player)
    {
        Debug.Log("Set Up Player");
        players[playerRef] = player;
        player.transform.SetParent(networkRunner.transform);
        Debug.Log(player);
        Session.Map.SpawnAvatar(player);
    }

    public void StartSession()
    {
        Connect();

        networkRunner.ProvideInput = true;

        networkRunner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.AutoHostOrClient,
            Address = NetAddress.Any(),
            Initialized = null,
            SceneManager = loader
        }) ;
    }



    public async Task EnterLobby(string lobbyID)
    {
        Connect();
        currentLobbyID = lobbyID;
        var result = await networkRunner.JoinSessionLobby(SessionLobby.Custom, currentLobbyID);
        if(!result.Ok)
        {
            Debug.LogError("Failed To Connect To Lobby");
        }
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


    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("A player has joined");
        if (session == null && IsMaster)
        {
            Debug.Log("Spawning world");
            session = runner.Spawn(sessionPrefab, Vector3.zero, Quaternion.identity);
        }

        if (runner.IsServer || runner.Topology == SimulationConfig.Topologies.Shared && player == runner.LocalPlayer)
        {
            Debug.Log("Spawning player");
            runner.Spawn(networkPlayerPrefab, Vector3.zero, Quaternion.identity, player);
        }

        else Debug.Log("A player joined");
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {

        if (localPlayerInput == null && NetworkPlayer.Local != null)
        {
            localPlayerInput = NetworkPlayer.Local.GetComponent<PlayerInputHandler>();
        }

        if (localPlayerInput != null)
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
}
