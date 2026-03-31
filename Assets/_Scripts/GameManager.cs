using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager: MonoBehaviour
{
	
	static GameManager instance;
	public static GameManager Instance
	{
		get
		{
			if(instance == null)
			{
				return null;
			}
			return instance;
		}
	}
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}

		_ = QuestDB.Instance;
		_ = QuestInstantiater.Instance;

	}
	private async void Start()
	{

		string assetKey = AssetKeyDB.GetAssetKey(KeyType.Quest);
		await QuestDB.Instance.LoadData(assetKey);
		Quest q = QuestInstantiater.Instance.InstantiateQuestFromID(assetKey);

		Debug.Log(q);
	}
}
