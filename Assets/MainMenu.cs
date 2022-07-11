using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject joinMenu;
    NetworkPlayer localPlayer;
    [SerializeField] TMP_InputField usernameInput;


    private void Start()
    {
        loadingScreen.SetActive(true);
        joinMenu.SetActive(false);
        PlayerPrefs.DeleteKey("Name");
        
    }

    public void JoinRandomGame()
    {
        NetworkManager.instance.StartSession();
       
    }

    public void OpenJoinMenu()
    {
        joinMenu.SetActive(true);
        loadingScreen.SetActive(false);
    }

    public void UpdatePlayerName()
    {
        PlayerPrefs.SetString("Name", usernameInput.text);
        PlayerPrefs.Save();
    }
}
