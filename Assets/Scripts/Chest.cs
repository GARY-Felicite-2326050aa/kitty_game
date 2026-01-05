using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Chest : MonoBehaviour
{
    public Animator animator;
    public float openDelay = 1f; // durée animation

    public UnityEvent onDoorOpened;

    private bool isUnlocked = false;
    private bool isOpened = false;

    public void Unlock()
    {
        isUnlocked = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isUnlocked || isOpened) return;

        if (other.CompareTag("Player"))
        {
            isOpened = true;
            animator.SetBool("Open", true);
            StartCoroutine(OpenDoorSequence());
        }
    }

    private IEnumerator OpenDoorSequence()
    {
        yield return new WaitForSeconds(openDelay);
        onDoorOpened.Invoke();
    }
}
