using UnityEngine;

public interface IRewardHandler
{
    void Handle(IQuestRewardEarner earner, QuestRewardContext context);

}