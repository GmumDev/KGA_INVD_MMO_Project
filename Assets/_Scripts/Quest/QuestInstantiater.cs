using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestInstantiater
{
    static QuestInstantiater instance;
    public static QuestInstantiater Instance
    {
        get
        {
            if (instance == null)
                instance = new QuestInstantiater();
            return instance;
        }
    }
    private QuestInstantiater() { }


    public Quest InstantiateQuestFromID(string id)
    {
        QuestSO so = QuestDB.Instance.GetSO(id);

		return new Quest(so);
    }
}
