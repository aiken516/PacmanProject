using UnityEngine;

public class ExtraBoomItem : Item
{
    public override void GetItem(PlayerItem playerItem)
    {
        playerItem.GetExtraBoom();
    }
}
