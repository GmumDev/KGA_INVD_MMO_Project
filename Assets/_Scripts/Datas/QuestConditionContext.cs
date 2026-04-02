using UnityEngine;

public struct QuestConditionContext
{
    public QuestConditionIds id;
    public QuestConditionType type;
    
    // obtain
    public ItemIds itemID;
    public int itemCount;
    public bool removeItemsOnComplete;

    //
}
