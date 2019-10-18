using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBase : MonoBehaviour
{
    [Header("Slots")]
    public GameObject slot;
    public float width;
    public float height;
    public static float slotWidth;
    public static float slotHeight;

    [Header("Inventory")]
    public int inventorySlotsWide;
    public int inventorySlotsHigh;
    GameObject[,] inventorySlotsObj;
    InventorySlot[,] inventorySlots;

    [Header("Border")]
    public float backgroundBorderWidth;

    // Do we have an item on our mouse, not in inventory. Such as picking it up and moving it
    // grabbing an item to drop it, etc. etc.
    [HideInInspector] public bool isItemOnMouse;
    // IDK which type of item class i'll need
    [HideInInspector] public MouseItemControl mouseItemControl;
    [HideInInspector] public Item itemOnMouse;

    [Header("Inventory Icon Spawn")]
    public GameObject itemIcon;

    RectTransform baseRect;

    // ITEMS FOR TEST SPAWNING
    [Header("TESTING")]
    public Item item1;
    public Item item2;

    // Start is called before the first frame update
    void Start()
    {
        slotWidth = width;
        slotHeight = height;

        baseRect = GetComponent<RectTransform>();
        inventorySlotsObj = new GameObject[inventorySlotsWide, inventorySlotsHigh];
        inventorySlots = new InventorySlot[inventorySlotsWide, inventorySlotsHigh];

        Vector2 invSize = new Vector2();
        invSize.x = (backgroundBorderWidth * 2) + (inventorySlotsWide * slotWidth);
        invSize.y = (backgroundBorderWidth * 2) + (inventorySlotsHigh * slotHeight);

        baseRect.sizeDelta = invSize;

        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            AddItem(item1);
            Debug.Log("Item1 should be added");
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            AddItem(item2);
            Debug.Log("Item2 should be added");
        }
    }

    void CreateSlots()
    {
        for (int i = 0; i < inventorySlotsWide; i++)
        {
            for (int j = 0; j < inventorySlotsHigh; j++)
            {
                GameObject createdSlot = Instantiate(slot);
                inventorySlotsObj[i, j] = createdSlot;
                inventorySlots[i, j] = createdSlot.GetComponent<InventorySlot>();

                InventorySlot.SlotID slotId;
                slotId.x = i;
                slotId.y = j;
                inventorySlots[i, j].Initialise(gameObject, slotWidth, slotHeight, backgroundBorderWidth, slotId);
            }
        }
    }

    public void AddItem(Item itemToAdd)
    {
        bool itemAdded = false;
        bool canItemBeAdded = true;
        List<InventorySlot> addToSlots = new List<InventorySlot>();
        for (int i = 0; i < inventorySlotsWide; i++)
        {
            canItemBeAdded = true;
            for (int j = 0; j < inventorySlotsHigh; j++)
            {
                canItemBeAdded = true;
                if (!inventorySlots[i, j].isUsed)
                {
                    addToSlots.Add(inventorySlots[i, j]);

                    // Check if the whole item can fit in this part of the inventory
                    for (int itemWidth = 0; itemWidth < itemToAdd.inventorySpaceX; itemWidth++)
                    {
                        // If we are going out of range of the array, break
                        if (i + itemWidth >= inventorySlotsWide)
                        {
                            addToSlots.Clear();
                            canItemBeAdded = false;
                            break;
                        }
                        else if (itemAdded)
                        {
                            addToSlots.Clear();
                            break;
                        }
                        else if (!canItemBeAdded)
                        {
                            addToSlots.Clear();
                            break;
                        }

                        for (int itemHeight = 0; itemHeight < itemToAdd.inventorySpaceY; itemHeight++)
                        {
                            // If we are going out of range of the array, break
                            if (j + itemHeight >= inventorySlotsHigh)
                            {
                                addToSlots.Clear();
                                canItemBeAdded = false;
                                break;
                            }
                            else if (itemAdded)
                            {
                                addToSlots.Clear();
                                break;
                            }
                            else if (!canItemBeAdded)
                            {
                                addToSlots.Clear();
                                break;
                            }

                            if (inventorySlots[i + itemWidth, j + itemHeight].isUsed)
                            {
                                addToSlots.Clear();
                                canItemBeAdded = false;
                                break;
                            }
                            else
                            {
                                addToSlots.Add(inventorySlots[i + itemWidth, j + itemHeight]);
                            }
                            
                            // If we have checked the whole width & height of the item
                            // We can add it to these slots
                            //Debug.Log("itemHeight = " + (itemHeight + 1) + "| itemSpaceY = " + itemToAdd.inventorySpaceY);
                            if (itemHeight+1 == itemToAdd.inventorySpaceY)
                            {
                                //Debug.Log("itemWidth = " + itemWidth + "| itemSpaceX = " + itemToAdd.inventorySpaceX);
                                if (itemWidth+1 == itemToAdd.inventorySpaceX)
                                {
                                    if (canItemBeAdded)
                                    {
                                        Debug.Log("Attempting to instantiate new inventory item");
                                        GameObject newItemObj = Instantiate(itemIcon);
                                        InventoryItem newItem = newItemObj.GetComponent<InventoryItem>();
                                        newItem.Initialise(gameObject, itemToAdd, inventorySlots[i, j].transform.position, new Vector3(0.95f, 0.95f), new Vector2(-0.025f, 1.025f));
                                        newItem.slotsUsed = addToSlots;
                                        foreach (InventorySlot slot in addToSlots)
                                        {
                                            slot.isUsed = true;
                                            slot.storedItem = newItem.item;
                                        }
                                        itemAdded = true;
                                    }
                                }
                            }
                        }
                    }
                }
                if (itemAdded)
                {
                    break;
                }
            }
            if (itemAdded)
            {
                break;
            }
        }
        if (!itemAdded)
        {
            addToSlots.Clear();
            Debug.Log("Item can't be added");
        }
    }

    public bool AddItem(Item itemToAdd, InventorySlot.SlotID atID)
    {
        bool itemAdded = false;
        bool canItemBeAdded = true;
        List<InventorySlot> addToSlots = new List<InventorySlot>();

        if (!inventorySlots[atID.x, atID.y].isUsed)
        {
            addToSlots.Add(inventorySlots[atID.x, atID.y]);

            // Check if the whole item can fit in this part of the inventory
            for (int itemWidth = 0; itemWidth < itemToAdd.inventorySpaceX; itemWidth++)
            {
                // If we are going out of range of the array, break
                if (atID.x + itemWidth >= inventorySlotsWide)
                {
                    addToSlots.Clear();
                    canItemBeAdded = false;
                    break;
                }
                else if (itemAdded)
                {
                    addToSlots.Clear();
                    break;
                }
                else if (!canItemBeAdded)
                {
                    addToSlots.Clear();
                    break;
                }

                for (int itemHeight = 0; itemHeight < itemToAdd.inventorySpaceY; itemHeight++)
                {
                    // If we are going out of range of the array, break
                    if (atID.y + itemHeight >= inventorySlotsHigh)
                    {
                        addToSlots.Clear();
                        canItemBeAdded = false;
                        break;
                    }
                    else if (itemAdded)
                    {
                        addToSlots.Clear();
                        break;
                    }
                    else if (!canItemBeAdded)
                    {
                        addToSlots.Clear();
                        break;
                    }

                    if (inventorySlots[atID.x + itemWidth, atID.y + itemHeight].isUsed)
                    {
                        addToSlots.Clear();
                        canItemBeAdded = false;
                        break;
                    }
                    else
                    {
                        addToSlots.Add(inventorySlots[atID.x + itemWidth, atID.y + itemHeight]);
                    }

                    // If we have checked the whole width & height of the item
                    // We can add it to these slots
                    //Debug.Log("itemHeight = " + (itemHeight + 1) + "| itemSpaceY = " + itemToAdd.inventorySpaceY);
                    if (itemHeight + 1 == itemToAdd.inventorySpaceY)
                    {
                        //Debug.Log("itemWidth = " + itemWidth + "| itemSpaceX = " + itemToAdd.inventorySpaceX);
                        if (itemWidth + 1 == itemToAdd.inventorySpaceX)
                        {
                            if (canItemBeAdded)
                            {
                                Debug.Log("Attempting to instantiate new inventory item");
                                GameObject newItemObj = Instantiate(itemIcon);
                                InventoryItem newItem = newItemObj.GetComponent<InventoryItem>();
                                newItem.Initialise(gameObject, itemToAdd, inventorySlots[atID.x, atID.y].transform.position, new Vector3(0.95f, 0.95f), new Vector2(-0.025f, 1.025f));
                                newItem.slotsUsed = addToSlots;
                                foreach (InventorySlot slot in addToSlots)
                                {
                                    slot.isUsed = true;
                                    slot.storedItem = newItem.item;
                                }
                                itemAdded = true;
                                return itemAdded;
                            }
                        }
                    }
                }
            }
        }
        Debug.Log(itemAdded);
        return itemAdded;
    }

    void RemoveItemAt(InventorySlot.SlotID id)
    {

    }
}
