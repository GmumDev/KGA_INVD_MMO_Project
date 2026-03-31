using UnityEngine;

public interface IInventory
{
    public bool HasItem(string id);
    public int GetItemCount(string id);
    public void AddItem(string id, ItemData item, int cnt);
}
