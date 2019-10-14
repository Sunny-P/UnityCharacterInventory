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

    public struct IntRange
    {
        public int min;
        public int max;
    }

    [Header("Item Types")]
    [HideInInspector] public ItemType itemType;
    [HideInInspector] public ArmourType armourType;
    [HideInInspector] public WeaponType weaponType;
    [HideInInspector] public EquipmentSlot equipmentSlot;

    [Header("Stat Bonuses")]
    [HideInInspector] public bool applyRandomStats;
    [HideInInspector] public int strength;
    [HideInInspector] public int agility;
    [HideInInspector] public int constitution;
    [HideInInspector] public int intellect;
    [HideInInspector] public int physicalArmour;
    [HideInInspector] public int magicalArmour;

    [Header("Random Range")]
    [HideInInspector] public IntRange strengthRange;
    [HideInInspector] public IntRange agilityRange;
    [HideInInspector] public IntRange constitutionRange;
    [HideInInspector] public IntRange intellectRange;
    [HideInInspector] public IntRange physicalArmourRange;
    [HideInInspector] public IntRange magicalArmourRange;

    [Header("Image")]
    [HideInInspector] public Sprite inventorySprite;

    // TODO: Add editor functionality to define how much inventorySpace we take up
    [Header("Inventory Space")]
    [HideInInspector] public int inventorySpaceX;
    [HideInInspector] public int inventorySpaceY;

    [HideInInspector] public string name;

    // TODO: Add a section for Tooltip on mouseover.

    // Possible room to add Trait section. List of Traits. Required for Production game
    // Trait randomness/chance needs to be an option as well

    // TODO: Add a function to apply random values to stats -> So we can also dynamically apply random stats on spawn rather than just our predefined item stats
    // Parameters: ref stat value to chance, minRandomRange value, maxRandomRange value
    public void RandomiseStatValues()
    {
        strength = Random.Range(strengthRange.min, strengthRange.max);
        agility = Random.Range(agilityRange.min, agilityRange.max);
        constitution = Random.Range(constitutionRange.min, constitutionRange.max);
        intellect = Random.Range(intellectRange.min, intellectRange.max);
        physicalArmour = Random.Range(physicalArmourRange.min, physicalArmourRange.max);
        magicalArmour = Random.Range(magicalArmourRange.min, magicalArmourRange.max);
    }
}
