using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject victoryMenu;
    public GameObject loseMenu;

    public static bool emPausa = false;
    public Animator fade;

    void Start()
    {
        pauseMenu.SetActive(false);
        victoryMenu.SetActive(false);
        loseMenu.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (emPausa)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        emPausa = true;
    }

    public void VictoryMenu()
    {
        victoryMenu.SetActive(true);
        loseMenu.SetActive(false);
        Time.timeScale = 0f;
        emPausa = true;
    }

    public void LoseMenu()
    {
        loseMenu.SetActive(true);
        Time.timeScale = 0f;
        emPausa = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        emPausa = false;
    }

    public void GoMainMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadLevel(0));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        loseMenu.SetActive(false);
        Time.timeScale = 1f;
        emPausa = false;
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadNextLevel()
    {
        victoryMenu.SetActive(false);
        Time.timeScale = 1f;
        emPausa = false;
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int nivel)
    {
        SceneManager.LoadScene(nivel);

        fade.SetTrigger("Start");

        yield return new WaitForSeconds(2);
    }
}
