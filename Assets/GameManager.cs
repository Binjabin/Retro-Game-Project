using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class GameManager : MonoBehaviourPunCallbacks
{
    public List<GameObject> players = new List<GameObject>();
    [SerializeField] GameObject playerPrefab;
    PhotonView view;
    float degreesPerPlayer;
    float circleRadius;
    GameObject myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        LobbySpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
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
        degreesPerPlayer = 360f / playerCount;
        int index = 0;
        foreach (GameObject currentPlayer in players)
        {
            InitPlayer(currentPlayer);
            index++;
        }
    }

    void LobbySpawn()
    {
        myPlayer = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);

    }



    void InitPlayer(GameObject playerObject)
    {
        if (playerObject.GetComponent<PhotonView>().IsMine)
        {
            PlayerMovement movementScript = playerObject.GetComponent<PlayerMovement>();
            float degreesSoFar = degreesPerPlayer * movementScript.playerIndex;
            Debug.Log(degreesSoFar);
            float thisPlayerX = Mathf.Cos(degreesSoFar) * circleRadius;
            float thisPlayerY = Mathf.Sin(degreesSoFar) * circleRadius;
            Vector3 newPos = new Vector3(thisPlayerX, thisPlayerY, 0f);

            playerObject.transform.position = newPos;
            playerObject.transform.rotation = Quaternion.LookRotation(transform.forward, newPos);
            movementScript.StopMovement();
        }

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        players.Remove(myPlayer);
    }

}
