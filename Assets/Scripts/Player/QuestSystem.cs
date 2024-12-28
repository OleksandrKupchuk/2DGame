using System.Collections.Generic;
using UnityEngine;

public class QuestSystem {
    private List<IQuest> _quests = new List<IQuest>();
    private List<GameObject> _questItems = new List<GameObject>();

    public void AddQuest(IQuest quest) {
        if(_quests.Contains(quest)) return;
        System.Console.WriteLine("quest was added to player");
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
