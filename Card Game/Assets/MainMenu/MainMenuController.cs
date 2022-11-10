using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnMainMenuButton()
    {
        SceneManager.LoadScene("CardInventory");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
