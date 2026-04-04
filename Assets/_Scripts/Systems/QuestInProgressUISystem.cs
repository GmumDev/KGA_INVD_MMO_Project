using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class QuestInProgressUISystem: MonoBehaviour
{
    [Header("Quest Progress Panel")]
    [SerializeField]
    GameObject[] questInProgressPanel;

    TextMeshProUGUI[] questTitles;
    TextMeshProUGUI[][] questProgressText;



    List<SubscriptionToken> subscriptionTokens;

    Dictionary<QuestIds, (TextMeshProUGUI, TextMeshProUGUI[])> progressUIContainer;

    int curQuestShownCnt;

    private void OnEnable()
    {

    }
    private void Start()
    {
        // QuestManager.OnEnable 이후 실행되어야 하므로 Start에 넣음. 
        // QuestManager의 이벤트가 발동해서 progress를 업데이트 한 뒤 그걸 가져와야
        // 되기 때문. 
        subscriptionTokens = new List<SubscriptionToken>();
        subscriptionTokens.Add(EventBus.Subscribe<InventoryChangedEvent>(OnInventoryChanged));
        subscriptionTokens.Add(EventBus.Subscribe<EnemyKilledEvent>(OnEnemyKilled));
        subscriptionTokens.Add(EventBus.Subscribe<QuestAcceptedEvent>(OnQuestAccepted));

        progressUIContainer = new Dictionary<QuestIds, (TextMeshProUGUI, TextMeshProUGUI[])>();
        curQuestShownCnt = 0;
        questTitles = new TextMeshProUGUI[5];
        questProgressText = new TextMeshProUGUI[5][];
        for (int i = 0; i < questInProgressPanel.Length; i++)
        {
            questProgressText[i] = new TextMeshProUGUI[3];
            var textGUIs = questInProgressPanel[i].GetComponentsInChildren<TextMeshProUGUI>();
            Debug.Log(textGUIs);
            questTitles[i] = textGUIs[0];
            for (int j = 0; j < 3; j++)
                questProgressText[i][j] = textGUIs[j + 1];
        }
    }
    private void OnDisable()
    {
        foreach(var token in subscriptionTokens)
        {
            EventBus.Unsubscribe(token);
        }
    }
    void OnQuestAccepted(QuestAcceptedEvent ev)
    {
        progressUIContainer.Add(ev.questId, (questTitles[curQuestShownCnt], questProgressText[curQuestShownCnt]));
        var quest = QuestManager.Instance.QuestStates[ev.questId];
        progressUIContainer[ev.questId].Item1.text = quest.data.title;
        int maxlen = Mathf.Min(3, quest.progress.Count);
        for (int i = 0; i < 3; i++)
        {
            if (i < quest.progress.Count)
                progressUIContainer[ev.questId].Item2[i].text = quest.progress[i].UIText;
            else
                progressUIContainer[ev.questId].Item2[i].text = "";
        }
        questInProgressPanel[curQuestShownCnt].SetActive(true);

        curQuestShownCnt++;
    }
    void OnQuestAbandoned()
    {

    }
    void OnQuestComplete()
    {

    }
    void OnInventoryChanged(InventoryChangedEvent ev)
    {
        foreach (var quest in QuestManager.Instance.QuestStates.Values)
        {
            if (quest.conditionTypes.Contains(QuestConditionType.Obtain) == false)
                continue;

            for(int i = 0; i < quest.data.conditionContexts.Length; i++)
            {
                if (quest.data.conditionContexts[i].type != QuestConditionType.Obtain)
                    continue;
                if (quest.data.conditionContexts[i].itemID != ev.itemId)
                    continue;

                progressUIContainer[quest.data.id].Item2[i].text = quest.progress[i].UIText;
            }
        }
    }

    void OnEnemyKilled(EnemyKilledEvent ev)
    {
        foreach (var quest in QuestManager.Instance.QuestStates.Values)
        {
            if (quest.conditionTypes.Contains(QuestConditionType.Kill) == false)
                continue;

            for (int i = 0; i < quest.data.conditionContexts.Length; i++)
            {
                if (quest.data.conditionContexts[i].type != QuestConditionType.Kill)
                    continue;
                if (quest.data.conditionContexts[i].enemyId != ev.enemyId)
                    continue;

                progressUIContainer[quest.data.id].Item2[i].text = quest.progress[i].UIText;
            }
        }
    }
}
