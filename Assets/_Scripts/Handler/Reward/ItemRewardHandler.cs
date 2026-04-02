using UnityEngine;

public class ItemRewardHandler : IRewardHandler
{
    void IRewardHandler.Handle(IQuestRunner runner, QuestRewardContext context)
    {
        runner.AddItem(context.itemId, context.itemCount);
    }
}
