using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField] public PlayerMovement playerPrefab;
    [Networked] public NetworkString<_32> Name { get; set; }

    public PlayerMovement playerMovement;
    public PlayerInputHandler playerInput;
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
        
        Debug.Log("Get name");
        if(PlayerPrefs.HasKey("Name"))
        {
            RPC_SetName(PlayerPrefs.GetString("Name"));
        }
        else
        {
            RPC_SetName(null);
        }
        NetworkManager.Instance.SetUpPlayer(Object.InputAuthority, this);

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
