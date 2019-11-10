using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    
    public struct Condition
    {

        public float duration; //The duration of the condition
        public ConditionType conditionType; //Name of condition

    }

    public enum ConditionType
    {
        DELAYEDBLAST,
    }

    public struct RewindPoint
    {
        public int currentHealthRewind;
        public bool isDeadRewind;
        public Transform locationRewind;
        public List<Condition> currentConditionsRewind;

    }


    [Header("Health and Death")]

    public int startingHealth = 100;
    public int currentHealth;
    public bool isDead;

    [Header("Level")]

    public int level;
    public int experience;
    [SerializeField] int xpToNextLevel;
    int[] levelBrackets;

    [Header("Stats")]

    public int strength;
    public int agility;
    public int constitution;
    public int intellect;
    public int physicalArmour;
    public int magicalArmour;

    [Header("Derived Stats")]
    
    public int maxHP;
    public int currentHP;
    public int movementSpeed;
    public int dodgeChance;
    public int physDamagePotential;
    public int magDamagePotential;
    public int experienceRequiredToNextLevel;
    public int physDamageReduction;
    public int magDamageReduction;

    [Header("Conditions and Immunities")]

    public List<Condition> currentConditions;
    public bool cannotBeTeleported;

    [Header("Rewind Point")]


    //public RewindPoint rewindPoint;
    public List<RewindPoint> rewindPoints;

    // Start is called before the first frame update
    void Start()
    {



        //How you give a condition
        //Condition delayedBlastTest = new Condition();
        //delayedBlastTest.duration = 5.0f;
        //delayedBlastTest.conditionType = ConditionType.DELAYEDBLAST;

        //currentConditions.Add(delayedBlastTest);
    }

    // Update is called once per frame
    void Update()
    {
        //mAKE SURE to update all conditions for any other object that inhereits from entity
        UpdateAllConditions();

    }

     void GiveExperience(int value)
    {
        experience += value;
        if (experience >= levelBrackets[level])
        {
            //You leveled up!
            level++;

            CalculateMaxHP();
            CalculateMovementSpeed();
            CalculateDodgeChance();
            CalculateMagDamagePotential();
            CalculatePhysDamagePotential();

        }
    }

    //Function that is called when the player deals damage to you
    //Default condition format
    public virtual void TakeDamage(int amount)
    {
        Debug.Log("OOF x " + amount);
        if (isDead)
            return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
        }
    }


    public virtual void Death()
    {
        isDead = true;

    }

    public void UpdateAllConditions()
    {
        if (currentConditions.Count != 0)
        {
            foreach (Condition condition in currentConditions)
            {
                if (condition.conditionType == ConditionType.DELAYEDBLAST)
                {
                    //Do the thing!
                    //Wait for player input of kaboom
                    //If have condition, detonate and remove condition


                }

                //At moment of adding condition
                //timeLeft = condition.duration;

                //timeLeft -= Time.DeltaTime;
                //if (timeLeft >= 0)
                //{
                //remove condition
                // }
            }
        }


    }

    void CalculateMaxHP()
    {
        maxHP = (6 + constitution) * level;
    }

    void CalculateMovementSpeed()
    {
        movementSpeed = agility;
    }

    void CalculateDodgeChance()
    {
        dodgeChance = 0; //No default, this is for later balance purpose potential
    }

    void CalculatePhysDamagePotential()
    {
        physDamagePotential = Mathf.Abs(strength * (strength / 2)) * level;
    }

    void CalculateMagDamagePotential()
    {
        magDamagePotential = Mathf.Abs(intellect * (intellect / 2)) * level;
    }

    public void InitialiseAll()
    {
        currentConditions = new List<Condition>();
        
        rewindPoints = new List<RewindPoint>();
        Debug.Log("New return point made");

        //Make level brackets accurate
        levelBrackets = new int[10];
        levelBrackets[0] = 0;
        levelBrackets[1] = 100;
        levelBrackets[2] = 250;
        levelBrackets[3] = 600;
        levelBrackets[4] = 1350;
        levelBrackets[5] = 2900;
        levelBrackets[6] = 6050;
        levelBrackets[7] = 16000;
        levelBrackets[8] = 32350;
        levelBrackets[9] = 65100;

        CalculateAllDerivedStats();


    }

    public void CalculateAllDerivedStats()
    {
        CalculateMaxHP();
        CalculateMovementSpeed();
        CalculateDodgeChance();
        CalculateMagDamagePotential();
        CalculatePhysDamagePotential();
    }

    public void RecordRewind()
    {
        RewindPoint temp;

        temp.currentHealthRewind = currentHealth;
        temp.isDeadRewind = isDead;
        temp.locationRewind = transform;
        temp.currentConditionsRewind = currentConditions;

        // add check to delete list contents if no longer needed
        //if()
        // rewindPoints.RemoveAt(rewindPoints.Count - 1);

        rewindPoints.Insert(0, temp);
    }

}
