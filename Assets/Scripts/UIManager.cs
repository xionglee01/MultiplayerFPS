using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startMenu;
    public InputField usernameField;
    public InputField ipField;
    public Camera mainCamera;
    public Text hpText;
    public Text ammoText;
    public GameObject endingSign;

    public GameObject Stats;
    public Text kills;
    public Text deaths;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void ConnectToServer() {

        startMenu.SetActive(false);
        usernameField.interactable = false;
        ipField.interactable = false;
        mainCamera.enabled = false;
        hpText.enabled = true;
        ammoText.enabled = true;
        Stats.SetActive(true);
        Client.instance.ConnectToServer();
        
    }

    public void Quit() {
        Application.Quit();
    }
}
