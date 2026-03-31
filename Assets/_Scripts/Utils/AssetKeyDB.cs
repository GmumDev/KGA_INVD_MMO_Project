using System;
using UnityEngine;
public static class AssetKeyDB
{

	// QuestID 뿐만아니라 여러가지 오버로딩해서 가져올 수 있도록 구현

    public static string GetAssetKey(QuestIds id)
    {
		return Enum.GetName(typeof(QuestIds), id);
	}
}
