using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int cookieCount = 0;
    public int maxCookies = 15;

    public UnityEvent<int> onCookieCountChanged;
    public UnityEvent onAllCookiesCollected;

    [Header("Audio")]
    public AudioSource audioSource; // Le haut-parleur
    public AudioClip coinSound;     // Le fichier audio "coin"

    private void OnTriggerEnter(Collider other)
    {
        Pickable pickable = other.GetComponent<Pickable>();

        if (pickable != null)
        {

            if (audioSource != null && coinSound != null)
            {
                audioSource.PlayOneShot(coinSound);
            }
            cookieCount++;
            onCookieCountChanged.Invoke(cookieCount);

            pickable.PickUp();

            if (cookieCount >= maxCookies)
            {
                onAllCookiesCollected.Invoke();
            }
        }
    }
}
