using UnityEngine;
using System.Collections.Generic;


    // Ce script reste sur l'objet Parent (le Pivot)
    public class CenterSetter : MonoBehaviour
    {
        [Header("Cibles")]
        public Transform player;
        public List<Transform> enemies = new List<Transform>(); 

        [Header("Réglages de Suivi")]
        public float heightOffset = 1.2f;   // Hauteur (ajustée pour un chat)
        public float lerpSpeed = 10f;       
        public float maxShiftFromPlayer = 2f; // LIMITE : le pivot ne s'éloigne pas de plus de 2m du chat

        void LateUpdate()
        {
            if (player == null) return;

            // 1. On calcule la position moyenne théorique (Joueur + Ennemis)
            Vector3 combinedPosition = player.position;
            int count = 1;

            if (enemies != null && enemies.Count > 0)
            {
                foreach (Transform enemy in enemies)
                {
                    if (enemy != null)
                    {
                        combinedPosition += enemy.position;
                        count++;
                    }
                }
            }

            Vector3 theoreticalCenter = combinedPosition / count;

            // 2. CONTRAINTE : On empêche le pivot de trop s'éloigner du chat
            Vector3 directionToCenter = theoreticalCenter - player.position;
            
            // Si le milieu est trop loin, on le "bride" à maxShiftFromPlayer
            if (directionToCenter.magnitude > maxShiftFromPlayer)
            {
                directionToCenter = directionToCenter.normalized * maxShiftFromPlayer;
            }

            Vector3 finalTargetPos = player.position + directionToCenter;
            finalTargetPos.y = player.position.y + heightOffset;

            // 3. Application fluide
            transform.position = Vector3.Lerp(transform.position, finalTargetPos, Time.deltaTime * lerpSpeed);
        }

        public void AddEnemy(Transform newEnemy) => enemies.Add(newEnemy);
        public void RemoveEnemy(Transform enemyToRemove) => enemies.Remove(enemyToRemove);
    }
