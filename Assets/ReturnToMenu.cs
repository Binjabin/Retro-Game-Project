using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMenu : MonoBehaviour
{
    bool menuActive = false;
    [SerializeField] GameObject escMenu;
    public void DisconnectButton()
    {
        NetworkManager.Instance.Disconnect();

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menuActive = !menuActive;
        }
        escMenu.SetActive(menuActive);
    }

    
}
