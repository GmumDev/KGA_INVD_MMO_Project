using System.Collections.Generic;
using UnityEngine;


public class Inventory: MonoBehaviour, IInventory
{
	Dictionary<ItemIds, int> datas = new Dictionary<ItemIds, int>();

	void IInventory.AddItem(ItemIds id, int cnt)
	{
		if(datas.ContainsKey(id))
		{

            datas[id] += cnt;
		}
		else
		{
			datas.Add(id, cnt);
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
			return datas[id];
		}
		return 0;
	}

}
