using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/QuestObtainConditionSO")]
public class QuestObtainConditionSO: QuestConditionSO
{
    public ItemIds itemID;
    public int itemCount;
    public bool removeItemsOnComplete;

    public override QuestCondition GetConditionInstance()
    {
		return new QuestObtainCondition(this);
	}
}
