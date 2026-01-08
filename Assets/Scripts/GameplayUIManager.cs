using TMPro;
using UnityEngine;

public class Game2playUIManager : MonoBehaviour
{
    public TextMeshProUGUI cookieText;
    public TextMeshProUGUI taskText;

    public void UpdateCookieUI(int currentCookies,int maxCookies)
    {
        cookieText.text = "Cookies : " + currentCookies + " / " + maxCookies;
    }

    public void UpdateTaskUI(string newTask)
    {
        taskText.text = "Objectif : " + newTask;
    }
}
