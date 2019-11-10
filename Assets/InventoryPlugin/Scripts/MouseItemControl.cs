using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseItemControl : MonoBehaviour
{
    public InventoryItem mouseItem;
    public InventoryBase invBase;
    EquipmentPanelControl equipPanelControl = null;

    public Canvas inventoryCanvas;

    GraphicRaycaster raycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

    // Start is called before the first frame update
    void Start()
    {
        raycaster = inventoryCanvas.GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }

    private void Awake()
    {
        if (mouseItem != null)
        {
            mouseItem.Initialise(gameObject, null, transform.position, new Vector3(1.0f, 1.0f), true);
            mouseItem.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckItemClick();
        if (mouseItem.enabled)
        {
            transform.position = Input.mousePosition;
        }
    }

    void CheckItemClick()
    {
        // On a left click
        if (Input.GetMouseButtonDown(0))
        {
            // If we need to grab an item
            if (mouseItem.item == null)
            {
                foreach (RaycastResult result in GetNewPointerEventRaycast())
                {
                    // Picking up Items in Inventory or from an Equip Slot
                    // Is the currently checked object an inventoryItem
                    if (result.gameObject.name.Contains("InventoryItem"))
                    {
                        Debug.Log("InventoryItem hit with click");
                        InventoryItem checkedResult = result.gameObject.GetComponent<InventoryItem>();

                        // Put item on our mouse
                        mouseItem.gameObject.SetActive(true);
                        mouseItem.SetItem(checkedResult);

                        // Item was equipped - unequipping now
                        if (checkedResult.isEquipped)
                        {
                            checkedResult.usedEquipSlot.isUsed = false;
                            checkedResult.usedEquipSlot.equippedItem = null;

                            mouseItem.isEquipped = true;
                            mouseItem.usedEquipSlot = checkedResult.usedEquipSlot;
                            //checkedResult.usedEquipSlot = null;
                            CharacterPanelStatControl.OnItemRemove(mouseItem.item);
                        }
                        else // Item was in inventory
                        {
                            foreach (InventorySlot slot in checkedResult.slotsUsed)
                            {
                                slot.isUsed = false;
                                slot.storedItem = null;
                            }
                        }

                        Destroy(checkedResult.gameObject);
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0)) // We have an item grabbed
        {
            if (mouseItem.item != null)
            {
                // Check if we are hovering over an inventory slot we can drop the item into
                List<RaycastResult> mouseRaycastList = GetNewPointerEventRaycast();
                int incrementsWithoutUsableSlot = 0;
                foreach (RaycastResult result in mouseRaycastList)
                {
                    incrementsWithoutUsableSlot++;
                    // Item is being put in inventory slot
                    if (result.gameObject.name.Contains("InventorySlot"))
                    {
                        incrementsWithoutUsableSlot--;
                        InventorySlot checkedSlot = result.gameObject.GetComponent<InventorySlot>();

                        if (invBase.AddItem(mouseItem.item, checkedSlot.slotID))
                        {
                            Debug.Log("ITEM WAS ADDED FROM MOUSE");

                            mouseItem.ClearItem();
                            mouseItem.gameObject.SetActive(false);
                        }
                        else // Otherwise put the item back in the inventory slots it was previously in
                        {
                            invBase.AddItem(mouseItem.item, mouseItem.slotsUsed[0].slotID);

                            Debug.Log("ITEM WAS ADDED FROM MOUSE");

                            mouseItem.ClearItem();
                            mouseItem.gameObject.SetActive(false);
                        }
                    }
                    // Hovering over character render, so dropping here equips the item. Equip the item
                    else if (result.gameObject.name.Contains("Character Render Surface"))
                    {
                        if (equipPanelControl == null)
                        {
                            equipPanelControl = result.gameObject.GetComponent<EquipmentPanelControl>();
                        }

                        if (equipPanelControl.EquipItem(mouseItem))
                        {
                            incrementsWithoutUsableSlot--;
                            mouseItem.ClearItem();
                            mouseItem.gameObject.SetActive(false);
                        }

                        //for (int i = 0; i < equipPanelControl.equipmentSlots.Length-1; i++)
                        //{
                            
                        //    if (mouseItem.item.equipmentSlot == equipPanelControl.equipmentSlots[i].equipmentSlot)
                        //    {
                        //        // Equip the item into this slot if this slot isn't used
                        //        if (!equipPanelControl.equipmentSlots[i].isUsed)
                        //        {
                        //            incrementsWithoutUsableSlot--;

                        //            InventoryItem equippedItem = Instantiate(invBase.itemIcon).GetComponent<InventoryItem>();

                        //            Vector3 equipInterfacePos = equipPanelControl.equipmentSlots[i].gameObject.transform.position;
                        //            equipInterfacePos.x += (EquipmentSlot.width * 0.1f);
                        //            equipInterfacePos.y -= (EquipmentSlot.height * 0.1f);

                        //            equippedItem.Initialise(equipPanelControl.equipmentSlots[i].gameObject, 
                        //                equippedItem.item,
                        //                equipInterfacePos, 
                        //                Vector3.one);

                        //            equippedItem.SetItem(mouseItem, true);
                        //            equippedItem.isEquipped = true;
                        //            equippedItem.usedEquipSlot = equipPanelControl.equipmentSlots[i];

                        //            CharacterPanelStatControl.OnItemEquip(equippedItem.item);

                        //            equipPanelControl.equipmentSlots[i].isUsed = true;
                        //            equipPanelControl.equipmentSlots[i].equippedItem = equippedItem.item;

                                    
                        //            break;
                        //        }
                        //    }
                        //}
                    }
                }
                // Checking if the number of increments done in the list equals the list size
                // Meaning no inventory slots were hovered over
                // We are placing the item back in the slot it was originally in
                if (incrementsWithoutUsableSlot == mouseRaycastList.Count)
                {

                    if (invBase.AddItem(mouseItem.item, mouseItem.slotsUsed[0].slotID))
                    {
                        Debug.Log("ITEM WAS ADDED FROM MOUSE - OUT OF INVENTORY SLOT BOUNDS");

                        mouseItem.ClearItem();
                        mouseItem.gameObject.SetActive(false);
                    }
                    else
                    {
                        if (invBase.AddItem(mouseItem.item))
                        {
                            Debug.Log("ITEM WAS ADDED FROM MOUSE - OUT OF INVENTORY SLOT BOUNDS");

                            mouseItem.ClearItem();
                            mouseItem.gameObject.SetActive(false);
                        }
                        else
                        {
                            
                            if (mouseItem.isEquipped)
                            {
                                Debug.Log("MouseItemControl: Re-equipping item. Not enough free inventory space to unequip");
                                // Re-equip item in its slot.
                                // Not enough inventory space
                                equipPanelControl.EquipItem(mouseItem);

                                mouseItem.ClearItem();
                                mouseItem.gameObject.SetActive(false);
                            }
                            else
                            {
                                Debug.Log("MouseItemControl: Item was out of bounds. Tried adding to own slots, then any slots. This shouldn't be hit");
                            }
                        }
                    }
                    
                }
            }
        }
    }

    List<RaycastResult> GetNewPointerEventRaycast()
    {
        pointerEventData = new PointerEventData(eventSystem);

        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        raycaster.Raycast(pointerEventData, results);

        return results;
    }
}
