using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SOLoader<TID, TSO> where TSO : SORuntimeLoadable<TID>
{
	private static SOLoader<TID, TSO> instance;
	public static SOLoader<TID, TSO> Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new SOLoader<TID, TSO>();
			}
			return instance;
		}
	}
	private Dictionary<TID, TSO> datas = new Dictionary<TID, TSO>();
	
	public async Awaitable LoadData(TID id)
	{
		string assetKey = Enum.GetName(typeof(TID), id);
		AsyncOperationHandle handle = Addressables.LoadAssetAsync<TSO>(assetKey);
		handle.Completed += Handle_Completed;

		await handle.Task;

		Addressables.Release(handle);
	}
	private void Handle_Completed(AsyncOperationHandle obj)
	{
		if (obj.Status == AsyncOperationStatus.Succeeded)
		{
			// same key exception can occur
			TSO so = (TSO)obj.Result;
			datas.Add(so.id, so);
		}
	}

	public TSO GetSO(TID id)
	{
		if (datas.ContainsKey(id)) return datas[id];
		return default;
	}
}
