using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestSystem {
    private List<IQuestTask> _questTasks = new List<IQuestTask>();
    private List<GameObject> _questItems = new List<GameObject>();

    public void AddQuest(IQuestTask quest) {
        if(_questTasks.Contains(quest)) return;
        _questTasks.Add(quest);
    }

    public void AddQuestItem(GameObject item) {
        if(_questItems.Contains(item)) return;
        _questItems.Add(item);
    }

    public bool IsQuestItem(GameObject item) {
        return _questItems.Contains(item);
    }
}
