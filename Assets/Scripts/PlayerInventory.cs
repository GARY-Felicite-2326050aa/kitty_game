using UnityEngine;
using UnityEngine.Events;

   public class PlayerInventory : MonoBehaviour
    {
        public int cookieCount = 0;
        public int maxCookies = 15;

        [Header("Événements d'UI (Lier au Canvas)")]
        // Pour afficher 0/15 au départ
        public UnityEvent<int, int> onCookieUIInit; 
        // Pour mettre à jour pendant le jeu
        public UnityEvent<int, int> onCookieCountChanged;

        [Header("Événements de Progression")]
        public UnityEvent onAllCookiesCollected;

        [Header("Audio")]
        public AudioSource audioSource;
        public AudioClip coinSound;

        private void Start()
        {
            // Initialisation de l'affichage au lancement
            if (onCookieUIInit != null)
            {
                onCookieUIInit.Invoke(cookieCount, maxCookies);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // --- CAS 1 : C'EST UN COOKIE ---
            if (other.CompareTag("Cookie"))
            {
                // Son
                if (audioSource != null && coinSound != null)
                    audioSource.PlayOneShot(coinSound);
                
                // Score
                cookieCount++;
                onCookieCountChanged.Invoke(cookieCount, maxCookies);

                // Check Victoire/Progression
                if (cookieCount >= maxCookies) 
                {
                    onAllCookiesCollected.Invoke();
                }

                // On désactive l'objet et on s'arrête là
                other.gameObject.SetActive(false);
                return; 
            }

            // --- CAS 2 : C'EST UN OBJET RAMASSABLE (Clé, etc.) ---
            // On cherche si l'objet a un script qui hérite de "Pickable"
            Pickable item = other.GetComponent<Pickable>();
            if (item != null)
            {
                // On appelle la fonction PickUp() définie dans KeyPickable
                item.PickUp();
            }
        }
    }