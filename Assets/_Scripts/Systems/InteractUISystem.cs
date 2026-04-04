using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractUISystem : MonoBehaviour
{

    [Header("Player NPC Interaction Panel")]
    [SerializeField]
    GameObject interactHoverPanel;
    [SerializeField]
    TextMeshProUGUI interactMassageTextField;


    SubscriptionToken token1;
    SubscriptionToken token2;

    private Dictionary<InteractableIds, string> interactionMassage;
    void Start()
    {
        interactHoverPanel.SetActive(false);
        interactionMassage = new Dictionary<InteractableIds, string>()
        {
            {InteractableIds.NPC, "대화하기"}
        };
    }
    void HandleInteractTargetedOn(PlayerInteractTargetedEvent ev)
    {
        InteractableIds targetId = ev.target.GetInteractableID();

        if(interactionMassage.ContainsKey(targetId))
        {
            interactMassageTextField.text = interactionMassage[targetId];
        }
        interactHoverPanel.SetActive(true);
    }
    void HandleInteractTargetedOff(PlayerInteractUnTargetedEvent ev)
    {
        interactHoverPanel.SetActive(false);
    }
    private void OnEnable()
    {
        token1 = EventBus.Subscribe<PlayerInteractTargetedEvent>(HandleInteractTargetedOn);
        token2 = EventBus.Subscribe<PlayerInteractUnTargetedEvent>(HandleInteractTargetedOff);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(token1);
        EventBus.Unsubscribe(token2);
    }

}
