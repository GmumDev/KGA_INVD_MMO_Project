using UnityEngine;

public interface IQuestRewardEarner
{
	public void EarnItemReward(ItemIds id, int cnt);
	public void EarnGoldReward(int cnt);
	public void EarnExpReward(int cnt);
}
