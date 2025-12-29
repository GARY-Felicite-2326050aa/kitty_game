using TMPro;
using UnityEngine;

public class Game2playUIManager : MonoBehaviour
{
    public TextMeshProUGUI cookieText;
    public int maxCookies = 15;

    public void UpdateCookieUI(int currentCookies)
    {
        cookieText.text = "Cookies : " + currentCookies + " / " + maxCookies;
    }
}
