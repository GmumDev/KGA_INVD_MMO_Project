using UnityEngine;

public interface IInteractable
{
    void OnInteract();
    InteractableIds GetInteractableID();
}
