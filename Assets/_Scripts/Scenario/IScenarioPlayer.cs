using UnityEngine;

public interface IScenarioPlayer
{
    bool IsPlaying { get; }
    void PlayScenario(Scenario scenario);
    void NextNode();

    //

	void DoDialogue(ScenarioDialogueNodeSO so);
    void ClearDialogue();
}
