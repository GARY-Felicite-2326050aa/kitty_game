using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public GameObject endGameCanvas;
    public bool hasWon;


    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip winSound;

    public void WinGame()
    {
        if (audioSource && winSound)
        {
            audioSource.PlayOneShot(winSound);
        }

        endGameCanvas.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
Cursor.visible = true;
    }



    public void LoseGame()
    {
        hasWon = false;
        endGameCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }

    public void ReplayGame()
{
    // On remet le temps à la normale sinon le jeu restera figé
    Time.timeScale = 1f; 
    // On recharge la scène active (GameScene)
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
}
