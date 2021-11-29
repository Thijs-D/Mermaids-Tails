using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    // public variables
    public GameObject gameMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnClick StartGame Button
    public void Resume()
    {
        // In Build-Settings are the correct order
        gameMenuUI.SetActive(false);
    }

    // OnClick Options Button
    public void Options()
    {
        //TODO
    }

    // OnClick Exit Button
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
