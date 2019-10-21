using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Entity
{
    public enum PlayerState
    {
        FREE,
        DOINGSKILL
    }

    [Header("State")]
    public PlayerState playerState;

    [Header("Navigation")]
    public float turningSpeed;
    NavMeshAgent navAgent = null;

    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        // Using base given stats, get derived stats
        InitialiseAll();
        currentHP = maxHP;
        
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = movementSpeed;
        playerState = PlayerState.FREE;
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAllConditions();
        CalculateAllDerivedStats();
        //if () // Check if player is dead
        if (!isDead)
        {
            switch (playerState)
            {
                case PlayerState.FREE:  // Player can move, and if in combat can receive input for selecting a skill
                    Move();

                    break;

                case PlayerState.DOINGSKILL: // Player has selected a skill. Choose where to cast
                                                // Make the player stop moving
                    
                    navAgent.speed = 0.0f;
                    navAgent.angularSpeed = 0.0f;
                    //TargetSkill();


                    break;

                default:
                    break;
            }
        }
    }



    void Move()
    {
        if (Input.GetMouseButton(0))
        {
            navAgent.speed = movementSpeed;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 200.0f))
            {
                //if (hit.collider.tag.Contains("Finish"))
                //{
                    navAgent.SetDestination(hit.point);
                //}
            }

        }
    }
}
