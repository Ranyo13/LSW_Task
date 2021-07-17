using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public Item headItem;
    public Item bodyItem;
    public List<Item> inventory;
    public Transform inventoryPanel;
    public SpriteRenderer playerBody;
    public SpriteRenderer playerHead;
    public TextMeshProUGUI coinsText;
    public UnityEvent onInventoryChanged;
    public UnityEvent onEquipHead;
    public UnityEvent onEquipBody;
    public Transform inventoryItemOption;

    public int coins;

    public void Awake()
    {

    }

    public void Debuggerino()
    {
        Debug.Log("Fired");
    }

    public void AddCoins(int value)
    {
        coins += value;
        coinsText.text = coins.ToString();
    }

    public bool RemoveCoins (int value)
    {
        if (coins >= value)
        {
            coins -= value;
            coinsText.text = coins.ToString();
            return true;

        }
        else
            return false;
 
    }

    public bool Equip (int index)
    {
        Debug.Log(index);
        if (inventory[index]!=null)
        {
            
            if (inventory[index].itemType == ItemType.Body)
            {
                inventory.Add(bodyItem);
                bodyItem = inventory[index];
                playerBody.sprite = bodyItem.itemEquipSprite;
                onEquipBody.Invoke();
                
                //Update Inventory and Equipment slots
            }
            else
            {
                inventory.Add(headItem);
                headItem = inventory[index];
                playerHead.sprite = headItem.itemEquipSprite;
                onEquipHead.Invoke();
            }
            inventory.RemoveAt(index);
            onInventoryChanged.Invoke();
        }
        return true;
    }

    public void CloseInventory()
    {
        inventoryPanel.gameObject.SetActive(false);
    }

    public void OpenInventory()
    {
        if (inventoryPanel.gameObject.activeSelf)
        {
            inventoryPanel.gameObject.SetActive(false);
            onInventoryChanged.RemoveListener(RefreshInventory);
        }
        else
        {
            RefreshInventory();
            inventoryPanel.gameObject.SetActive(true);
            onInventoryChanged.AddListener(RefreshInventory);


        }
    }

    public bool AddItemToInventory(Item item)
    {
        if (inventory.Count < 4)
        {
            inventory.Add(item);
            onInventoryChanged.Invoke();
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// Called to refresh inventory's UI
    /// </summary>
    public void RefreshInventory()
    {
        //foreach (Transform child in inventoryItemOption)
        //    Debug.Log(child);
        Transform content = inventoryPanel.Find("Scroll View/Viewport/Content");
        //Transform itemOption = content.Find("ItemOption");
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < inventory.Count; i++)
        {
            int listenerInput = i;
            Transform itemOptionDuplicate = Instantiate(inventoryItemOption.transform, content);
            itemOptionDuplicate.gameObject.SetActive(true);
            itemOptionDuplicate.Find("Image").GetComponent<Image>().sprite = inventory[i].itemEquipSprite;
            //itemOptionDuplicate.Find("Price").GetComponent<Text>().text = inventory[i].cost.ToString();
            itemOptionDuplicate.Find("Button").GetComponent<Button>().onClick.AddListener(() => Equip(listenerInput));
        }
    }

    // Start is called before the first frame update
    //void EquipArmor (BodyItem inBodyItem)
    //{
    //    //playerBody.sprite = inBodyItem.itemEquipSprite;
    //    //coins -= inBodyItem.cost;
    //}

}
