using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;
public class ConnectToNetwork : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject joinButton;
    [SerializeField] GameObject loadingText;
    [SerializeField] TMP_InputField usernameInput;
    // Start is called before the first frame update
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        DoConnectingUI();
        ConnectToMaster();
    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master! Joining room.");
        DoConnectedUI();
    }

    public void ConnectToMaster()
    {
        if(!PhotonNetwork.IsConnected)
        {
            Debug.Log("Connecting...");
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("Already connected.");
            DoConnectedUI();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No available rooms. Creating new room...");
        PhotonNetwork.CreateRoom(null, new RoomOptions());
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Successfully joined room.");
        PhotonNetwork.LoadLevel("Game Scene");
    }

    void DoConnectedUI()
    {
        loadingText.SetActive(false);
        joinButton.SetActive(true);
    }
    void DoConnectingUI()
    {
        loadingText.SetActive(true);
        joinButton.SetActive(false);
    }
    public void JoinAnyRoom()
    {
        PhotonNetwork.JoinRandomRoom();
        if (usernameInput.text != null)
        {
            PhotonNetwork.NickName = usernameInput.text;
        }
        else
        {

        }
        
        DoConnectingUI();

    }
}
