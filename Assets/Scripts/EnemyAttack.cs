using UnityEngine;



   public class EnemyAttack : MonoBehaviour
    {
        [Header("Réglages Attaque")]
        [SerializeField] private int m_Damage = 1;
        [SerializeField] private float m_AttackCooldown = 1.5f; 
        
        private float m_NextAttackTime;

        
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (Time.time >= m_NextAttackTime)
                {
                    Health playerHealth = other.GetComponent<Health>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(m_Damage);
                        m_NextAttackTime = Time.time + m_AttackCooldown;
                        Debug.Log("L'ennemi a frappé le joueur !");
                    }
                }
            }
        }
    }
