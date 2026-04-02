using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Collections.Unicode;

[RequireComponent(typeof(Obtainer))]
public class QuestRunner : MonoBehaviour, IObserver<InventoryChangeEvent>, IQuestRunner, IQuestRewardEarner
{
	private class QuestProgressContext
	{
		public QuestContext questCtx;
		public List<QuestConditionProgress> conditionProgress;
		public bool isComplete;
	}
	Dictionary<QuestIds, QuestProgressContext> questProgress = new Dictionary<QuestIds, QuestProgressContext>();

	ISubject<InventoryChangeEvent> inventorySubject;
	IInventory inventory;

	void Start()
	{
		var tmpInven = GetComponent<Inventory>();
		inventorySubject = tmpInven;
		inventory = tmpInven;
		tmpInven = null;

		inventorySubject?.Subscribe(this);
	}
	void IQuestRunner.CompleteQuest(QuestIds questID)
	{
		if (questProgress.ContainsKey(questID) == false) return;
		if (questProgress[questID].isComplete == false)
		{
			// not clear yet. // 위(클라UI)에서 거르고 들어오기. // 이 블록 지우기
			return;
		}

		foreach (var ctx in questProgress[questID].questCtx.rewardContexts)
		{
			QuestRewardService.Give(this, ctx);
		}

		questProgress.Remove(questID);
	}
	void IQuestRunner.AbandonQuest(QuestIds questID)
	{
		if (questProgress.ContainsKey(questID) == false) return;

		questProgress.Remove(questID);
	}
	void IQuestRunner.AcceptQuest(QuestIds questID)
	{
		if (questProgress.ContainsKey(questID)) return;

		var context = SOLoader<QuestIds, QuestSO>.Instance.GetSO(questID).ToContext();
		List<QuestConditionProgress> conditionProgresses = new List<QuestConditionProgress>();
		foreach(var ctx in context.conditionContexts)
		{
			switch(ctx.type)
			{
				case QuestConditionType.Obtain:
					conditionProgresses.Add(QuestConditionProgress.GetObtainConditionProgress(ctx.itemID));
					break;
				case QuestConditionType.Kill:
					conditionProgresses.Add(QuestConditionProgress.GetKillConditionProgress(ctx.enemyId));
					break;
			}
		}
		QuestProgressContext questProgressContext = new QuestProgressContext();
		questProgressContext.questCtx = context;
		questProgressContext.conditionProgress = conditionProgresses;
		questProgressContext.isComplete = false;
		questProgress.Add(questID, questProgressContext);
	}

	// Quest Obtain-Condition Update
	void IObserver<InventoryChangeEvent>.Update(InventoryChangeEvent ev)
	{
		if (ev.reason != InventoryChangeReason.Added) return;

		Dictionary<QuestIds, Queue<int>> changedConditionProgressIdxs = new Dictionary<QuestIds, Queue<int>>();

		foreach (var questId in questProgress.Keys)
		{
			for (int i = 0; i < questProgress[questId].conditionProgress.Count; i++)
			{
				if (ev.itemId != questProgress[questId].conditionProgress[i].itemID) continue;

				questProgress[questId].conditionProgress[i].itemCount += ev.deltaCnt;

				if (changedConditionProgressIdxs.ContainsKey(questId) == false)
					changedConditionProgressIdxs.Add(questId, new Queue<int>());
				changedConditionProgressIdxs[questId].Enqueue(i);
			}
		}
		foreach (var questId in changedConditionProgressIdxs.Keys)
		{
			UpdateConditionProgresses(questId, changedConditionProgressIdxs[questId]);
		} 
	}
	

	void IQuestRewardEarner.EarnItemReward(ItemIds id, int cnt) => inventory.ObtainItem(id, cnt);
	void IQuestRewardEarner.EarnGoldReward(int cnt) => throw new System.NotImplementedException();
	void IQuestRewardEarner.EarnExpReward(int cnt) => throw new System.NotImplementedException();
	void UpdateConditionProgresses(QuestIds questId, Queue<int> que)
	{
		bool flag = true;
		while (flag && que.Count > 0)
		{
			int i = que.Dequeue();

			var goalCtx = questProgress[questId].questCtx.conditionContexts[i];
			var nowCtx = questProgress[questId].conditionProgress[i];
			flag = QuestConditionService.IsMet(this, goalCtx, nowCtx);
		}
		questProgress[questId].isComplete = flag;
	}


	void OnDestroy()
	{
		inventorySubject?.Unsubscribe(this);
	}

}
