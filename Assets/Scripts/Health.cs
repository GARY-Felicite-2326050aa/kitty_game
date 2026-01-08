using UnityEngine;
using UnityEngine.Events;
public class Health : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;
    private bool isDead = false; // Sécurité

    public UnityEvent<int, int> onHealthChanged;
    public UnityEvent OnDeath;

    void Start() { currentHealth = maxHealth; 
    if (onHealthChanged != null)
    {
        onHealthChanged.Invoke(currentHealth, maxHealth);
    }}

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Si déjà mort, on ignore

        currentHealth -= damage;
        onHealthChanged.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            isDead = true; // On marque comme mort
            Die();
        }
    }

    private void Die()
    {
        OnDeath.Invoke();
        if (CompareTag("Enemy"))
        {
            GameplayManager gm = Object.FindFirstObjectByType<GameplayManager>();
            if (gm != null) gm.OnEnemyKilled();
        }
        Destroy(gameObject, 0.1f);
    }
}