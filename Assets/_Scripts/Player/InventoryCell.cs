using UnityEngine;

public struct InventoryCell
{
    public ItemData Item;
    public int Count;
    public InventoryCell(ItemData item, int count)
    {
        this.Item = item;
        this.Count = count;
    }
}
