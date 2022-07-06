using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;

public class NetworkSession : NetworkBehaviour
{
    public LevelManager Map {get; set;}


    public override void Spawned()
    {
        NetworkManager.Instance.Session = this;
        if(Object.HasStateAuthority && (Runner.CurrentScene == 0 || Runner.CurrentScene == SceneRef.None))
        {
            Debug.Log("triggering scene change");
            LoadMap();
            
        }
        else
        {
            Debug.Log("Game scene not loading cause we are in scene " + Runner.CurrentScene);
        }
    }

    public void LoadMap()
    {
        Runner.SetActiveScene(1);
    }
}
