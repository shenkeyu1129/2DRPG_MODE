using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{

    public TMP_Text healthtext;
    public Animator healthTextAnim;
    private void Start()
    {
        healthtext.text = "HP:" + StatsManager.Instance.currentHealth + " / " + StatsManager.Instance.maxHealth;
    }
    public void ChangeHealth(int amount)
    {
        StatsManager.Instance.currentHealth += amount;
        healthTextAnim.Play("TextUpdate");
        healthtext.text = "HP:" + StatsManager.Instance.currentHealth + " / " + StatsManager.Instance.maxHealth;
        if (StatsManager.Instance.currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
