using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject menuOpcoes;

    public Animator fade;

    public bool aberto = false;

    void Start()
    {
        mainMenu.SetActive(true);
        menuOpcoes.SetActive(false);
    }

    public void Opcoes()
    {
        if (aberto)
        {
            menuOpcoes.SetActive(false);
            aberto = false;
        }
        else
        {
            menuOpcoes.SetActive(true);
            aberto = true;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int nivel)
    {
        SceneManager.LoadScene(nivel);

        fade.SetTrigger("Start");

        yield return new WaitForSeconds(2);
    }
}
