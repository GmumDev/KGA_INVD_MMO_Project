using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(InventorySystem))]
public class ItemObtainer: MonoBehaviour, IItemObtainer
{

    private IInventory inventory;
	private void Start()
	{
		inventory = GetComponent<InventorySystem>();
	}
    void IItemObtainer.Obtain(ItemIds id, int cnt)
	{
        if (inventory == null) return;
        inventory.ObtainItem(id, cnt);
	}
}
