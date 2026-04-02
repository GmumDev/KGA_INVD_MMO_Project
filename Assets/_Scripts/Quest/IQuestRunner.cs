using UnityEngine;

public interface IQuestRunner
{
	void CompleteQuest(QuestIds questID);
	void AbandonQuest(QuestIds questID);
	void AcceptQuest(QuestIds questID);
}
