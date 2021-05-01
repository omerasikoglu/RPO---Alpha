using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public ConversationScript[] conversations;
    
    Quest activeQuest = null;

    Quest[] quests;

    //GameModel model = Schedule.GetModel<GameModel>();

    void OnEnable()
    {
        quests = gameObject.GetComponentsInChildren<Quest>();
    }


}
