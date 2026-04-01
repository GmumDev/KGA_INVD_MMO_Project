using System.Collections.Generic;
using UnityEngine;

public static class QuestRewardService
{
    private static readonly Dictionary<QuestRewardType, IRewardHandler> handlers
        = new Dictionary<QuestRewardType, IRewardHandler>()
        {
            { QuestRewardType.Item, new ItemRewardHandler() }
        };
    public static void Give(IQuestRunner runner, QuestRewardContext context)
    {
        handlers[context.type].Handle(runner, context);
    }

}
