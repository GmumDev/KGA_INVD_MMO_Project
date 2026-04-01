using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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

	}
	private async void Start()
	{
		string assetKey = AssetKeyDB.GetAssetKey(QuestIds.FirstQuest);
		await QuestDB.Instance.LoadData(assetKey); // Load하면 QuestDB.Instance 내에 SO 남아있음. 

		Quest q = new Quest(QuestDB.Instance.GetSO(QuestIds.FirstQuest));

		Debug.Log(q);
	}
}
