using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class Obtainer: MonoBehaviour, IObtainer
{
    List<IObtainObserver> observers = new List<IObtainObserver>();	// Finish before Awake call

    private IInventory inventory;
	private void Start()
	{
		inventory = GetComponent<Inventory>();
	}
	void IObtainer.Subscribe(IObtainObserver observer)
    {
        observers.Add(observer);
    }

    void IObtainer.Unsubscribe(IObtainObserver observer)
    {
        observers.Remove(observer);
    }
    void IObtainer.Obtain(ItemData item, int cnt)
	{
        if (inventory == null) return;
        inventory.AddItem(item.id, item, cnt);

		NotifyObservers(item, cnt);
	}
	private void NotifyObservers(ItemData item, int cnt = 1)
	{
		for (int i = 0; i < observers.Count; i++)
		{
			observers[i].Update(item, cnt);
		}
	}
}
