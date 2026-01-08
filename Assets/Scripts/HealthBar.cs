using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        // On calcule le pourcentage (0 à 1)
        slider.value = (float)currentHealth / (float)maxHealth;
    }
}