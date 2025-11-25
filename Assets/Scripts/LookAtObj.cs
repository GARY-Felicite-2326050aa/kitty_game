using UnityEngine;

public class LookAtObj : MonoBehaviour
{
    public Transform target; 

    void LateUpdate()
    {
        if (target != null)
            transform.LookAt(target);
    }
}
