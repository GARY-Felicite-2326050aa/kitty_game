using UnityEngine;
using UnityEngine.Events;

public class KeyPickable : Pickable
{
    public UnityEvent onKeyPicked;

    public override void PickUp()
    {
        onKeyPicked.Invoke();
        Destroy(gameObject);
    }
}
