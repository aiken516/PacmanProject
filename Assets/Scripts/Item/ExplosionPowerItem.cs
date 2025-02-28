using UnityEngine;

public class ExplosionPowerItem : Item
{
    public override void GetItem(PlayerItem playerItem)
    {
        playerItem.GetExplosionPower();
    }
}
