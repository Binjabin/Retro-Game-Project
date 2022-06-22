using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class GameManager : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }
    [PunRPC]
    void StartGame()
    {
        players = GameObject.FindGameObjectsWithTag("Player").ToList();

        int playerCount = players.Count;
        float requiredCircumference = playerCount * 5f;
        float circleRadius = requiredCircumference / (2 * Mathf.PI);
        float degreesPerPlayer = 360f / playerCount;

        float degreesSoFar = 0f;
        foreach(GameObject currentPlayer in players)
        {
            float thisPlayerX = Mathf.Cos(degreesSoFar) * circleRadius;
            float thisPlayerY = Mathf.Sin(degreesSoFar) * circleRadius;

            Vector3 newPos = new Vector3(thisPlayerX, thisPlayerY, 0f);
            view.RPC("SetPosition", RpcTarget.All);
            currentPlayer.transform.position = newPos;
            currentPlayer.transform.rotation = Quaternion.LookRotation(transform.forward, newPos);
            currentPlayer.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            currentPlayer.GetComponent<Rigidbody2D>().angularVelocity = 0;
            degreesSoFar += degreesPerPlayer;
        }
    }

    void LobbySpawn()
    {
        var playerObject = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
    }
    [PunRPC]
    void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

}
