using UnityEngine;

public interface IQuestRunner
{
	public bool HasItem(ItemIds id);
	public int GetItemCount(ItemIds id);
	public void AddItem(ItemIds id, int cnt);
	public void CompleteQuest(QuestIds questID);
	public void AddQuest(QuestIds questID);
}
