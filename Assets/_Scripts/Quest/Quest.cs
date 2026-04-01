using UnityEngine;

public class Quest
{
    QuestIds id;
    string title;
    string descript;
	QuestCondition[] Conditions { get; }
    QuestReward[] Rewards { get; }

    public bool IsComplete(IQuestRunner runner)
	{
		for (int i = 0; i < Conditions.Length; i++)
		{
            if (Conditions[i].IsMet(runner) == false) return false;
        }
        return true;
    }
    public Quest(QuestSO questSO)
    {
        this.Conditions = new QuestCondition[questSO.conditions.Length];
        for(int i = 0; i < questSO.conditions.Length; i++)
        {
			Conditions[i] = questSO.conditions[i].GetConditionInstance();
        }

        this.Rewards = new QuestReward[questSO.rewards.Length];
		for (int i = 0; i < questSO.rewards.Length; i++)
		{
			Rewards[i] = questSO.rewards[i].GetRewardInstance();
		}
        this.title = questSO.title;
        this.descript = questSO.descript;
        this.id = questSO.id;
	}
    public void GetRewards(IQuestRunner runner)
    {
        foreach(var reward in Rewards)
        {
            reward.GiveRewardToRunner(runner);
        }
    }
}
