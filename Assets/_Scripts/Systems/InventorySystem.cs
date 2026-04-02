using System.Collections.Generic;
using UnityEngine;


public class InventorySystem: MonoBehaviour, IInventory
{
	Dictionary<ItemIds, int> datas = new Dictionary<ItemIds, int>();

	void IInventory.ObtainItem(ItemIds id, int cnt)
	{
		if(datas.ContainsKey(id))
		{
            datas[id] += cnt;
		}
		else
		{
			datas.Add(id, cnt);
		}

		var ev = new InventoryChangedEvent(
			itemId: id,
			reason: InventoryChangeReason.Added,
			deltaCnt: cnt
			);

		EventBus.Publish(ev);
    }
	bool IInventory.CheckHasItem(ItemIds id)
	{
		return datas.ContainsKey(id);
	}
	int IInventory.GetItemCount(ItemIds id)
	{
		if(datas.ContainsKey(id))
		{
			return datas[id];
		}
		return 0;
	}
}
