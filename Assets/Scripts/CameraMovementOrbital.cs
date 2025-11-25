using UnityEngine;

public class CameraMovementOrbital : MonoBehaviour
{
    public Transform target; // le joueur
    public float distance = 6f;
    public float xSpeed = 120f;
    public float ySpeed = 120f;

    private float x = 0f;
    private float y = 0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        if(target == null) return;

        x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
        y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
        y = Mathf.Clamp(y, -20, 80);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0, 0, -distance) + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }
}
