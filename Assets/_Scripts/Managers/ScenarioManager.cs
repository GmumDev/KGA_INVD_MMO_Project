using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScenarioManager : MonoBehaviour, IScenarioManager, IScenarioContextRunner
{
	[SerializeField]
	GameObject ScenarioPanel;

	[SerializeField]
	TextMeshProUGUI dialogueUGUI;

    ScenarioContext curScenario;
    ScenarioNodeSO curNodeSO;
    ScenarioNodeContext curNodeContext;



    bool isPlaying;
    bool IScenarioManager.IsPlaying => isPlaying;

    #region Scenario Test On UI Button
    public void Test_Play()
	{
		PlayScenario(ScenarioIds.FirstTutorial);
    }
    public void Test_NextNode()
    {
        NextNode();
    }
    #endregion 

    public void PlayScenario(ScenarioIds scenarioId)
    {
        if (isPlaying) return;

        ScenarioSO scenario = SOLoader<ScenarioIds, ScenarioSO>.Instance.GetSO(scenarioId);

        isPlaying = true;
        curNodeSO = scenario.startNode;
        curScenario = scenario.ToContext();
        curNodeContext = curNodeSO.ToContext();

        ScenarioService.PlayAsFirstNode(this, curNodeContext);
    }
    public void NextNode()
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
