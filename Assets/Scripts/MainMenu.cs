using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string startScene, continueScene;
    [SerializeField] GameObject continueButton;

    void Start()
    {
        if (PlayerPrefs.HasKey(startScene + "_unlocked"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(continueScene);
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
        PlayerPrefs.DeleteAll();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }
}
