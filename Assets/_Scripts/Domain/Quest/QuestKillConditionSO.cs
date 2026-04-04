using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Quest/KillConditionSO")]

public class QuestKillConditionSO: QuestConditionSO
{
    public EnemyIds enemyId;
    public int enemyCntToKill;

	public override QuestConditionContext ToContext()
	{
		var context = new QuestConditionContext();
		context.type = type;

		context.enemyId = enemyId;
		context.enemyCntToKill = enemyCntToKill;

		return context;
	}
}
