using System.Collections.Generic;
using UnityEngine;

public static class QuestConditionService
{
    private static readonly Dictionary<QuestConditionType, IConditionChecker> handlers
        = new Dictionary<QuestConditionType, IConditionChecker>()
        {
            { QuestConditionType.Obtain, new ObtainConditionChecker() },
			{ QuestConditionType.Kill, new KillConditionChecker() }
		};
    public static bool IsMet(IQuestRunner runner, QuestConditionContext conditionCtx, QuestConditionProgress progressCtx)
    {
        return handlers[conditionCtx.type].Check(runner, conditionCtx, progressCtx);

    }
}
