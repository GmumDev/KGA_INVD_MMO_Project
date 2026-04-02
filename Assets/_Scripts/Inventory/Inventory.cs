using System.Collections.Generic;
using UnityEngine;


public class Inventory: MonoBehaviour, IInventory, ISubject<InventoryChangeEvent>
{
	Dictionary<ItemIds, int> datas = new Dictionary<ItemIds, int>();
	List<IObserver<InventoryChangeEvent>> observers = new List<IObserver<InventoryChangeEvent>>();  // Finish before Awake call

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

		NotifyObservers(new InventoryChangeEvent(
			itemId: id,
			reason: InventoryChangeReason.Added,
			deltaCnt: cnt
			));
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


	void NotifyObservers(InventoryChangeEvent ctx)
	{
		for (int i = 0; i < observers.Count; i++)
		{
			observers[i].Update(ctx);
		}
	}
	void ISubject<InventoryChangeEvent>.Subscribe(IObserver<InventoryChangeEvent> observer)
	{
		observers.Add(observer);
	}

	void ISubject<InventoryChangeEvent>.Unsubscribe(IObserver<InventoryChangeEvent> observer)
	{
		observers.Remove(observer);
	}
}
