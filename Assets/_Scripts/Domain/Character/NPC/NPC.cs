using UnityEngine;

public abstract class NPC : MonoBehaviour, IInteractable
{
    [SerializeField]
    protected NPCSO npcSo;

    public abstract void OnInteract();

    public InteractableIds GetInteractableID() => InteractableIds.NPC;

}
