using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elvations_Entry : MonoBehaviour
{
    public Collider2D[] mountations;
    public Collider2D[] boundrys;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.tag == "Player")
            {
                foreach (Collider2D mountation in mountations)
                {
                    mountation.enabled = false;

                }
                foreach (Collider2D boundry in boundrys)
                {

                    boundry.enabled = true;

                }
            }
      
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;

        }
    }
}
