using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseItemControl : MonoBehaviour
{
    public InventoryItem mouseItem;
    public InventoryBase invBase;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (mouseItem.enabled)
        {
            transform.position = Input.mousePosition;
        }
    }

    void PickUpItem()
    {

    }
}
