using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBase : MonoBehaviour
{
    [Header("Slots")]
    public GameObject slot;
    public float slotWidth;
    public float slotHeight;

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
    [HideInInspector] public InventoryItem mouseItem;
    [HideInInspector] public Item itemOnMouse;

    RectTransform baseRect;

    // Start is called before the first frame update
    void Start()
    {
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

    void AddItem(Item itemToAdd)
    {

    }

    void RemoveItemAt(InventorySlot.SlotID id)
    {

    }
}
