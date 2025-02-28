using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    public void GetExtraBoom()
    {
        GetComponent<PlayerBoom>().BoomMaxCount += 1;
    }

    public void GetExplosionPower()
    {
        if (GetComponent<PlayerBoom>().BoomPower < 8)
        {
            GetComponent<PlayerBoom>().BoomPower += 1;
        }
    }
}
