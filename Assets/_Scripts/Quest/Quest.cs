using UnityEngine;

public class Quest
{
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
    public Quest(QuestSO _questSO)
    {
        this.Conditions = new QuestCondition[_questSO.conditions.Length];
        for(int i = 0; i < _questSO.conditions.Length; i++)
        {
			Conditions[i] = _questSO.conditions[i].GetCondition();
        }

        this.Rewards = new QuestReward[_questSO.rewards.Length];
		for (int i = 0; i < _questSO.rewards.Length; i++)
		{
			Rewards[i] = _questSO.rewards[i].GetRewardInstance();
		}
        this.title = _questSO.title;
        this.descript = _questSO.descript;
	}
    public QuestReward[] GetRewards()
    {
        return Rewards;
    }
}
