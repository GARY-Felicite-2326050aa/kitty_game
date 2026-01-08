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

            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
            
           

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            Vector3 dir = rotation * new Vector3(0, 0, -distance);
            RaycastHit hit;
            float targetDistance = distance;

          
            if (Physics.SphereCast(target.position, sphereRadius, dir.normalized, out hit, distance, collisionLayers))
            {
                targetDistance = Mathf.Clamp(hit.distance - cushion, minDistance, distance);
            }

            currentDistance = Mathf.Lerp(currentDistance, targetDistance, Time.deltaTime * smoothSpeed);

        
            Vector3 finalPosition = (rotation * new Vector3(0, 0, -currentDistance)) + target.position;

           
            if (finalPosition.y < target.position.y + 0.1f)
            {
                finalPosition.y = target.position.y + 0.1f;
            }

           
            transform.rotation = rotation;
            transform.position = finalPosition;
        }
    }
