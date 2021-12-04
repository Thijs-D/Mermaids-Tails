using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // public variables
    public GameObject OptionsPanel;
    public AudioClip clickSound;

    // private variables
    private AudioSource sound;

    private void Start()
    {
        sound = gameObject.AddComponent<AudioSource>();
        sound.clip = clickSound;
    }

    // OnClick StartGame Button
    public void StartGame()
    {
        // In Build-Settings are the correct order
        sound.Play();
        SceneManager.LoadScene(1);
    }

    // OnClick Options Button
    public void Options()
    {
        sound.Play();
        OptionsPanel.SetActive(true);
    }

    // OnClick Exit Button
    public void ExitGame()
    {
        sound.Play();
        Application.Quit();
    }

    // OnClick Credits Button
    public void Credits()
    {
        sound.Play();
    }

    // OnClick Return Button
    public void ReturnToMenu()
    {
        sound.Play();
        OptionsPanel.SetActive(false);
    }
}
