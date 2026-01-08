using UnityEngine;


    public class CameraMovementOrbital : MonoBehaviour
    {
        [Header("Cibles")]
        public Transform target; // L'objet parent qui a le CenterSetter

        [Header("Réglages de Distance")]
        public float distance = 6f;          
        public float minDistance = 1.0f;    
        public float smoothSpeed = 10f;     
        public float cushion = 0.3f;        

        [Header("Vitesse Rotation")]
        public float xSpeed = 120f;
        public float ySpeed = 120f;

        [Header("Collision & Sécurité")]
        public LayerMask collisionLayers;    // Sélectionne "Terrain"
        public float sphereRadius = 0.2f;    // Épaisseur de détection (évite de passer à travers l'herbe)
        public float yMinLimit = 5f;         // Empêche de descendre sous le chat (minimum 5 au lieu de -20)
        public float yMaxLimit = 80f;

        private float x = 0f;
        private float y = 0f;
        private float currentDistance;

        void Start()
        {
            Vector3 angles = transform.eulerAngles;
            x = angles.y;
            y = angles.x;
            currentDistance = distance;
        }

        void LateUpdate()
        {
            if (target == null) return;

            // 1. Récupération des mouvements souris
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
            
            // On bride l'angle Y : 5 évite de passer sous le sol, 80 évite de se retourner
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            // 2. Calcul de la direction et détection d'obstacle avec SphereCast
            Vector3 dir = rotation * new Vector3(0, 0, -distance);
            RaycastHit hit;
            float targetDistance = distance;

            // On utilise SphereCast (une "boule") pour ne pas passer entre les hexagones
            if (Physics.SphereCast(target.position, sphereRadius, dir.normalized, out hit, distance, collisionLayers))
            {
                targetDistance = Mathf.Clamp(hit.distance - cushion, minDistance, distance);
            }

            // 3. Lissage de la distance
            currentDistance = Mathf.Lerp(currentDistance, targetDistance, Time.deltaTime * smoothSpeed);

            // 4. Calcul de la position finale
            Vector3 finalPosition = (rotation * new Vector3(0, 0, -currentDistance)) + target.position;

            // --- SÉCURITÉ SOL ULTIME ---
            // Si malgré tout la caméra veut descendre plus bas que le chat, on la bloque
            if (finalPosition.y < target.position.y + 0.1f)
            {
                finalPosition.y = target.position.y + 0.1f;
            }

            // 5. Application
            transform.rotation = rotation;
            transform.position = finalPosition;
        }
    }
