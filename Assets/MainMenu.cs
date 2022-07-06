using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject joinMenu;

    private void Start()
    {
        loadingScreen.SetActive(true);
        joinMenu.SetActive(false);
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
}
