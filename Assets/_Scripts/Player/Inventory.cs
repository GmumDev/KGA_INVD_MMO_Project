using System.Collections.Generic;
using UnityEngine;


public class Inventory: IInventory
{
	Dictionary<string, InventoryCell> datas = new Dictionary<string, InventoryCell>();

	void IInventory.AddItem(string id, ItemData item, int cnt)
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
	bool IInventory.HasItem(string id)
	{
		return datas.ContainsKey(id);
	}
	int IInventory.GetItemCount(string id)
	{
		if(datas.ContainsKey(id))
		{
			return datas[id].Count;
		}
		return 0;
	}

}
