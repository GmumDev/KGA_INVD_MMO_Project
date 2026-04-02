using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/QuestObtainConditionSO")]
public class QuestObtainConditionSO: QuestConditionSO
{
    public ItemIds itemID;
    public int itemCount;
    public bool removeItemsOnComplete;

    public override QuestConditionContext ToContext()
    {
        var context = new QuestConditionContext();
        context.type = type;

        context.itemID = itemID;
        context.itemCount = itemCount;
        context.removeItemsOnComplete = removeItemsOnComplete;

        return context;
    }
}
