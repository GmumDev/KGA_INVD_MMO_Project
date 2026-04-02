using UnityEngine;

public class InventoryChangedEvent
{
    public ItemIds itemId;
	public int deltaCnt;

	public InventoryChangedEvent(
		ItemIds itemId,
		int deltaCnt)
	{
		this.itemId = itemId;
		this.deltaCnt = deltaCnt;
	}
}
