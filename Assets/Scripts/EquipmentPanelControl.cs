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
}
