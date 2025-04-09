using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public SettingsScript settings;
    public GameObject characterPanel;

    private void Awake()
    {
        characterPanel.SetActive(false);
    }

    public void LoadCharacterScreen()
    {
        characterPanel.SetActive(true);
    }
    public void PlayGame(int you)
    {
        settings.characterProfile = you;
        SceneManager.LoadScene(1);
    }
}
