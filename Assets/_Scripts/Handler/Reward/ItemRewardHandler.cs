using UnityEngine;

public class ItemRewardHandler : IRewardHandler
{
    void IRewardHandler.Handle(IQuestRewardEarner earner, QuestRewardContext context)
    {
		earner.EarnItemReward(context.itemId, context.itemCount);
    }
}
