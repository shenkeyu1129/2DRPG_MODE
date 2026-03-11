using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlots;
    public CanvasGroup statsCanvas;
    private bool StatsOpen = false;

    private void Start()
    {
        UpdateAll();
    }
    private void Update()
    {
        if (Input.GetButtonDown("ToggleStats"))
        {
            if (StatsOpen)
            {
                Time.timeScale = 1;
                UpdateAll();
                statsCanvas.alpha = 0;
                statsCanvas.blocksRaycasts = false;
                StatsOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                UpdateAll();
                statsCanvas.alpha = 1;
                statsCanvas.blocksRaycasts = true;
                StatsOpen = true;
            }
            
        }
    }
    public void UpdateDamage()
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = "Damage:" + StatsManager.Instance.damage;
    }
    public void UpdateSpeed()
    {
        statsSlots[1].GetComponentInChildren<TMP_Text>().text = "Speed:" + StatsManager.Instance.speed;
    }
    public void UpdateAll()
    {
        UpdateDamage();
        UpdateSpeed();
    }
}
