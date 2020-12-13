using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    [SerializeField] string mainMenu;

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
