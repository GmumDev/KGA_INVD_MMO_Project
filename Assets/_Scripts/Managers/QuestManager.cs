
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour, IQuestManager, IQuestRewardEarner
{
    class QuestState
    {
        public QuestContext data;
        public List<QuestConditionProgress> progress;
        public bool isCompleted;
    }
    Dictionary<QuestIds, QuestState> questStates = new Dictionary<QuestIds, QuestState>();

	IInventory inventory;

	List<SubscriptionToken> subscriptionTokens = new List<SubscriptionToken>();

	QuestRewardService rewardService = new QuestRewardService();

    void Start()
	{
        inventory = GetComponent<InventorySystem>();

		subscriptionTokens.Add(EventBus.Subscribe<InventoryChangedEvent>(OnInventoryChanged));
	}
	void IQuestManager.CompleteQuest(QuestIds questID)
	{
		if (questStates.ContainsKey(questID) == false) return;
		if (questStates[questID].isCompleted == false) return;

        foreach (var ctx in questStates[questID].data.rewardContexts)
		{
            rewardService.Give(this, ctx);
		}

        questStates.Remove(questID);
	}
	void IQuestManager.AbandonQuest(QuestIds questID)
	{
		if (questStates.ContainsKey(questID) == false) return;

        questStates.Remove(questID);
	}
	void IQuestManager.AcceptQuest(QuestIds questID)
	{
		if (questStates.ContainsKey(questID)) return;

		var context = SOLoader<QuestIds, QuestSO>.Instance.GetSO(questID).ToContext();
		List<QuestConditionProgress> _progresses = new List<QuestConditionProgress>();
		foreach(var ctx in context.conditionContexts)
		{
			switch(ctx.type)
			{
				case QuestConditionType.Obtain:
                    _progresses.Add(QuestConditionProgress.GetObtainConditionProgress(ctx.itemID));
					break;
				case QuestConditionType.Kill:
                    _progresses.Add(QuestConditionProgress.GetKillConditionProgress(ctx.enemyId));
					break;
			}
		}
        QuestState _questState = new QuestState();
        _questState.data = context;
		_questState.progress = _progresses;
        _questState.isCompleted = false;
		questStates.Add(questID, _questState);
	}

	//
	void OnInventoryChanged(InventoryChangedEvent ev)
	{
		if (ev.reason != InventoryChangeReason.Added) return;

        foreach (var _questState in questStates.Values)
        {
            foreach (var _progress in _questState.progress)
            {
                if (_progress.itemID != ev.itemId) continue;

                _progress.itemCount += ev.deltaCnt;
            }

            UpdateConditionProgress(_questState);
        }
	}
	

	void IQuestRewardEarner.EarnItemReward(ItemIds id, int cnt) => inventory.ObtainItem(id, cnt);
	void IQuestRewardEarner.EarnGoldReward(int cnt) => throw new System.NotImplementedException();
	void IQuestRewardEarner.EarnExpReward(int cnt) => throw new System.NotImplementedException();
	void UpdateConditionProgress(QuestState quest)
	{
        bool allMet = true;

		for(int i = 0; i < quest.progress.Count; i++)
        {
			var condition = quest.data.conditionContexts[i];
			var progress = quest.progress[i];

            if (!QuestConditionService.IsMet(this, condition, progress))
            {
                allMet = false;
                break;
            }
        }

        quest.isCompleted = allMet;
    }


	void OnDestroy()
    {
		foreach(var token in subscriptionTokens)
			EventBus.Unsubscribe(token);
    }

}
