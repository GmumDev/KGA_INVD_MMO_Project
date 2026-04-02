using UnityEngine;

public interface IQuestManager
{
	void CompleteQuest(QuestIds questID);
	void AbandonQuest(QuestIds questID);
	void AcceptQuest(QuestIds questID);
}
