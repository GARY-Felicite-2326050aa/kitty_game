using UnityEngine;

public class Chest : MonoBehaviour
{
    public Animator animator;
    private bool isUnlocked = false;

    public void Unlock()
    {
        isUnlocked = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isUnlocked) return;

        if (other.CompareTag("Player"))
        {
            animator.SetBool("Open", true);
        }
    }
}
