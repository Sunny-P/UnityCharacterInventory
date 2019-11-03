using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentPanelControl : MonoBehaviour
{
    [Header("Equip Slot Setting")]
    public GameObject equipmentSlot;
    public float slotWidth;
    public float slotHeight;
    public float offsetFromEdge;
    public List<Item.EquipmentSlot> equipmentSlotsUsed;

    [HideInInspector] public GameObject[] equipmentSlotsObj;
    [HideInInspector] public EquipmentSlot[] equipmentSlots;

    [Header("Inventory Reference")]
    public InventoryBase invBase;

    RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        equipmentSlotsObj = new GameObject[equipmentSlotsUsed.Count];
        equipmentSlots = new EquipmentSlot[equipmentSlotsUsed.Count];

        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateSlots()
    {
        int shiftedId = 0;
        // Create the equipment slots and put them in the UI positionally properly
        for (int i = 0; i < equipmentSlotsUsed.Count; i++)
        {
            GameObject createdSlot = Instantiate(equipmentSlot);
            equipmentSlotsObj[i] = createdSlot;
            equipmentSlots[i] = createdSlot.GetComponent<EquipmentSlot>();

            InventorySlot.SlotID ids;
            ids.x = i;
            ids.y = shiftedId;
            if (equipmentSlots[i].Initialise(gameObject, rect, slotWidth, slotHeight, offsetFromEdge, equipmentSlotsUsed[i], ids))
            {
                shiftedId++;
            }
        }
    }

    public bool EquipItem(InventoryItem invItem)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {

            if (invItem.item.equipmentSlot == equipmentSlots[i].equipmentSlot)
            {
                // Equip the item into this slot if this slot isn't used
                if (!equipmentSlots[i].isUsed)
                {
                    InventoryItem equippedItem = Instantiate(invBase.itemIcon).GetComponent<InventoryItem>();

                    Vector3 equipInterfacePos = equipmentSlots[i].gameObject.transform.position;
                    equipInterfacePos.x += (EquipmentSlot.width * 0.1f);
                    equipInterfacePos.y -= (EquipmentSlot.height * 0.1f);

                    equippedItem.Initialise(equipmentSlots[i].gameObject,
                        equippedItem.item,
                        equipInterfacePos,
                        Vector3.one);

                    equippedItem.SetItem(invItem, true);
                    equippedItem.isEquipped = true;
                    equippedItem.usedEquipSlot = equipmentSlots[i];

                    CharacterPanelStatControl.OnItemEquip(equippedItem.item);

                    equipmentSlots[i].isUsed = true;
                    equipmentSlots[i].equippedItem = equippedItem.item;

                    return true;
                }
            }
        }
        return false;
    }
}
