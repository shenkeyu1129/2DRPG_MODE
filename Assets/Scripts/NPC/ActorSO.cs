using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ActorName",menuName ="Dialogue/NPC")]
public class ActorSO : ScriptableObject
{
    public string actorName;
    public Sprite portrait;
}
