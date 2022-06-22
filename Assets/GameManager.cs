using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> players = new List<GameObject>();
    [SerializeField] GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
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
    void StartGame()
    {
        int playerCount = players.Count;
        float requiredCircumference = playerCount * 5f;
        float circleRadius = requiredCircumference / (2 * Mathf.PI);

        foreach(GameObject currentPlayer in players)
        {

        }
    }

    void LobbySpawn()
    {
        var playerObject = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        players.Add(playerObject);
    }

}
