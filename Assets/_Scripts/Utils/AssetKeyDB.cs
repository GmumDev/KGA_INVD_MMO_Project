using UnityEngine;

public enum KeyType
{
    Quest,
	QuestCondition,
	QuestReward,
}
public static class AssetKeyDB
{
    public static string GetAssetKey(KeyType keytype)
    {
        switch(keytype)
        {
            case KeyType.Quest:
                return "4210001";
            case KeyType.QuestCondition:
                return "4310001";
			case KeyType.QuestReward:
				return "4410001";
            default:
                return "";
        }
    }
}
