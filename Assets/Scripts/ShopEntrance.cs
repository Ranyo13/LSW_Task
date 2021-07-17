using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopEntrance : MonoBehaviour
{
    [SerializeField]
    List<Item> shopItems;
    [SerializeField]
    Transform buyingItemsPanel;

    PlayerStats playerStats;


    private void Awake()
    {
        Transform content = buyingItemsPanel.Find("Scroll View/Viewport/Content");
        Transform itemOption = content.Find("ItemOption");
        itemOption.gameObject.SetActive(false);
        for (int i = 0; i < shopItems.Count; i++)
        {
            int listenerInput = i;
            Transform itemOptionDuplicate = Instantiate(itemOption, content);
            itemOptionDuplicate.gameObject.SetActive(true);
            itemOptionDuplicate.Find("Image").GetComponent<Image>().sprite = shopItems[i].itemEquipSprite;
            itemOptionDuplicate.Find("Price").GetComponent<Text>().text = shopItems[i].cost.ToString();
            itemOptionDuplicate.Find("Button").GetComponent<Button>().onClick.AddListener(() => BuyItem(listenerInput));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerStats = collision.GetComponent<PlayerStats>();
            buyingItemsPanel.gameObject.SetActive(true);
           
            


        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CloseMenu();
        }
    }

    public void BuyItem(int id)
    {
        if (playerStats.RemoveCoins(shopItems[id].cost))
        {
            Item newItem = new Item();
            newItem = shopItems[id];
            playerStats.AddItemToInventory(newItem);
        }
            
        //if (shopItems[id].itemType == ItemType.Body)
        //{
        //    playerStats.playerBody.sprite = shopItems[id].itemEquipSprite;
        //    playerStats.RemoveCoins(shopItems[id].cost);
        //}
        //if (shopItems[id].itemType == ItemType.Head)
        //{
        //    playerStats.playerHead.sprite = shopItems[id].itemEquipSprite;
        //    playerStats.RemoveCoins(shopItems[id].cost);
        //}
    }

    public void CloseMenu()
    {
        buyingItemsPanel.gameObject.SetActive(false);
    }

}
