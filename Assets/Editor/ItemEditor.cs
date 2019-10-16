using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(Item))]
//[CanEditMultipleObjects]
public class ItemEditor : Editor
{
    Item itemScript;

    //struct IntRange
    //{
    //    public int min;
    //    public int max;
    //}
    //IntRange strengthRange;
    //IntRange agilityRange;
    //IntRange constitutionRange;
    //IntRange intellectRange;
    //IntRange physicalArmourRange;
    //IntRange magicalArmourRange;

    delegate void ItemDelegate();
    ItemDelegate methodToCall;

    SerializedProperty itemInventoryImage;
    SerializedProperty itemEquippedImage;

    struct ItemSizeSlot
    {
        public Rect rect;
        public int xId;
        public int yId;
        public bool selected;

        public ItemSizeSlot(Rect rect, int xID, int yID, bool isSelected)
        {
            this.rect = rect;
            xId = xID;
            yId = yID;
            selected = isSelected;
        }
    }

    List<ItemSizeSlot> slots;

    // Start is called before the first frame update
    private void OnEnable()
    {
        itemScript = target as Item;
        itemInventoryImage = serializedObject.FindProperty("inventorySprite");
        itemEquippedImage = serializedObject.FindProperty("equippedSprite");

        slots = new List<ItemSizeSlot>();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        
        EditorGUILayout.LabelField("Types", EditorStyles.boldLabel);

        EditorGUILayout.LabelField("Item Type");
        itemScript.itemType = (Item.ItemType)EditorGUILayout.EnumPopup(itemScript.itemType);

        if (itemScript.itemType == Item.ItemType.Armour)
        {
            EditorGUILayout.LabelField("Armour Type");
            itemScript.armourType = (Item.ArmourType)EditorGUILayout.EnumPopup(itemScript.armourType);
        }
        else
        {
            EditorGUILayout.LabelField("Weapon Type");
            itemScript.weaponType = (Item.WeaponType)EditorGUILayout.EnumPopup(itemScript.weaponType);
        }

        EditorGUILayout.LabelField("Equipment Slot");
        itemScript.equipmentSlot = (Item.EquipmentSlot)EditorGUILayout.EnumPopup(itemScript.equipmentSlot);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Stat Bonuses", EditorStyles.boldLabel);

        itemScript.applyRandomStats = GUILayout.Toggle(itemScript.applyRandomStats, "Use Random Item Stats?");

        if (!itemScript.applyRandomStats)
        {
            float statLabelWidth = 95.0f;
            CreateLabelIntSliderField(ref itemScript.strength, "Strength", statLabelWidth, 0, 100);
            CreateLabelIntSliderField(ref itemScript.agility, "Agility", statLabelWidth, 0, 100);
            CreateLabelIntSliderField(ref itemScript.constitution, "Constitution", statLabelWidth, 0, 100);
            CreateLabelIntSliderField(ref itemScript.intellect, "Intellect", statLabelWidth, 0, 100);
            CreateLabelIntSliderField(ref itemScript.physicalArmour, "Physical Armour", statLabelWidth, 0, 100);
            CreateLabelIntSliderField(ref itemScript.magicalArmour, "Magical Armour", statLabelWidth, 0, 100);
        }
        else
        {
            float statLabelWidth = 95.0f;
            CreateLabelWithRange("Strength", statLabelWidth, ref itemScript.strengthRange.min, ref itemScript.strengthRange.max);
            CreateLabelWithRange("Agility", statLabelWidth, ref itemScript.agilityRange.min, ref itemScript.agilityRange.max);
            CreateLabelWithRange("Constitution", statLabelWidth, ref itemScript.constitutionRange.min, ref itemScript.constitutionRange.max);
            CreateLabelWithRange("Intellect", statLabelWidth, ref itemScript.intellectRange.min, ref itemScript.intellectRange.max);
            CreateLabelWithRange("Physical Armour", statLabelWidth, ref itemScript.physicalArmourRange.min, ref itemScript.physicalArmourRange.max);
            CreateLabelWithRange("Magical Armour", statLabelWidth, ref itemScript.magicalArmourRange.min, ref itemScript.magicalArmourRange.max);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Str: " + itemScript.strength, EditorStyles.label, GUILayout.Width(90));
            EditorGUILayout.LabelField("Agi: " + itemScript.agility, EditorStyles.label, GUILayout.Width(90));
            EditorGUILayout.LabelField("Con: " + itemScript.constitution, EditorStyles.label, GUILayout.Width(90));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Int: " + itemScript.intellect, EditorStyles.label, GUILayout.Width(70));
            EditorGUILayout.LabelField("Phys Arm: " + itemScript.physicalArmour, EditorStyles.label, GUILayout.Width(90));
            EditorGUILayout.LabelField("Magi Arm: " + itemScript.magicalArmour, EditorStyles.label, GUILayout.Width(90));
            EditorGUILayout.EndHorizontal();

            //methodToCall = RandomiseStatValues;
            CreateButton("Randomise Stat Values", methodToCall = itemScript.RandomiseStatValues);

            EditorGUILayout.Space();

            EditorGUILayout.HelpBox("Note: that the Max range value is exclusive. \nWill return a randomised value from Min to (Max-1)", MessageType.Info, true);
        }
        EditorGUILayout.Space();

        //Sprite invImage = itemScript.inventorySprite;
        EditorGUILayout.PropertyField(itemInventoryImage, new GUIContent("Inventory Icon"));
        EditorGUILayout.PropertyField(itemEquippedImage, new GUIContent("Equipped Icon"));
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Item Inventory Size", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox("Click on the tiles to determine the items size it will be in the inventory. \nOr type in the Width and Height in the fields below.", MessageType.Info, true);

        Rect inventorySize = GUILayoutUtility.GetLastRect();

        Color backgroundColour = new Color(0.1f, 0.1f, 0.1f, 0.9f);
        Color selectedColour = new Color(1.0f, 0.0f, 0.0f, 1);
        Color unselectedColour = new Color(1.0f, 0.0f, 0.0f, 0.25f);

        DrawInventorySizeSelection(inventorySize, 20, 20, 5, 5, 1, backgroundColour, selectedColour, unselectedColour);

        InsertSpace(19);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Item Size Width");
        itemScript.inventorySpaceX = EditorGUILayout.DelayedIntField(itemScript.inventorySpaceX);
        if (itemScript.inventorySpaceX > 5)
        {
            itemScript.inventorySpaceX = 5;
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Item Size Height");
        itemScript.inventorySpaceY = EditorGUILayout.DelayedIntField(itemScript.inventorySpaceY);
        if (itemScript.inventorySpaceY > 5)
        {
            itemScript.inventorySpaceY = 5;
        }
        EditorGUILayout.EndHorizontal();

        EvaluateManualItemSize();

        slots.Clear();
        EditorUtility.SetDirty(itemScript);
    }

    void CreateLabelIntSliderField(ref int stat, string statName, float nameLabelWidth, int rangeMin, int rangeMax)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(statName, GUILayout.Width(nameLabelWidth));
        stat = EditorGUILayout.IntSlider(stat, rangeMin, rangeMax);
        EditorGUILayout.EndHorizontal();
    }

