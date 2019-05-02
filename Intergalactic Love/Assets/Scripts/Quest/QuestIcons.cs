using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIcons : MonoBehaviour
{
    [SerializeField] private MeshRenderer questQuad1;
    [SerializeField] private MeshRenderer questQuad2;

    public void Disable()
    {
        questQuad1.gameObject.SetActive(false);
        questQuad2.gameObject.SetActive(false);
    }

    public void SetQuest(Quest.QuestType questType)
    {
        questQuad1.gameObject.SetActive(true);
        questQuad2.gameObject.SetActive(true);

        switch (questType)
        {
            case Quest.QuestType.NewQuest:
                questQuad1.material = GameManager.gm.questManager.newQuest;
                questQuad2.material = GameManager.gm.questManager.newQuest;
                break;
            case Quest.QuestType.UpdateQuestNone:
                questQuad1.material = GameManager.gm.questManager.updateQuestNone;
                questQuad2.material = GameManager.gm.questManager.updateQuestNone;
                break;
            case Quest.QuestType.UpdateQuest:
                questQuad1.material = GameManager.gm.questManager.updateQuest;
                questQuad2.material = GameManager.gm.questManager.updateQuest;
                break;
        }
    }

}
