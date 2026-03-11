using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Talk : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public Animator interactAnim;
    public DialogueSO currentconversation;
    public List<DialogueSO> conversations;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    private void OnEnable()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        anim.Play("Idle");
        interactAnim.Play("Open");
    }
    private void OnDisable()
    {
        interactAnim.Play("Close");
        rb.isKinematic = false;
    }
    private void Update()
    {
        if(Input.GetButtonDown("Interact"))
        {
            if(DialogueManager.Instance.isDialogueActive)
            {
                DialogueManager.Instance.AdvanceDialogue();
            }
            else
            {
                CheckForNewConversation();
                DialogueManager.Instance.StartDialogue(currentconversation);
            }
        }
    }

    private void CheckForNewConversation()
    {
        for (int i = 0; i < conversations.Count; i++)
        {
            var convo = conversations[i];
            if(convo != null && convo.isConditionMet())
            {
                conversations.RemoveAt(i);
                currentconversation = convo;
            }
        }
    }
}
