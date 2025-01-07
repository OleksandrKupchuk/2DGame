using System.Collections.Generic;
using UnityEngine;

public class QuestSystem {
    private static QuestSystem _instance;
    private List<IQuest> _quests = new List<IQuest>();
    private List<GameObject> _questItems = new List<GameObject>();

    public static QuestSystem Instance { get { 
            if(_instance == null) {
                _instance = new QuestSystem();
            }

            return _instance;
        }
    }

    private QuestSystem() { }

    public void AddQuest(IQuest quest) {
        if(_quests.Contains(quest)) return;
        Debug.Log("quest was added to player");
        _quests.Add(quest);
    }

    public void AddQuestItem(GameObject item) {
        if(_questItems.Contains(item)) return;
        _questItems.Add(item);
    }

    public bool IsQuestItem(GameObject item) {
        return _questItems.Contains(item);
    }
}
