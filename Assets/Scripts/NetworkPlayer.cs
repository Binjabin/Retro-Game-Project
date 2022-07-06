using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


public class NetworkPlayer : NetworkBehaviour
{
    public static NetworkPlayer Local { get; set; }
    [SerializeField] public PlayerMovement playerPrefab;
    [Networked] public NetworkString<_32> Name { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;
            NetworkManager.Instance.SetUpPlayer(Object.InputAuthority, this);
            RPC_SetName(PlayerPrefs.GetString("Name"));
            Debug.Log("Spawn own ship");
        }
        else
        {
            Debug.Log("Spawn other ship");
        }

    }



    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetName(NetworkString<_32> name)
    {
        Name = name;
    }

    public void PlayerLeft(PlayerRef player)
    {
        if(player == Object.InputAuthority)
        {
            Runner.Despawn(Object);
        }
    }
}
