using System.Collections.Generic;
using UnityEngine;
using static QuestManager;

public interface IQuestManager
{
    Dictionary<QuestIds, QuestState> QuestStates { get; }
    void CompleteQuest(QuestIds questID);
	void AbandonQuest(QuestIds questID);
	void AcceptQuest(QuestIds questID);
}
