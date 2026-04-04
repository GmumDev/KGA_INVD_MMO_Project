using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractUISystem : MonoBehaviour
{
    static InteractUISystem instance;
    public static InteractUISystem Instance { get => instance; }

    
    [Header("Player NPC Interaction Panel")]
    [SerializeField]
    GameObject interactHoverPanel;
    [SerializeField]
    TextMeshProUGUI interactMassageTextField;

    private Dictionary<InteractableIds, string> interactionMassage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        interactHoverPanel.SetActive(false);
        interactionMassage = new Dictionary<InteractableIds, string>()
        {
            {InteractableIds.NPC, "대화하기"}
        };
    }


    public void InteractTargetedOn(InteractableIds targetId)
    {
        if(interactionMassage.ContainsKey(targetId))
        {
            interactMassageTextField.text = interactionMassage[targetId];
        }
        interactHoverPanel.SetActive(true);
    }
    public void InteractTargetedOff()
    {
        if(interactHoverPanel != null)
            interactHoverPanel.SetActive(false);
    }


}
