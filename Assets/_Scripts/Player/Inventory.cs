using System.Collections.Generic;
using UnityEngine;


public class Inventory: MonoBehaviour, IInventory
{
	Dictionary<ItemIds, InventoryCell> datas = new Dictionary<ItemIds, InventoryCell>();

	void IInventory.AddItem(ItemIds id, ItemData item, int cnt)
	{
		if(datas.ContainsKey(id))
		{
			InventoryCell cell = datas[id];

			cell.Count += cnt;
			
			datas[id] = cell;
		}
		else
		{
			datas.Add(id, new InventoryCell(item, cnt));
		}
	}
	bool IInventory.HasItem(ItemIds id)
	{
		return datas.ContainsKey(id);
	}
	int IInventory.GetItemCount(ItemIds id)
	{
		if(datas.ContainsKey(id))
		{
			return datas[id].Count;
		}
		return 0;
	}

}
