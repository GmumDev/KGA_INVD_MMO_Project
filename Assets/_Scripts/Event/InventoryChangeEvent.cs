using UnityEngine;

public struct InventoryChangeEvent
{
    public ItemIds itemId;
	public InventoryChangeReason reason;
	public int deltaCnt;

	public InventoryChangeEvent(
		ItemIds itemId,
		InventoryChangeReason reason, 
		int deltaCnt)
	{
		this.itemId = itemId;
		this.reason = reason;
		this.deltaCnt = deltaCnt;
	}
}
