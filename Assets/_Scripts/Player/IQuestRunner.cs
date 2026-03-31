using UnityEngine;

public interface IQuestRunner
{
	public bool HasItem(ItemIds id);
	public int GetItemCount(ItemIds id);
	public void AddItem(ItemIds id, ItemData item, int cnt);
	public void CompleteQuest(Quest quest);
	public void AddQuest(Quest quest);
}
