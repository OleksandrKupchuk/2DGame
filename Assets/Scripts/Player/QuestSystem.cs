using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestSystem")]
public class QuestSystem : ScriptableObject {
    private List<Quest> _quests = new List<Quest>();
    private List<GameObject> _questItems = new List<GameObject>();

    public void AddQuest(Quest quest) {
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
