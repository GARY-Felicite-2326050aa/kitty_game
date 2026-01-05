using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Transform exitPoint;

    public void Teleport(GameObject player)
    {
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        player.transform.position = exitPoint.position;

        if (cc != null) cc.enabled = true;
    }
}
