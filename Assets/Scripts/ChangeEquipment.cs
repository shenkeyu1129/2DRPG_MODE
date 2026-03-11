using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEquipment : MonoBehaviour
{
    public PlayerBow bow;
    public PlayerCombat combat;

    void Update()
    {
        if(Input.GetButtonDown("ChangeEquipment"))
        {
            bow.enabled = !bow.enabled;
        }
    }
}
