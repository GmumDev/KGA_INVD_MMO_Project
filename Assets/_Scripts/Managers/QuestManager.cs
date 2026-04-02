
using System.Collections.Generic;
using System.Data;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class QuestManager : MonoBehaviour, IQuestManager, IQuestRewardEarner
{
    class QuestState
    {
        public QuestContext data;
        public List<QuestConditionProgress> progress;
		public HashSet<QuestConditionType> conditionTypes;
        public bool isCompleted;

        public QuestState(QuestContext data, List<QuestConditionProgress> progress, HashSet<QuestConditionType> conditionTypes, bool isCompleted)
        {
            this.data = data;
            this.progress = progress;
            this.conditionTypes = conditionTypes;
            this.isCompleted = isCompleted;
        }
    }
    Dictionary<QuestIds, QuestState> questStates = new Dictionary<QuestIds, QuestState>();

	IInventory inventory;

	List<SubscriptionToken> subscriptionTokens = new List<SubscriptionToken>();

	QuestRewardService rewardService = new QuestRewardService();

    void Start()
	{
        inventory = GetComponent<InventorySystem>();

		subscriptionTokens.Add(EventBus.Subscribe<InventoryChangedEvent>(OnInventoryChanged));
		subscriptionTokens.Add(EventBus.Subscribe<EnemyKilledEvent>(OnEnemyKilled));
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
		HashSet<QuestConditionType> conditionTypes = new HashSet<QuestConditionType>();
		foreach(var ctx in context.conditionContexts)
        {
            conditionTypes.Add(ctx.type);
            switch (ctx.type)
			{
				case QuestConditionType.Obtain:
                    _progresses.Add(QuestConditionProgress.GetObtainConditionProgress(ctx.itemID));
                    break;
				case QuestConditionType.Kill:
                    _progresses.Add(QuestConditionProgress.GetKillConditionProgress(ctx.enemyId));
                    break;
			}
		}
		QuestState _questState = new QuestState(
			data: context,
			progress: _progresses,
			isCompleted: false,
			conditionTypes: conditionTypes
		);

        questStates.Add(questID, _questState);
	}

	//
	void OnInventoryChanged(InventoryChangedEvent ev)
	{
		foreach (var _questState in questStates.Values)
		{
			if (_questState.conditionTypes.Contains(QuestConditionType.Obtain) == false) continue;

            bool allMet = true;
            for (int i = 0; i < _questState.progress.Count; i++)
			{
				var condition = _questState.data.conditionContexts[i];
				if (condition.type != QuestConditionType.Obtain) continue;
                if (condition.itemID != ev.itemId) continue;

                var progress = _questState.progress[i];

				progress.itemCount += ev.deltaCnt;

				allMet = allMet && QuestConditionService.IsMet(this, condition, progress);
            }
            _questState.isCompleted = allMet;
        }
	}
	void OnEnemyKilled(EnemyKilledEvent ev)
	{
		foreach(var _questState in questStates.Values)
		{
			if (_questState.conditionTypes.Contains(QuestConditionType.Kill) == false) continue;

            bool allMet = true;

            for (int i = 0; i < _questState.progress.Count; i++)
			{
				var condition = _questState.data.conditionContexts[i];
				if (condition.type != QuestConditionType.Kill) continue;
				if (condition.enemyId != ev.enemyId) continue;

				var progress = _questState.progress[i];

				progress.enemyCnt += ev.enemyKilledCnt;

                allMet = allMet && QuestConditionService.IsMet(this, condition, progress);
            }
            _questState.isCompleted = allMet;
        }
	}

	void IQuestRewardEarner.EarnItemReward(ItemIds id, int cnt) => inventory.ObtainItem(id, cnt);
	void IQuestRewardEarner.EarnGoldReward(int cnt) => throw new System.NotImplementedException();
	void IQuestRewardEarner.EarnExpReward(int cnt) => throw new System.NotImplementedException();
	

	void OnDestroy()
    {
		foreach(var token in subscriptionTokens)
			EventBus.Unsubscribe(token);
    }

}
