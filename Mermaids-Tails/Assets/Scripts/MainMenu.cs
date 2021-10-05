using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        // In Build-Settings are the correct order
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        OptionsPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {

    }

    public void ReturnToMenu()
    {
        OptionsPanel.SetActive(false);
    }
}
