using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Weapon,
        Armour
    }

    public enum ArmourType
    {
        Light,
        Medium,
        Heavy
    }

    public enum WeaponType
    {
        Sword,
        Staff,
        Bow
    }

    public enum EquipmentSlot
    {
        Head,
        Shoulders,
        Chest,
        Legs,
        Feet,
        Hands,
        Weapon,
        Ring1,
        Ring2,
        Neck
    }

    [Header("Item Types")]
    public ItemType itemType;
    public ArmourType armourType;
    public WeaponType weaponType;
    public EquipmentSlot equipmentSlot;

    [Header("Stat Bonuses")]
    public bool applyRandomStats;
    public int strength;
    public int agility;
    public int constitution;
    public int intellect;
    public int physicalArmour;
    public int magicalArmour;

    [Header("Image")]
    public Sprite inventorySprite;

    // TODO: Add editor functionality to define how much inventorySpace we take up
    [Header("Inventory Space")]
    public int[,][,] inventorySpace;

    // TODO: Add a section for Tooltip on mouseover.

    // Possible room to add Trait section. List of Traits. Required for Production game
    // Trait randomness/chance needs to be an option as well

    // TODO: Add a function to apply random values to stats -> So we can also dynamically apply random stats on spawn rather than just our predefined item stats
    // Parameters: ref stat value to chance, minRandomRange value, maxRandomRange value
}
