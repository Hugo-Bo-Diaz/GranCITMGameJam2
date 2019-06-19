using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Colliders")]
    public Collider2D melee_attack;

    [Header("Particles")]
    public GameObject particle_spawner;


    [Header("Attack vars (s)")]
    public float startup;
    public float attack_duration;

    public float attack_force = 20;

    [Header("Input")]
    public string attack_str = "Melee Attack 1";

    private bool attacking = false;
    private bool attack_first_frame = true;
    private bool collider_active = false;

    private float attack_start_time = 0;

    private Movement movement;
    private GameController gc;

    [Header("Team")]
    public string team;

    Animator animator;

    void Start()
    {
        movement = GetComponent<Movement>();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        float attack_input = Input.GetAxis(attack_str);

        if (!attacking) attacking = attack_input == 1;

        if (attacking)
        {
            if (attack_first_frame) {
                attack_start_time = Time.time;
                attack_first_frame = false;
                // Play animation
                animator.SetTrigger("Attacking");
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

            Vector3 coll_centre = collision.bounds.center;
            Vector3 pos = melee_attack.bounds.center;
            Vector3 diff = coll_centre - pos;

            Vector3 particle_spawn = pos += diff;

            Instantiate(particle_spawner, particle_spawn, transform.rotation);

            melee_attack.enabled = false;
            BubblePhysics physics = collision.gameObject.GetComponent<BubblePhysics>();
            physics.ApplyHit(movement.GetWantedDirection(), attack_force);
            gc.AddTouchTo(team);
        }
    }

    public bool IsAttacking() { return attacking; }

}
