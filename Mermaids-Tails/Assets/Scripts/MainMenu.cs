using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // public variables
    public GameObject OptionsPanel;

    // OnClick StartGame Button
    public void StartGame()
    {
        // In Build-Settings are the correct order
        SceneManager.LoadScene(1);
    }

    // OnClick Options Button
    public void Options()
    {
        OptionsPanel.SetActive(true);
    }

    // OnClick Exit Button
    public void ExitGame()
    {
        Application.Quit();
    }

    // OnClick Credits Button
    public void Credits()
    {

    }

    // OnClick Return Button
    public void ReturnToMenu()
    {
        OptionsPanel.SetActive(false);
    }
}
