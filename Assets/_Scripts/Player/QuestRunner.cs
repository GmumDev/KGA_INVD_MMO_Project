using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestRunner: IObtainObserver, IQuestRunner
{
    List<Quest> questCompleted = new List<Quest>();
	List<Quest> questNotCompleted = new List<Quest>();

    IObtainer obtainer;
	IInventory inventory;

	public QuestRunner(IObtainer obtainer, IInventory inventory)
	{
		obtainer?.Subscribe(this);
	}
	void IObtainObserver.Update(ItemData item, int cnt) => UpdateWhetherQuestComplete();
	bool IQuestRunner.HasItem(string id) => inventory.HasItem(id);
	int IQuestRunner.GetItemCount(string id) => inventory.GetItemCount(id);
	void IQuestRunner.AddItem(string id, ItemData item, int cnt) => inventory.AddItem(id, item, cnt);
	void IQuestRunner.CompleteQuest(Quest quest)
	{
		var complete = questCompleted.Remove(quest);
		if (complete == false)
		{
			return;
		}

		quest.GetRewards(this);
	}
	void IQuestRunner.AddQuest(Quest quest)
	{
		questNotCompleted.Add(quest);
		UpdateWhetherQuestComplete();
	}

	private void UpdateWhetherQuestComplete()
	{
		foreach(var quest in questNotCompleted)
		{
			if(quest.IsComplete(this))
			{
				questNotCompleted.Remove(quest);
				questCompleted.Add(quest);
			}
		}
	}
	~QuestRunner()
	{
		obtainer?.Describe();
	}

}
