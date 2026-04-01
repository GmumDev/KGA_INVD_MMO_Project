using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/QuestItemRewardSO")]

public class QuestItemRewardSO: QuestRewardSO
{
	public ItemIds itemId;
	public int itemCount;

    public override QuestRewardContext ToContext()
    {
        var context = new QuestRewardContext();
        context.id = id;
        context.type = type;
        context.itemId = itemId;
        context.itemCount = itemCount;

        return context;
    }
}