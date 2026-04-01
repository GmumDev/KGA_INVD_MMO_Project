using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScenarioPlayer : MonoBehaviour, IScenarioPlayer
{


	Scenario currentScenario;

	[SerializeField]
	GameObject ScenarioPanel;

	[SerializeField]
	TextMeshProUGUI dialogueUGUI;

	Scenario scenario;

	private async void Start()
	{

		SOLoader<ScenarioIds, ScenarioSO> ScenarioLoader = SOLoader<ScenarioIds, ScenarioSO>.Instance;

		await ScenarioLoader.LoadData(ScenarioIds.FirstTutorial);

		scenario = new Scenario(ScenarioLoader.GetSO(ScenarioIds.FirstTutorial));

	}

	public void Play()
	{
		(this as IScenarioPlayer).PlayScenario(scenario);
	}

	public void NextScenarioNode()
	{
		(this as IScenarioPlayer).NextNode();
	}


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
	void IScenarioPlayer.DoDialogue(ScenarioDialogueNodeSO so)
	{
		dialogueUGUI.text = so.dialogueStr;
		ScenarioPanel.SetActive(true);
	}
	void IScenarioPlayer.ClearDialogue()
	{
		dialogueUGUI.text = "";
		ScenarioPanel.SetActive(false);
	}
	void OnScenarioFinished()
	{

	}
}