    void CreateLabelWithRange(string statName, float nameLabelWidth, ref int rangeMin, ref int rangeMax)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(statName, GUILayout.Width(nameLabelWidth));
        EditorGUILayout.LabelField("| Range:    Min", GUILayout.Width(80));
        rangeMin = EditorGUILayout.IntField(rangeMin, GUILayout.Width(30));
        EditorGUILayout.LabelField("    Max: ", GUILayout.Width(50));
        rangeMax = EditorGUILayout.IntField(rangeMax, GUILayout.Width(30));
        EditorGUILayout.EndHorizontal();

        if (rangeMin > rangeMax)
        {
            rangeMax = rangeMin;
        }
    }

    void CreateButton(string buttonText, ItemDelegate method)
    {
        if (GUILayout.Button(buttonText))
        {
            method();
        }
    }

    void CreateButton(string buttonText, ItemDelegate method, ItemDelegate methodTwo)
    {
        if (GUILayout.Button(buttonText))
        {
            method();
            methodTwo();
        }
    }

    void InsertSpace(int numOfSpaces)
    {
        for (int i = 0; i < numOfSpaces; i++)
        {
            EditorGUILayout.Space();
        }
    }

    void DrawInventorySizeSelection(Rect lastPos,float squareWidth, float squareHeight, int squaresWide, int squaresHigh, float borderWidth, Color background, Color selectedSquare, Color unselectSquare)
    {
        float bgX = lastPos.position.x;
        float bgY = lastPos.position.y + 50;
        // Background
        EditorGUI.DrawRect(
            new Rect(bgX - borderWidth, 
            bgY - borderWidth, 
            squaresWide * squareWidth + (borderWidth * ((squaresWide * borderWidth) + borderWidth)), 
            squaresHigh * squareHeight + (borderWidth * ((squaresHigh * borderWidth) + borderWidth))), 
            background);

        int itemWidthCount = 0;
        for (int i = 0; i < squaresWide; i++)
        {
            int itemHeightCount = 0;
            itemWidthCount++;

            for (int j = 0; j < squaresHigh; j++)
            {
                itemHeightCount++;

                if (itemHeightCount <= itemScript.inventorySpaceY)
                {
                    if (itemWidthCount <= itemScript.inventorySpaceX)
                    {
                        // Each slot unit
                        Rect tempRect = new Rect(bgX + (i * squareWidth) + (borderWidth * i),
                                bgY + (j * squareWidth) + (borderWidth * j),
                                squareWidth,
                                squareHeight);

                        EditorGUI.DrawRect(tempRect, selectedSquare);

                        ItemSizeSlot slot = new ItemSizeSlot(tempRect, i, j, true);
                        slots.Add(slot);
                    }
                    else
                    {
                        // Each slot unit
                        Rect tempRect = new Rect(bgX + (i * squareWidth) + (borderWidth * i),
                                bgY + (j * squareWidth) + (borderWidth * j),
                                squareWidth,
                                squareHeight);

                        EditorGUI.DrawRect(tempRect, unselectSquare);

                        ItemSizeSlot slot = new ItemSizeSlot(tempRect, i, j, false);
                        slots.Add(slot);
                    }
                }
                else
                {
                    // Each slot unit
                    Rect tempRect = new Rect(bgX + (i * squareWidth) + (borderWidth * i),
                            bgY + (j * squareWidth) + (borderWidth * j),
                            squareWidth,
                            squareHeight);

                    EditorGUI.DrawRect(tempRect, unselectSquare);

                    ItemSizeSlot slot = new ItemSizeSlot(tempRect, i, j, true);
                    slots.Add(slot);
                }
                
            }
        }
    }

    void EvaluateManualItemSize()
    {
        Event click = Event.current;

        switch (click.type)
        {
            case EventType.MouseDown:
                if (Event.current.button == 0)
                {
                    foreach (ItemSizeSlot slot in slots)
                    {
                        if (Event.current.mousePosition.x > slot.rect.x && Event.current.mousePosition.x < (slot.rect.x + slot.rect.width))
                        {
                            if (Event.current.mousePosition.y > slot.rect.y && Event.current.mousePosition.y < (slot.rect.y + slot.rect.height))
                            {
                                itemScript.inventorySpaceX = slot.xId + 1;
                                itemScript.inventorySpaceY = slot.yId + 1;
                            }
                        }
                    }
                }
                break;

            default:
                break;
        }
    }
}
