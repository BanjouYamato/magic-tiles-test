using UnityEngine;

public class NormalTilePool : BaseTilePool<TileMover>
{
    public override void ResetPool()
    {
        foreach (Transform t in transform)
        {
            TileMover mover = t.GetComponent<TileMover>();
            ReturnToPool(mover);
        }
    }
    protected override void ResetObject(TileMover obj)
    {
        float width = canvas.sizeDelta.x / 4;
        float heigh = canvas.sizeDelta.y / 2;
        Vector2 size = new Vector2(width, width * 1.6f);
        obj.SetPool(this, size, heigh);
    }
}
