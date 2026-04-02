using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Collections.Unicode;

[RequireComponent(typeof(Obtainer))]
public class QuestRunner: MonoBehaviour, IObtainObserver, IQuestRunner
{
	Dictionary<QuestIds, QuestContext> quests;
	Dictionary<QuestIds, bool> isQuestCompleted;

    IObtainer obtainer;
	IInventory inventory;

	void Start()
	{
		obtainer = GetComponent<Obtainer>();
		inventory = GetComponent<Inventory>();

		obtainer?.Subscribe(this);
	}
	void IObtainObserver.Update(ItemIds id, int cnt) => UpdateWhetherQuestComplete();
	bool IQuestRunner.HasItem(ItemIds id) => inventory.HasItem(id);
	int IQuestRunner.GetItemCount(ItemIds id) => inventory.GetItemCount(id);
	void IQuestRunner.AddItem(ItemIds id, int cnt) => inventory.AddItem(id, cnt);
	void IQuestRunner.CompleteQuest(QuestIds questID)
	{
		var context = SOLoader<QuestIds, QuestSO>.Instance.GetSO(questID).ToContext();
		
		foreach (var ctx in context.rewardContexts)
		{
            QuestRewardService.Give(this, ctx);
		}
	}
	void IQuestRunner.AddQuest(QuestIds questID)
	{
		var context = SOLoader<QuestIds, QuestSO>.Instance.GetSO(questID).ToContext();

		quests.Add(questID, context);

		UpdateWhetherQuestComplete();
	}

	private void UpdateWhetherQuestComplete()
    {
		foreach(var questContext in quests.Values)
		{
			bool flag = true;

            foreach (var ctx in questContext.conditionContexts)
			{
				if (QuestConditionService.IsMet(this, ctx))
				{
					flag = false;
					break;
				}
			}
			isQuestCompleted[questContext.id] = flag;
        }
    }
	~QuestRunner()
	{
		obtainer?.Unsubscribe(this);
	}

}
