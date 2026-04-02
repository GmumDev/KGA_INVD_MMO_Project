using UnityEngine;

public interface IRewardHandler
{
    void Handle(IQuestRunner runner, QuestRewardContext context);

}