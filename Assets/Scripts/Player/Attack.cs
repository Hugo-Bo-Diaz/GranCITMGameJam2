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
    public float hitstop_amount = 0.2f;

    [Header("Input")]
    public string attack_str = "Melee Attack 1";

    private bool attacking = false;
    private bool attack_first_frame = true;
    private bool collider_active = false;

    private float attack_start_time = 0;

    private Movement movement;
    private Animator animator;
    private GameController gc;

    [Header("Team")]
    public string team;

    AudioSource audio;

    public AudioClip hitSound;
    public AudioClip fishWhiff;
    void Start()
    {
        audio = GetComponent<AudioSource>();
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
                audio.PlayOneShot(fishWhiff);
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

    IEnumerator HitstopApplyHit(BubblePhysics physics)
    {
        yield return new WaitForSeconds(hitstop_amount);
        physics.enabled = true;
        movement.enabled = true;
        animator.enabled = true;
        physics.ApplyHit(movement.GetWantedDirection(), attack_force);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("bum");
        if (collision.gameObject.tag == "Ball")
        {
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(hitSound);
            Vector3 coll_centre = collision.bounds.center;
            Vector3 pos = melee_attack.bounds.center;
            Vector3 diff = coll_centre - pos;

            Vector3 particle_spawn = pos += diff;

            // Looks bad?
            Instantiate(particle_spawner, particle_spawn, transform.rotation);
            melee_attack.enabled = false;


            BubblePhysics physics = collision.gameObject.GetComponent<BubblePhysics>();
            physics.enabled = false;
            movement.enabled = false;
            animator.enabled = false;
            physics.WiggleFor(hitstop_amount);
            StartCoroutine(HitstopApplyHit(physics));
            gc.AddTouchTo(team);
        }
    }

    public bool IsAttacking() { return attacking; }

}
