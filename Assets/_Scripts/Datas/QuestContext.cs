using UnityEngine;

public struct QuestContext
{
    public QuestIds id;
    public string title;
    public string descript;
    public QuestConditionContext[] conditionContexts;
    public QuestRewardContext[] rewardContexts;
}
