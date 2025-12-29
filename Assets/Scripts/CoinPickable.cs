using UnityEngine;

public class CoinPickable : Pickable
{
    public override void PickUp()
    {
        Destroy(gameObject);
    }
}
