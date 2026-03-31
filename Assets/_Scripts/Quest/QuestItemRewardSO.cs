using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/QuestItemRewardSO")]

public class QuestItemRewardSO: QuestRewardSO
{
	public string id;
	public ItemData itemData;
	public int itemCount;

	public override QuestReward GetRewardInstance()
	{
		return new QuestItemReward(this);
	}
}