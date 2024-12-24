using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestSystem {
    private Player _player;
    private List<IQuestTask> _questTasks = new List<IQuestTask>();
    private List<GameObject> _questItems = new List<GameObject>();

    public PlayerQuestSystem(Player player) {
        _player = player;
    }

    public void AddQuest(IQuestTask quest) {
        if(_questTasks.Contains(quest)) return;
        quest.Init(_player);
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
