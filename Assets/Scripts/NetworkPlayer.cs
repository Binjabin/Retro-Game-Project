using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Cinemachine;

public class NetworkPlayer : NetworkBehaviour
{
    public static NetworkPlayer Local { get; set; }
    [SerializeField] public PlayerMovement playerPrefab;
    [Networked] public NetworkString<_32> Name { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SetUpCamera()
    {
        var vcam = FindObjectOfType<CinemachineVirtualCamera>();
        vcam.LookAt = transform;
        vcam.Follow = transform;
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
            //SetUpCamera();
            Debug.Log("Spawn own ship");
        }
        else
        {
            Debug.Log("Spawn other ship");
        }

    }

    public void PlayerLeft(PlayerRef player)
    {
        if(player == Object.InputAuthority)
        {
            Runner.Despawn(Object);
        }
    }
}
