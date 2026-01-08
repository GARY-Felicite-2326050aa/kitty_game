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
            startPosition = transform.position; 
            wanderTimer = idleWaitTime;         
            
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        void Update()
        {
            if (player == null) return;

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
               
                agent.SetDestination(player.position);
               
                wanderTimer = 0; 
            }
            else
            {
                
                if (canWander)
                {
                    HandleWandering();
                }
                else
                {
                   
                    if (agent.hasPath) agent.ResetPath();
                }
            }
        }

        private void HandleWandering()
        {
            
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                wanderTimer += Time.deltaTime;

               
                if (wanderTimer >= idleWaitTime)
                {
                    Vector3 newPos = GetRandomPoint(startPosition, wanderRadius);
                    agent.SetDestination(newPos);
                    wanderTimer = 0;
                }
            }
        }


        private Vector3 GetRandomPoint(Vector3 center, float radius)
        {
            Vector3 randomDir = Random.insideUnitSphere * radius;
            randomDir += center;
            
            NavMeshHit hit;
           
            if (NavMesh.SamplePosition(randomDir, out hit, radius, 1))
            {
                return hit.position;
            }
            
            return center;
        }

        
        private void OnDrawGizmosSelected()
        {
           
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRange);

           
            if (Application.isPlaying)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(startPosition, wanderRadius);
            }
        }
    }