using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Quest/QuestSO")]

public class QuestSO: SORuntimeLoadable<QuestIds>
{
	public QuestConditionSO[] conditions;
	public QuestRewardSO[] rewards;

	public string title;
	public string descript;

	public QuestContext ToContext()
	{
		var context = new QuestContext();
		context.id = id;
		context.title = title;
		context.descript = descript;
		context.conditionContexts = new QuestConditionContext[conditions.Length];
		for(int i = 0; i < conditions.Length; i++)
		{
			context.conditionContexts[i] = conditions[i].ToContext();
		}


        context.rewardContexts = new QuestRewardContext[rewards.Length];
        for (int i = 0; i < conditions.Length; i++)
        {
            context.rewardContexts[i] = rewards[i].ToContext();
        }

		return context;
    }
}
