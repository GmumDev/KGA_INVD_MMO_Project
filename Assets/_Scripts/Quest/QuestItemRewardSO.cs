using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/QuestItemRewardSO")]

public class QuestItemRewardSO: QuestRewardSO
{
	public ItemIds itemId;
	public ItemData itemData;
	public int itemCount;

	public override QuestReward GetRewardInstance()
	{
		return new QuestItemReward(this);
	}
}