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
        InitialiseNetworkRunner(networkRunner, roomName, GameMode.AutoHostOrClient, NetAddress.Any(), SceneManager.GetSceneByName("Game Scene").buildIndex, null);
    }

    public void JoinAnyGame()
    {
        InitialiseNetworkRunnerAny(networkRunner, GameMode.AutoHostOrClient, NetAddress.Any(), SceneManager.GetSceneByName("Game Scene").buildIndex, null);
    }

    private async void InitialiseNetworkRunner(NetworkRunner runner, String roomName, GameMode gameMode, NetAddress address, SceneRef scene, Action<NetworkRunner> initialized)
    {
        var sceneObjectProvider = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();
        if (sceneObjectProvider == null)
        {
            sceneObjectProvider = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        runner.ProvideInput = true;

        await runner.StartGame(new StartGameArgs
        {
            SessionName = roomName,
            GameMode = gameMode,
            Address = address,
            Initialized = initialized,
            SceneManager = sceneObjectProvider
        });

        runner.SetActiveScene("Game Scene");
    }

    private async void InitialiseNetworkRunnerAny(NetworkRunner runner, GameMode gameMode, NetAddress address, SceneRef scene, Action<NetworkRunner> initialized)
    {
        var sceneObjectProvider = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();
        if (sceneObjectProvider == null)
        {
            sceneObjectProvider = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        runner.ProvideInput = true;

        await runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            Address = address,
            Initialized = initialized,
            SceneManager = sceneObjectProvider
        });

        runner.SetActiveScene("Game Scene");
        GetComponent<SpawnPlayerNetwork>().SpawnLocalPlayer(runner);
    }


}
