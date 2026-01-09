using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameplayManager : MonoBehaviour
{
    public Game2playUIManager uiManager;
    
    [Header("Canvases")]
    public GameObject winGameCanvas;  // Pour la victoire
    public GameObject loseGameCanvas; // Pour la défaite

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip winSound;
    public AudioClip loseSound;

    private int enemiesRemaining = 0; 
    public GameObject cookieCounterObject;
    private void Start()
    {
        if (uiManager != null)
        {
            uiManager.UpdateTaskUI("Récolter tous les cookies");
        }
        else
        {
            Debug.LogWarning("Attention : Le UIManager n'est pas assigné !");
        }
    }

    public void OnCookiesFinished() 
    {
        uiManager.UpdateTaskUI("Trouver la clé d'or");
        cookieCounterObject.SetActive(false);
    }

    public void OnKeyPicked() 
    {
        uiManager.UpdateTaskUI("Ouvrir la porte");
    }

    public void StartCombatPhase()
    {
      
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesRemaining = enemies.Length;

        if (enemiesRemaining > 0)
            uiManager.UpdateTaskUI("Vaincre les " + enemiesRemaining + " ennemis !");
        else
            WinGame();
    }

    public void OnEnemyKilled()
    {
        enemiesRemaining--; 
        
        if (enemiesRemaining <= 0)
        {
        
            WinGame();
        }
        else
        {
            uiManager.UpdateTaskUI("Ennemis restants : " + enemiesRemaining);
        }
    }

    public void WinGame()
    {
        if (audioSource && winSound) audioSource.PlayOneShot(winSound);
        uiManager.UpdateTaskUI("Niveau terminé !");
        winGameCanvas.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoseGame()
    {
        if (audioSource && loseSound) audioSource.PlayOneShot(loseSound);
        loseGameCanvas.SetActive(true);
        Time.timeScale = 0f; 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReplayGame()
{
    Time.timeScale = 1f; 
    UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
}
 public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitter le jeu");
    }
}