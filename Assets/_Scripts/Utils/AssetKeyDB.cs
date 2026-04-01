using System;
using UnityEngine;
public static class AssetKeyDB
{

	
    public static string GetAssetKey<T>(T id)
    {
		return Enum.GetName(typeof(T), id);
	}
}
