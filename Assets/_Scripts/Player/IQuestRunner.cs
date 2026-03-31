using UnityEngine;

public interface IQuestRunner
{
	public bool HasItem(string id);
	public int GetItemCount(string id);
	public void AddItem(string id, ItemData item, int cnt);
	public void CompleteQuest(Quest quest);
	public void AddQuest(Quest quest);
}
