using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class GameManager : MonoBehaviourPunCallbacks
{
    public List<GameObject> players = new List<GameObject>();
    [SerializeField] GameObject playerPrefab;
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        LobbySpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Start Game");
                view.RPC("StartGame", RpcTarget.All);
            }
        }
    }
    [PunRPC]
    void StartGame()
    {

        int playerCount = players.Count;
        float requiredCircumference = playerCount * 5f;
        float circleRadius = requiredCircumference / (2 * Mathf.PI);
        float degreesPerPlayer = 360f / playerCount;
        float degreesSoFar = 0f;
        int index = 0;
        foreach(GameObject currentPlayer in players)
        {
            if(currentPlayer.GetComponent<PhotonView>().IsMine)
            {
                float thisPlayerX = Mathf.Cos(degreesSoFar) * circleRadius;
                float thisPlayerY = Mathf.Sin(degreesSoFar) * circleRadius;
                Vector3 newPos = new Vector3(thisPlayerX, thisPlayerY, 0f);
                InitPlayer(currentPlayer, newPos);

            }
            degreesSoFar += degreesPerPlayer;
            index++;
        }
    }

    void LobbySpawn()
    {
        var playerObject = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        
    }



    void InitPlayer(GameObject playerObject, Vector3 pos)
    {
        if(playerObject.GetComponent<PhotonView>().IsMine)
        {
            playerObject.transform.position = pos;
            playerObject.transform.rotation = Quaternion.LookRotation(transform.forward, pos);
            playerObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            playerObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }
        
    }

    [PunRPC]
    void GetPlayerList()
    {
        List<PlayerMovement> playerMovementScripts = FindObjectsOfType<PlayerMovement>().ToList();
        foreach(PlayerMovement playerMovement in playerMovementScripts)
        {
            if(playerMovement.gameObject.GetComponent<PhotonView>().IsMine)
            {
                
            }
        }
    }
}
