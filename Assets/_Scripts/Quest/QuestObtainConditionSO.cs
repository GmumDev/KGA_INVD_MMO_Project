using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/QuestObtainConditionSO")]
public class QuestObtainConditionSO: QuestConditionSO
{
    public string itemID;
    public int itemCount;
    public bool removeItemsOnComplete;

    public override QuestCondition GetCondition()
    {
		return new QuestObtainCondition(this);
	}
}
