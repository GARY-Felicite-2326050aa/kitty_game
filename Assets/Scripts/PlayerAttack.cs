using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 5;
    public float attackRadius = 5.0f; // On augmente le rayon pour être sûr
    public LayerMask enemyLayer; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            PerformAttack();
        }
    }

    private void PerformAttack()
    {
        Debug.Log("--- Attaque lancée ! ---");
        
        // On crée la sphère
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRadius, enemyLayer);
        
        Debug.Log("Colliders détectés dans le layer : " + hitEnemies.Length);

        foreach (Collider enemy in hitEnemies)
        {
            // On cherche le Health sur l'ennemi OU ses parents
            Health enemyHealth = enemy.GetComponentInParent<Health>();
            
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
                Debug.Log("TOUCHÉ : " + enemy.name + " | Vie restante envoyée.");
            }
            else
            {
                Debug.Log("Objet touché (" + enemy.name + ") mais il n'a pas de script Health !");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}