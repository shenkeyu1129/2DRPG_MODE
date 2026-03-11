using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopKeeper : MonoBehaviour
{
    public static ShopKeeper currentShopKeeper;
    public Animator anim;
    public CanvasGroup shopCanvasGroup;
    public ShopManager shopManager;
    public static event Action<ShopManager, bool> OnShopStateChanged;
    [SerializeField] private List<ShopItems> shopItems;
    [SerializeField] private List<ShopItems> shopWeapons;
    [SerializeField] private List<ShopItems> shopArmours;
    [SerializeField] private Camera shopKeeperCam;
    [SerializeField] private Vector3 cameraOffest = new Vector3(0,0,-1);
    private bool playerInRange;
    private bool isShopOpen;
    
    // Update is called once per frame
    void Update()
    {
        if(playerInRange)
        {
            if(Input.GetButtonDown("Interact"))
            {
                if(!isShopOpen)
                {
                    currentShopKeeper = this;
                    OnShopStateChanged?.Invoke(shopManager, true);
                    isShopOpen = true;
                    shopCanvasGroup.alpha = 1;
                    shopCanvasGroup.blocksRaycasts = true;
                    shopCanvasGroup.interactable = true;
                    shopKeeperCam.transform.position = transform.position + cameraOffest;
                    shopKeeperCam.gameObject.SetActive(true);
                    OpenItemShop();
                }
                else if (isShopOpen)
                {
                    currentShopKeeper = null;
                    OnShopStateChanged?.Invoke(shopManager, false);
                    isShopOpen = false;
                    shopCanvasGroup.alpha = 0;
                    shopCanvasGroup.blocksRaycasts = false;
                    shopCanvasGroup.interactable = false;
                    shopKeeperCam.gameObject.SetActive(false);
                }
            }
           
        }
    }
    public void OpenItemShop()
    {
        shopManager.PopulateShopItems(shopItems);
    }
    public void OpenWeaponShop()
    {
        shopManager.PopulateShopItems(shopWeapons);
    }
    public void OpenArmourShop()
    {
        shopManager.PopulateShopItems(shopArmours);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            anim.SetBool("PlayerInRange", true);
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("PlayerInRange", false);
            playerInRange = false;
        }
    }
}
