using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public GameObject keyObject;
    public AudioClip spawnSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SpawnKey()
    {
        keyObject.SetActive(true);
        audioSource.PlayOneShot(spawnSound);
    }
}
