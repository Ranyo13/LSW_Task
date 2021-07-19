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
    [SerializeField]
    Transform sellingItemsPanel;
    [SerializeField]
    Transform shopOptionsPanel;
    public Transform buyingItemOption;
    public Transform sellingItemOption;
    PlayerStats playerStats;

    public Button buyButton;
    public Button sellButton;

    public bool firstEntrance = true;


    private void Awake()
    {
        //OpenBuyItemsMenu();
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
            OpenBuyItemsMenu();
            FindObjectOfType<AudioManager>().Play("EnterShop");
            FindObjectOfType<AudioManager>().Play("Greeting");




        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CloseMenu();
            FindObjectOfType<AudioManager>().Play("LeaveShop");
            
        }
    }

    public void BuyItem(int id)
    {
        if (playerStats.inventory.Count < 4 && playerStats.RemoveCoins(shopItems[id].cost) )
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

    public void SellItem(int id)
    {
        playerStats.AddCoins(playerStats.inventory[id].cost);
        playerStats.inventory.RemoveAt(id);
        playerStats.onInventoryChanged.Invoke();
        

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
        sellingItemsPanel.gameObject.SetActive(false);
        //shopOptionsPanel.gameObject.SetActive(false);
    }

    public void OpenBuyItemsMenu()
    {
        playerStats.onInventoryChanged.RemoveListener(OpenSellItemsMenu);
        buyingItemsPanel.gameObject.SetActive(true);
        sellingItemsPanel.gameObject.SetActive(false);
        //shopOptionsPanel.gameObject.SetActive(true);
        Transform content = buyingItemsPanel.Find("Scroll View/Viewport/Content");
        // Updates UI (No need for a function since it's always constant)
        buyButton.interactable = false;
        sellButton.interactable = true;
        if(firstEntrance)
        {
            for (int i = 0; i < shopItems.Count; i++)
            {
                int listenerInput = i;
                Transform itemOptionDuplicate = Instantiate(buyingItemOption, content);
                itemOptionDuplicate.gameObject.SetActive(true);
                itemOptionDuplicate.Find("Image").GetComponent<Image>().sprite = shopItems[i].itemEquipSprite;
                itemOptionDuplicate.Find("Price").GetComponent<Text>().text = shopItems[i].cost.ToString();
                itemOptionDuplicate.Find("Button").GetComponent<Button>().onClick.AddListener(() => BuyItem(listenerInput));
            }
            firstEntrance = false;
        }
        
    }

    public void OpenSellItemsMenu()
    {
        playerStats.onInventoryChanged.AddListener(OpenSellItemsMenu);
        buyingItemsPanel.gameObject.SetActive(false);
        sellingItemsPanel.gameObject.SetActive(true);
        buyButton.interactable = true;
        sellButton.interactable = false;
        Transform content = sellingItemsPanel.Find("Scroll View/Viewport/Content");
        // Updates UI (No need for a function since it's always constant)
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < playerStats.inventory.Count; i++)
        {
            int listenerInput = i;
            Transform itemOptionDuplicate = Instantiate(sellingItemOption.transform, content);
            itemOptionDuplicate.gameObject.SetActive(true);
            itemOptionDuplicate.Find("Image").GetComponent<Image>().sprite = playerStats.inventory[i].itemEquipSprite;
            itemOptionDuplicate.Find("Price").GetComponent<Text>().text = playerStats.inventory[i].cost.ToString();
            itemOptionDuplicate.Find("Button").GetComponent<Button>().onClick.AddListener(() => SellItem(listenerInput));
        }

    }

    public void PlaySwitchSound()
    {
        FindObjectOfType<AudioManager>().Play("Switch");
    }

    public void PlayClickSound()
    {
        FindObjectOfType<AudioManager>().Play("Click");
    }

    public void PlayExitSound()
    {
        FindObjectOfType<AudioManager>().Play("Exit");
    }

}
