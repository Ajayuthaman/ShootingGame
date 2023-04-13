using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("Level01");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuiteGame()
    {
        Application.Quit();
    }
}
