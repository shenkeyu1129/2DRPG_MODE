using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHistoryTracker : MonoBehaviour
{
    public  static DialogueHistoryTracker Insatnce;
    private readonly List<ActorSO> spokenNPCs = new List<ActorSO>();
    private void Awake()
    {
        if(Insatnce != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Insatnce = this;
        }
    }
    public void RecordNPC(ActorSO actorSO)
    {
        spokenNPCs.Add(actorSO);

    }
    public bool HasSpokenWith(ActorSO actorSO)
    {
        return spokenNPCs.Contains(actorSO);
    }
}
