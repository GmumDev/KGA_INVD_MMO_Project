using UnityEngine;

public interface IConditionChecker
{
    bool Check(IQuestRunner runner, QuestConditionContext context);
}
