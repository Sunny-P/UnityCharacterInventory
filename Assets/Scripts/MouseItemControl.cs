using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseItemControl : MonoBehaviour
{
    public InventoryItem mouseItem;
    public InventoryBase invBase;

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
            mouseItem.Initialise(gameObject, null, transform.position, new Vector3(1.0f, 1.0f));
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
                    // Is the currently checked object an inventoryItem
                    if (result.gameObject.name.Contains("InventoryItem"))
                    {
                        Debug.Log("InventoryItem hit with click");
                        InventoryItem checkedResult = result.gameObject.GetComponent<InventoryItem>();

                        // Put item on our mouse
                        mouseItem.gameObject.SetActive(true);
                        mouseItem.SetItem(checkedResult);

                        foreach (InventorySlot slot in checkedResult.slotsUsed)
                        {
                            slot.isUsed = false;
                            slot.storedItem = null;
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
                foreach (RaycastResult result in GetNewPointerEventRaycast())
                {
                    if (result.gameObject.name.Contains("InventorySlot"))
                    {
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
