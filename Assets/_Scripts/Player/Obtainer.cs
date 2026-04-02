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
    void IObtainer.Obtain(ItemIds id, int cnt)
	{
        if (inventory == null) return;
        inventory.ObtainItem(id, cnt);
	}
}
