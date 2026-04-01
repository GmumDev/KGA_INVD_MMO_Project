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
	}
	private async void Start()
	{
		SOLoader<QuestIds, QuestSO> QuestLoader = SOLoader<QuestIds, QuestSO>.Instance;

		await QuestLoader.LoadData(QuestIds.FirstQuest);
		
		Quest q = new Quest(QuestLoader.GetSO(QuestIds.FirstQuest));

		Debug.Log(q);
	}
}
