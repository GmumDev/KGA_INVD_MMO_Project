
using System.Collections.Generic;
using System.Data;
using Unity.Android.Gradle.Manifest;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour, IQuestManager, IQuestRewardEarner
{
    public class QuestState
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
	private static QuestManager instance;
	public static IQuestManager Instance { get => instance; }

	public Dictionary<QuestIds, QuestState> QuestStates { get; private set; }
	IInventory inventory;

	List<SubscriptionToken> subscriptionTokens = new List<SubscriptionToken>();

	QuestRewardService rewardService = new QuestRewardService();

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	void Start()
	{
        inventory = GetComponent<InventorySystem>();
        QuestStates = new Dictionary<QuestIds, QuestState>();
    }
	private void OnEnable()
	{
		subscriptionTokens.Add(EventBus.Subscribe<InventoryChangedEvent>(OnInventoryChanged));
		subscriptionTokens.Add(EventBus.Subscribe<EnemyKilledEvent>(OnEnemyKilled));
		subscriptionTokens.Add(EventBus.Subscribe<ScenarioNodeFinishedEvent>(OnScenarioNodeFinished));
	}
	void IQuestManager.CompleteQuest(QuestIds questID)
	{
		if (QuestStates.ContainsKey(questID) == false) return;
		if (QuestStates[questID].isCompleted == false) return;

        foreach (var ctx in QuestStates[questID].data.rewardContexts)
		{
            rewardService.Give(this, ctx);
		}

        QuestStates.Remove(questID);

		EventBus.Publish(new QuestCompletedEvent(
			QuestStates[questID].data.title, 
			QuestStates[questID].data.rewardContexts));
	}
	void IQuestManager.AbandonQuest(QuestIds questID)
	{
		if (QuestStates.ContainsKey(questID) == false) return;

        QuestStates.Remove(questID);
	}
	void IQuestManager.AcceptQuest(QuestIds questID)
	{
		if (QuestStates.ContainsKey(questID)) return;

		var context = SOLoader<QuestIds, QuestSO>.Instance.GetSO(questID).ToContext();
		List<QuestConditionProgress> _progresses = new List<QuestConditionProgress>();
		HashSet<QuestConditionType> conditionTypes = new HashSet<QuestConditionType>();
		foreach(var ctx in context.conditionContexts)
        {
            conditionTypes.Add(ctx.type);
            switch (ctx.type)
			{
				case QuestConditionType.Obtain:
                    _progresses.Add(QuestConditionProgress.GetObtainConditionProgress(ctx.itemID, ctx.itemCntToObtain));
                    break;
				case QuestConditionType.Kill:
                    _progresses.Add(QuestConditionProgress.GetKillConditionProgress(ctx.enemyId, ctx.enemyCntToKill));
                    break;
			}
		}
		QuestState _questState = new QuestState(
			data: context,
			progress: _progresses,
			isCompleted: false,
			conditionTypes: conditionTypes
		);
        QuestStates.Add(questID, _questState);

        EventBus.Publish(new QuestAcceptedEvent(questID));
	}

	//
	void OnInventoryChanged(InventoryChangedEvent ev)
	{
		foreach (var _questState in QuestStates.Values)
		{
			if (_questState.conditionTypes.Contains(QuestConditionType.Obtain) == false) continue;

            bool allMet = true;
            for (int i = 0; i < _questState.progress.Count; i++)
			{
				var condition = _questState.data.conditionContexts[i];
				if (condition.type != QuestConditionType.Obtain) continue;
                if (condition.itemID != ev.itemId) continue;

                var progress = _questState.progress[i];

				progress.curAmount += ev.deltaCnt;

				allMet = allMet && QuestConditionService.IsMet(this, condition, progress);
            }
            _questState.isCompleted = allMet;
        }
	}
	void OnEnemyKilled(EnemyKilledEvent ev)
	{
		foreach(var _questState in QuestStates.Values)
		{
			if (_questState.conditionTypes.Contains(QuestConditionType.Kill) == false) continue;

            bool allMet = true;

            for (int i = 0; i < _questState.progress.Count; i++)
			{
				var condition = _questState.data.conditionContexts[i];
				if (condition.type != QuestConditionType.Kill) continue;
				if (condition.enemyId != ev.enemyId) continue;

				var progress = _questState.progress[i];

				progress.curAmount += ev.enemyKilledCnt;

                allMet = allMet && QuestConditionService.IsMet(this, condition, progress);
            }
            _questState.isCompleted = allMet;
        }
	}
	void OnScenarioNodeFinished(ScenarioNodeFinishedEvent ev)
	{
		if(ev.eventType == ScenarioNodeFinishedEventType.FollowUpQuest)
        {
            (this as IQuestManager).AcceptQuest(ev.followUpQuestId);
        }
	}

	void IQuestRewardEarner.EarnItemReward(ItemIds id, int cnt) => inventory.ObtainItem(id, cnt);
	void IQuestRewardEarner.EarnGoldReward(int cnt) => throw new System.NotImplementedException();
	void IQuestRewardEarner.EarnExpReward(int cnt) => throw new System.NotImplementedException();
	

	void OnDisable()
    {
		foreach(var token in subscriptionTokens)
			EventBus.Unsubscribe(token);
    }

}
