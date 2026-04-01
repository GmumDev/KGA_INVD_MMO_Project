using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(QuestRunner))]
public class Player : MonoBehaviour
{
    IInventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
    }
}
