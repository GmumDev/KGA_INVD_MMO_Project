using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class NPC_Developer : NPC
{
    bool isTalking;

    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        ScenarioManager.Instance.OnScenarioEnded -= scenarioEndHandler;
    }
    void scenarioEndHandler()
    {
        ScenarioManager.Instance.OnScenarioEnded -= scenarioEndHandler;

        CameraManager.Instance.ResetCameraTarget();
    }

    public override void OnInteract()
    {
        isTalking = ScenarioManager.Instance.PlayScenario(npcSo.scenarioId);

        if (isTalking == false) return;


        ScenarioManager.Instance.OnScenarioEnded += scenarioEndHandler;

        
        CameraManager.Instance.FocusTalker(transform);

        // camManager.SoftMoveToTarget(this.transform)


    }
}
