using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class NPC_Developer : NPC
{
    bool isTalking;
    SubscriptionToken token;

    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(token);
    }
    void scenarioEndHandler(ScenarioFinishedEvent ev)
    {
        EventBus.Unsubscribe(token);

        CameraManager.Instance.ResetCameraTarget();
    }

    public override void OnInteract()
    {
        isTalking = ScenarioManager.Instance.PlayScenario(npcSo.scenarioId);

        if (isTalking == false) return;

        token = EventBus.Subscribe<ScenarioFinishedEvent>(scenarioEndHandler);

        CameraManager.Instance.FocusTalker(transform);

        // camManager.SoftMoveToTarget(this.transform)


    }
}
