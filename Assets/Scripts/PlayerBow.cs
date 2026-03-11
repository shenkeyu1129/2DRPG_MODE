using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBow : MonoBehaviour
{

    public Transform launchPoint;
    public float shootCooldown = 0.5f;
    public Animator anim;
    public PlayerMovement playerMovement;
    private string arrowPoolTag = "Arrow";
    private float shootTimer;
    public Vector2 animDrection = Vector2.right;
    void Update()
    {
        shootTimer -= Time.deltaTime;

        HandleAiming();
        if(Input.GetButtonDown("Shoot") && shootTimer<=0)
        {
            playerMovement.isShooting = true;
            anim.SetBool("isShooting", true);
        }
       
    }
    private void OnEnable()
    {
        anim.SetLayerWeight(0, 0);
        anim.SetLayerWeight(1, 1);
    }
    private void OnDisable()
    {
        anim.SetLayerWeight(0, 1);
        anim.SetLayerWeight(1, 0);
    }
    private void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if(horizontal!=0 || vertical!=0)
        {

            animDrection = new Vector2(horizontal, vertical).normalized;
            anim.SetFloat("animX", animDrection.x);
            anim.SetFloat("animY", animDrection.y);
        }
    }
    public void Shoot()
    {
        if(shootTimer <= 0)
        {
            GameObject obj = ObjectPoolManager.Instance.SpawnFromPool(
           arrowPoolTag,
           launchPoint.position,
           Quaternion.identity
       );
            Arrow arrow = obj.GetComponent<Arrow>();
            arrow.direction = animDrection;
            shootTimer = shootCooldown;
        }
      
        anim.SetBool("isShooting", false);
        playerMovement.isShooting = false;
    }
}
