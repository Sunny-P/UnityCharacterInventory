using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipPanelSprites : MonoBehaviour
{
    [SerializeField]
    Sprite headSlot = null;
    [SerializeField]
    Sprite shouldersSlot = null;
    [SerializeField]
    Sprite chestSlot = null;
    [SerializeField]
    Sprite legsSlot = null;
    [SerializeField]
    Sprite feetSlot = null;
    [SerializeField]
    Sprite handsSlot = null;
    [SerializeField]
    Sprite weaponSlot = null;
    [SerializeField]
    Sprite ring1Slot = null;
    [SerializeField]
    Sprite ring2Slot = null;
    [SerializeField]
    Sprite neckSlot = null;

    static public Sprite head;
    static public Sprite shoulders;
    static public Sprite chest;
    static public Sprite legs;
    static public Sprite feet;
    static public Sprite hands;
    static public Sprite weapon;
    static public Sprite ring1;
    static public Sprite ring2;
    static public Sprite neck;

    private void Start()
    {
        head = headSlot;
        shoulders = shouldersSlot;
        chest = chestSlot;
        legs = legsSlot;
        feet = feetSlot;
        hands = handsSlot;
        weapon = weaponSlot;
        ring1 = ring1Slot;
        ring2 = ring2Slot;
        neck = neckSlot;
    }
}
