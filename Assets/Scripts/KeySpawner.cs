using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public GameObject keyObject;

    public void SpawnKey()
    {
        keyObject.SetActive(true);
    }
}
