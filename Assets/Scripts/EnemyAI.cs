using UnityEngine;
using UnityEngine.AI;

    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAI : MonoBehaviour
    {
        [Header("Détection du Joueur")]
        public float detectionRange = 12f;
        
        [Header("Patrouille (Repos)")]
        public bool canWander = true;        // Active le mouvement quand le joueur est loin
        public float wanderRadius = 5f;     // Rayon de la zone de promenade
        public float idleWaitTime = 3f;      // Temps d'attente entre deux déplacements

        private NavMeshAgent agent;
        private Transform player;
        private Vector3 startPosition;
        private float wanderTimer;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            startPosition = transform.position; // On mémorise le centre de sa zone
            wanderTimer = idleWaitTime;         // Prêt à bouger au début
            
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        void Update()
        {
            if (player == null) return;

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                // ÉTAT 1 : CHASSE - On poursuit le joueur
                agent.SetDestination(player.position);
                // On réinitialise le timer de patrouille pour plus tard
                wanderTimer = 0; 
            }
            else
            {
                // ÉTAT 2 : REPOS / PATROUILLE
                if (canWander)
                {
                    HandleWandering();
                }
                else
                {
                    // Si on ne patrouille pas, on s'arrête simplement
                    if (agent.hasPath) agent.ResetPath();
                }
            }
        }

        private void HandleWandering()
        {
            // On vérifie si l'agent est arrivé à sa destination actuelle
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                wanderTimer += Time.deltaTime;

                // Si on a attendu assez longtemps, on choisit un nouveau point
                if (wanderTimer >= idleWaitTime)
                {
                    Vector3 newPos = GetRandomPoint(startPosition, wanderRadius);
                    agent.SetDestination(newPos);
                    wanderTimer = 0;
                }
            }
        }

        // Calcule un point aléatoire valide sur le NavMesh
        private Vector3 GetRandomPoint(Vector3 center, float radius)
        {
            Vector3 randomDir = Random.insideUnitSphere * radius;
            randomDir += center;
            
            NavMeshHit hit;
            // On cherche le point le plus proche sur le NavMesh pour éviter les erreurs
            if (NavMesh.SamplePosition(randomDir, out hit, radius, 1))
            {
                return hit.position;
            }
            
            return center; // Retourne au centre en cas d'échec
        }

        // Visualisation dans l'éditeur
        private void OnDrawGizmosSelected()
        {
            // Zone de détection (Jaune)
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRange);

            // Zone de patrouille (Bleu) - seulement si on connaît la position de départ
            if (Application.isPlaying)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(startPosition, wanderRadius);
            }
        }
    }