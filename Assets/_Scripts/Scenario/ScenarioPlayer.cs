using UnityEngine;

public class ScenarioPlayer : IScenarioPlayer
{
	static ScenarioPlayer instance;
	public static ScenarioPlayer Instance
	{
		get
		{
			if (instance == null)
				instance = new ScenarioPlayer();
			return instance;
		}
	}
	private ScenarioPlayer() { }



	void IScenarioPlayer.Next()
	{
		throw new System.NotImplementedException();
	}

	void IScenarioPlayer.Play(ScenarioNodeIds id)
	{
		throw new System.NotImplementedException();
	}
}
