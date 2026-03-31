using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class QuestDB
{
	private static QuestDB instance;
	public static QuestDB Instance
	{
		get
		{
			if(instance == null)
			{
				instance = new QuestDB();
			}
			return instance;
		}
	}
	private Dictionary<string, QuestSO> questSOs;

	private QuestDB()
	{
		questSOs = new Dictionary<string, QuestSO>();
	}
	public async Awaitable LoadData(string assetKey)
	{
		AsyncOperationHandle handle = Addressables.LoadAssetAsync<QuestSO>(assetKey);
		handle.Completed += Handle_Completed;

		await handle.Task;

		Addressables.Release(handle);
	}
	private void Handle_Completed(AsyncOperationHandle obj)
	{
		if (obj.Status == AsyncOperationStatus.Succeeded)
		{
			// same key exception can occur
			QuestSO so = (QuestSO)obj.Result;
			questSOs.Add(so.id, so);
		}
	}

	public QuestSO GetSO(string id)
	{
		if (questSOs.ContainsKey(id)) return questSOs[id];
		return null;
	}
}
