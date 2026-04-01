using Unity.VisualScripting;
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

	Scenario currentScenario;

	bool isPlaying;
	bool IScenarioPlayer.IsPlaying => isPlaying;

	void IScenarioPlayer.PlayScenario(Scenario scenario)
	{
		if (isPlaying) return;

		isPlaying = true;
		this.currentScenario = scenario;
		currentScenario.PlayCurrentNode(this as IScenarioPlayer);
	}
	void IScenarioPlayer.NextNode()
	{
		if (currentScenario == null) return;

		isPlaying = currentScenario.PlayNextNode(this);
		if(isPlaying == false)
		{
			OnScenarioFinished();
		}
	}
	void IScenarioPlayer.DoDialogue(ScenarioDialogueNode so)
	{
		throw new System.NotImplementedException();
	}

	void IScenarioPlayer.ClearDialogue()
	{
		throw new System.NotImplementedException();
	}
	void OnScenarioFinished()
	{

	}
}
