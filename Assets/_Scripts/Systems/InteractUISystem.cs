using System.Collections.Generic;
using UnityEngine;

public class InteractUISystem : MonoBehaviour
{

    [Header("Player NPC Interaction Panel")]
    [SerializeField]
    GameObject interactHoverPanel;


    void Start()
    {
        interactHoverPanel.SetActive(false);
    }
    void HandleInteractPanelRequest(bool v)
    {
        interactHoverPanel.SetActive(v);
    }
    private void OnEnable()
    {
        PlayerIdleState.OnInteractPanelRequested += HandleInteractPanelRequest;
    }
    private void OnDisable()
    {
        PlayerIdleState.OnInteractPanelRequested -= HandleInteractPanelRequest;
    }

}
