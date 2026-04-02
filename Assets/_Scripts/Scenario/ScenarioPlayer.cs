using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScenarioPlayer : MonoBehaviour, IScenarioPlayer, IScenarioContextRunner
{
	[SerializeField]
	GameObject ScenarioPanel;

	[SerializeField]
	TextMeshProUGUI dialogueUGUI;

    ScenarioContext curScenario;
    ScenarioNodeSO curNodeSO;
    ScenarioNodeContext curNodeContext;


    private async void Start()
	{

		SOLoader<ScenarioIds, ScenarioSO> ScenarioLoader = SOLoader<ScenarioIds, ScenarioSO>.Instance;

		await ScenarioLoader.LoadData(ScenarioIds.FirstTutorial);


	}

    bool isPlaying;
    bool IScenarioPlayer.IsPlaying => isPlaying;

	public void Play()
	{
		(this as IScenarioPlayer).PlayScenario(ScenarioIds.FirstTutorial);
	}
	public void NextScenarioNode()
	{
		if (isPlaying == false) return;

		(this as IScenarioPlayer).NextNode();
	}

	void IScenarioPlayer.PlayScenario(ScenarioIds scenarioId)
    {
        if (isPlaying) return;

        ScenarioSO scenario = SOLoader<ScenarioIds, ScenarioSO>.Instance.GetSO(scenarioId);

        isPlaying = true;
        curNodeSO = scenario.startNode;
        curScenario = scenario.ToContext();
        curNodeContext = curNodeSO.ToContext();

        ScenarioService.PlayAsFirstNode(this, curNodeContext);
    }
    void IScenarioPlayer.NextNode()
    {
        if (isPlaying == false)     // fool proof
        {
            return;
        }

        ScenarioService.FinishNode(this, curNodeContext);

        if (curNodeSO.nextNode == null)  // is end, but trying to next
        {
            isPlaying = false;
            return;
        }

        curNodeSO = curNodeSO.nextNode;

        curNodeContext = curNodeSO.ToContext();
        ScenarioService.PlayAsNextNode(this, curNodeContext);

    }


    void IScenarioContextRunner.DoDialogue(ScenarioNodeContext ctx)
    {
        dialogueUGUI.text = ctx.dialogueStr;
        ScenarioPanel.SetActive(true);
    }

    void IScenarioContextRunner.ClearDialogue() 
    {
        dialogueUGUI.text = "";
        ScenarioPanel.SetActive(false);
    }
}
