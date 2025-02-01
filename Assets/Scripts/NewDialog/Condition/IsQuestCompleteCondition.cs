using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCondition", menuName = "Conditions/NewCondition")]
public class IsQuestCompleteCondition : Condition {
    [SerializeField]
    private List<Quest> _quests;

    protected override bool GetActualResult() {
        foreach (var quest in _quests) {
            if (!quest.IsComplete()) {
                return false;
            }
        }

        return true;
    }
}
