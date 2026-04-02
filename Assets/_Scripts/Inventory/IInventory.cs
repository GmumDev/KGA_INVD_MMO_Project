using UnityEngine;

public interface IInventory
{
    public bool CheckHasItem(ItemIds id);
    public int GetItemCount(ItemIds id);
    public void ObtainItem(ItemIds id, int cnt);
}
