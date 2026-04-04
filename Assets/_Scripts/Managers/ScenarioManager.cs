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
	private void OnEnable()
	{
        PlayerWatchState.OnNextNodeRequested += NextNode;
	}
	public bool PlayScenario(ScenarioIds scenarioId)
    {
        if (isPlaying) return false;

        ScenarioSO scenario = SOLoader<ScenarioIds, ScenarioSO>.Instance.GetSO(scenarioId);

        isPlaying = true;
        curNodeSO = scenario.startNode;
        curScenario = scenario.ToContext();
        curNodeContext = curNodeSO.ToContext();

        ScenarioService.PlayAsFirstNode(this, curNodeContext);

        return true;
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
            if(curScenario.unloadDataOnScenarioFinished)
                SOLoader<ScenarioIds, ScenarioSO>.Instance.UnloadData(curScenario.id);
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
