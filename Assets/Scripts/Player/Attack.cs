﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Colliders")]
    public Collider2D melee_attack;

    [Header("Attack vars (s)")]
    public float startup;
    public float attack_duration;

    public float attack_force = 20;

    private bool attacking = false;
    private bool attack_first_frame = true;
    private bool collider_active = false;

    private float attack_start_time = 0;

    private Movement movement;


    
    void Start()
    {
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        float attack_input = Input.GetAxis("Melee Attack");

        if (!attacking) attacking = attack_input == 1;

        if (attacking)
        {
            if (attack_first_frame) {
                attack_start_time = Time.time;
                attack_first_frame = false;
                // Play animation
            }

            if (Time.time - attack_start_time > startup && !collider_active)
            {
                melee_attack.enabled = true;
                collider_active = true;
            }


            if(Time.time - attack_start_time > attack_duration)
            {
                melee_attack.enabled = false;
                attacking = false;
                attack_first_frame = true;
                collider_active = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("bum");
        if (collision.gameObject.tag == "Ball")
        {
            collision.gameObject.GetComponent<BubblePhysics>().ApplyHit(movement.GetDirection(), attack_force);
            melee_attack.enabled = false;


        }
    }

}