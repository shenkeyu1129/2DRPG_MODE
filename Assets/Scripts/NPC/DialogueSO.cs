using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DialogueSO",menuName ="Dialogue/DialogueNode")]
public class DialogueSO : ScriptableObject
{
    public DialogueLine[] lines;
    public DialogueOption[] options;
    [Header("Conditional Requirements (Optional)")]
    public ActorSO[] requiredNPCs;


    public bool isConditionMet()
    {
        if(requiredNPCs.Length>0)
        {
            foreach (var npc in requiredNPCs)
            {
                if(!DialogueHistoryTracker.Insatnce.HasSpokenWith(npc))
                {
                    return false;
                }
            }
        }
        return true;
    }
}
[System.Serializable]
public class DialogueLine
{
    public ActorSO speaker;
    [TextArea(3,5)]public string text;
}
[System.Serializable]
public class DialogueOption
{
    public string optionText;
    public DialogueSO nextDialogue;
}