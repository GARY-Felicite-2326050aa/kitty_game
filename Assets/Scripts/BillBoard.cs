using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        // Force l'objet à regarder la caméra
        transform.LookAt(transform.position + cam.forward);
    }
}