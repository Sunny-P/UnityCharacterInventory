using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanelStatControl : MonoBehaviour
{
    [Header("Editted Text Fields")]
    public Text strengthText;
    public Text agilityText;
    public Text constitutionText;
    public Text intellectText;
    public Text healthText;
    public Text physicalDamageReductionText;
    public Text magicalDamageReductionText;
    public Text physicalArmourText;
    public Text magicalArmourText;

    [Header("Literal Names of Entity Variables")]
    public string strength;
    public string agility;
    public string constitution;
    public string intellect;
    public string currentHealth;
    public string maxHealth;
    public string physicalDamageReduction;
    public string magicalDamageReduction;
    public string physicalArmour;
    public string magicalArmour;

    [Header("Entity to Mirror Stat Values From")]
    public Entity characterEntity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (characterEntity != null)
        {
            // Set Strength text field with strength
            UpdateTextFieldWithValue(strengthText, "Str: ", strength);

            // Set Agility text field with agility
            UpdateTextFieldWithValue(agilityText, "Agi: ", agility);

            // Set Constitution text field with constitution
            UpdateTextFieldWithValue(constitutionText, "Con: ", constitution);

            // Set Intellect text field with intellect
            UpdateTextFieldWithValue(intellectText, "Int: ", intellect);

            // Set Health text field with current & max health
            int realCurrentHealth = (int)characterEntity.GetType().GetField(currentHealth).GetValue(characterEntity);
            int realMaxHealth = (int)characterEntity.GetType().GetField(maxHealth).GetValue(characterEntity);
            healthText.text = realCurrentHealth + "/" + realMaxHealth;

            // Set Physical Damage Reduction text field with physicalDmgReduction
            UpdateTextFieldWithValue(physicalDamageReductionText, "Physical: ", physicalDamageReduction);

            // Set Magical Damage Reduction text field with magicalDmgReduction
            UpdateTextFieldWithValue(magicalDamageReductionText, "Magical: ", magicalDamageReduction);

            // Set Physical Armour text field with physicalArmour
            UpdateTextFieldWithValue(physicalArmourText, "Physical: ", physicalArmour);

            // Set Magical Armour text field with magicalArmour
            UpdateTextFieldWithValue(magicalArmourText, "Magical: ", magicalArmour);
        }
    }

    void UpdateTextFieldWithValue(Text updatedField, string baseText, string statLiteralName)
    {
        int realValue = (int)characterEntity.GetType().GetField(statLiteralName).GetValue(characterEntity);
        updatedField.text = baseText + realValue;
    }
}
