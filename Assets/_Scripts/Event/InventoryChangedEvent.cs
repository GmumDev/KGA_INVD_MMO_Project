using UnityEngine;

public struct InventoryChangedEvent
{
    public ItemIds itemId;
	public InventoryChangeReason reason;
	public int deltaCnt;

	public InventoryChangedEvent(
		ItemIds itemId,
		InventoryChangeReason reason, 
		int deltaCnt)
	{
		this.itemId = itemId;
		this.reason = reason;
		this.deltaCnt = deltaCnt;
	}
}
