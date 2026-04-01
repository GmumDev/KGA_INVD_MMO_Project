using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Obtainer))]
public class QuestRunner: MonoBehaviour, IObtainObserver, IQuestRunner
{
    List<Quest> questCompleted = new List<Quest>();
	List<Quest> questNotCompleted = new List<Quest>();

    IObtainer obtainer;

	IInventory inventory;

	void Start()
	{
		obtainer = GetComponent<Obtainer>();
		inventory = GetComponent<Inventory>();

		obtainer?.Subscribe(this);
	}
	void IObtainObserver.Update(ItemData item, int cnt) => UpdateWhetherQuestComplete();
	bool IQuestRunner.HasItem(ItemIds id) => inventory.HasItem(id);
	int IQuestRunner.GetItemCount(ItemIds id) => inventory.GetItemCount(id);
	void IQuestRunner.AddItem(ItemIds id, ItemData item, int cnt) => inventory.AddItem(id, item, cnt);
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
		obtainer?.Unsubscribe(this);
	}

}
