using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCondition", menuName = "Conditions/NewCondition")]
public class IsQuestComplete : Condition {
    [SerializeField]
    private List<Quest> _quests;    

    public override bool IsTrue() {
        foreach (var quest in _quests) {
            if (!quest.IsComplete()) {
                return false;
            }
        }

        return true;
    }
}
