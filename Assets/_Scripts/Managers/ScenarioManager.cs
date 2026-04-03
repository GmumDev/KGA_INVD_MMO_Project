using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScenarioManager : MonoBehaviour, IScenarioManager, IScenarioContextRunner
{
    private static ScenarioManager instance;
    public static ScenarioManager Instance { get => instance; }
    public event Action OnScenarioEnded;

	[SerializeField]
	GameObject ScenarioPanel;

	[SerializeField]
	TextMeshProUGUI dialogueSpeakerUGUI;
	[SerializeField]
	TextMeshProUGUI dialogueTextUGUI;

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

	private void Awake()
	{
		if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
	}

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
            OnScenarioEnded?.Invoke();

			return;
        }

        curNodeSO = curNodeSO.nextNode;

        curNodeContext = curNodeSO.ToContext();
        ScenarioService.PlayAsNextNode(this, curNodeContext);
    }


    void IScenarioContextRunner.DoDialogue(ScenarioNodeContext ctx)
	{
		dialogueSpeakerUGUI.text = ctx.speakerStr;
		dialogueTextUGUI.text = ctx.dialogueStr;
		ScenarioPanel.SetActive(true);
    }

    void IScenarioContextRunner.ClearDialogue()
	{
		dialogueSpeakerUGUI.text = "";
		dialogueTextUGUI.text = "";
        ScenarioPanel.SetActive(false);
    }
}
