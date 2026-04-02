using System.Collections.Generic;
using UnityEngine;

public static class QuestConditionService
{
    private static readonly Dictionary<QuestConditionType, IConditionChecker> handlers
        = new Dictionary<QuestConditionType, IConditionChecker>()
        {
            { QuestConditionType.Obtain, new ObtainConditionChecker() }
        };
    public static bool IsMet(IQuestRunner runner, QuestConditionContext context)
    {
        return handlers[context.type].Check(runner, context);

    }
}
