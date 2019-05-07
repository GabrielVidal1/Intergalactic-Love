using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartQuestEvent : QuestEvent
{
    [SerializeField] private Quest quest;

    protected override IEnumerator Execute()
    {
        quest.StartQuest();
        yield break;
    }
}
