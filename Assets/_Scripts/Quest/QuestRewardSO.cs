using UnityEngine;

public abstract class QuestRewardSO : ScriptableObject
{
	public QuestRewardType type;
	public abstract QuestReward GetRewardInstance();
}