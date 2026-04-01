using UnityEngine;

public interface IInventory
{
    public bool HasItem(ItemIds id);
    public int GetItemCount(ItemIds id);
    public void AddItem(ItemIds id, ItemData item, int cnt);
}
