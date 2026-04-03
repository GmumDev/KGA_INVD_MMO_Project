using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class QuestCompleteUISystem : MonoBehaviour
{
    [Header("Quest Completed Panel")]
	[SerializeField]
	GameObject questCompletePanel;

	[SerializeField]
	Transform questRewardIconsLayout;

	[SerializeField]
	TextMeshProUGUI questTitleText;

	[SerializeField]
	GameObject questRewardCellPrefab;   // object pool, need ImageComponent

	[Header("Player NPC Interaction Panel")]
	[SerializeField]
	GameObject interactHoverPanel;



	IObjectPool<Image> questRewardCellPool;

    List<SubscriptionToken> tokens = new List<SubscriptionToken>();


	void Start()
    {
		questRewardCellPool = new ObjectPool<Image>(
			createFunc: CreateItem,
			actionOnGet: OnGet,
			actionOnRelease: OnRelease,
			actionOnDestroy: OnDestroyItem,
			collectionCheck: true,   // helps catch double-release mistakes
			defaultCapacity: 5,
			maxSize: 20
		);
		questCompletePanel.SetActive(false);
		interactHoverPanel.SetActive(false);
	}
	private void OnEnable()
	{
		tokens.Add(EventBus.Subscribe<QuestCompletedEvent>(OnQuestCompleted));
		PlayerIdleState.OnInteractPanelRequested += interactHoverPanel.SetActive;	// Todo
	}
	void OnQuestCompleted(QuestCompletedEvent ev)
    {
        questTitleText.text = ev.title;

		int rewardLength = ev.rewardContexts.Length;

		for (int i = 0; i < rewardLength; i++)
		{
			var reward = ev.rewardContexts[i];
			IconIds iconId = IconIds.Default;
			switch (reward.type)
			{
				case QuestRewardType.Item:
					iconId = Enum.Parse<IconIds>(reward.itemId.ToString());
					break;
				case QuestRewardType.Exp:
					iconId = IconIds.Exp;
					break;
				case QuestRewardType.Gold:
					iconId = IconIds.Gold;
					break;
			}

			Sprite sprite = SOLoader<IconIds, IconSO>.Instance.GetSO(iconId).icon;
			var cell = questRewardCellPool.Get();


		}



		questCompletePanel.SetActive(true);
    }
	private void OnDisable()
	{
        foreach (var token in tokens)
            EventBus.Unsubscribe(token);
		PlayerIdleState.OnInteractPanelRequested -= interactHoverPanel.SetActive;
	}

	// Creates a new pooled GameObject the first time (and whenever the pool needs more).
	private Image CreateItem()
	{
		GameObject gameObject = Instantiate(questRewardCellPrefab);
		
		gameObject.SetActive(false);
		return gameObject.GetComponent<Image>();
	}

	// Called when an item is taken from the pool.
	private void OnGet(Image image)
	{
		image.gameObject.SetActive(true);
	}

	// Called when an item is returned to the pool.
	private void OnRelease(Image image)
	{
		image.gameObject.SetActive(false);
	}

	// Called when the pool decides to destroy an item (e.g., above max size).
	private void OnDestroyItem(Image image)
	{
		Destroy(image.gameObject);
	}

}
