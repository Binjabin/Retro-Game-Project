using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class GameManager : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    [SerializeField] GameObject playerPrefab;

    float degreesPerPlayer;
    float circleRadius;
    GameObject myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        LobbySpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (true)//PhotonNetwork.IsMasterClient)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Start Game");
                StartGame();
            }
        }
    }
    
    void StartGame()
    {
        int index = 0;
        foreach (GameObject currentPlayer in players)
        {
            //var currentPlayerView = currentPlayer.GetComponent<PhotonView>();
            //currentPlayerView.RPC("InitPlayer", currentPlayerView.Owner, index);
            index++;
        }
    }

    void LobbySpawn()
    {
        myPlayer = GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

    }


    public float GetSpawnCircleRadius()
    {
        int playerCount = players.Count;
        float requiredCircumference = playerCount * 5f;
        return requiredCircumference / (2 * Mathf.PI);
    }

    public float GetRadPerPlayer()
    {
        int playerCount = players.Count;
        float radPerCicrle = 360f * Mathf.Deg2Rad;
        return  radPerCicrle / playerCount;
    }






}
