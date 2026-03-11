using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public enum NPCState
    {
        Default,
        Idle,
        Patrol,
        Wander,
        Talk
    }

    public NPCState currentState = NPCState.Patrol;
    
    public NPC_Patrol patrol;
    public NPC_Talk talk;
    public NPC_Wander wander;

    private NPCState defaultState;

    private void Start()
    {
        defaultState = currentState;
        SwitchState(currentState);
    }

    public void SwitchState(NPCState newStaet)
    {
        currentState = newStaet;
        patrol.enabled = newStaet == NPCState.Patrol;
        talk.enabled = newStaet == NPCState.Talk;
        wander.enabled = newStaet == NPCState.Wander;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SwitchState(NPCState.Talk);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SwitchState(defaultState);
        }
    }
}
