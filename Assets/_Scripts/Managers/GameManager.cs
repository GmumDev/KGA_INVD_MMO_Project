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


        SOLoader<ScenarioIds, ScenarioSO> ScenarioLoader = SOLoader<ScenarioIds, ScenarioSO>.Instance;

        await ScenarioLoader.LoadData(ScenarioIds.FirstTutorial);

        SOLoader<QuestIds, QuestSO> QuestLoader = SOLoader<QuestIds, QuestSO>.Instance;

		await QuestLoader.LoadData(QuestIds.FirstQuest);

		//IQuestManager q;
		//q.AcceptQuest(QuestIds.FirstQuest);

	}
}
