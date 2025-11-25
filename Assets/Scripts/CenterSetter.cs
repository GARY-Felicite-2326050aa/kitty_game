using UnityEngine;

public class CenterSetter : MonoBehaviour
{
    public Transform player;
    public Transform otherObject;

    void Update()
    {
        Vector3 center = (player.position + otherObject.position) / 2;
        center.y = player.position.y;
        transform.position = center;
    }
}
