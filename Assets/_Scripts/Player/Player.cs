using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    IObtainer obtainer;
    IQuestRunner questRunner;

    void Start()
    {
        var inventory = new Inventory();
		obtainer = new Obtainer(inventory as IInventory);
        questRunner = new QuestRunner(obtainer as IObtainer, inventory as IInventory);

    }
}
