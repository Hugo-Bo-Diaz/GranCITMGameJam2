using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Player vars")]
    public float max_speed = 0;
    public float normal_max_speed = 10;
    public float turbo_max_speed = 20;
    public float time_to_accel = 0;
    public float deceleration_multiplicator = 0;
    public float min_angle = 10;
    public float rotation_speed = 0;
    

    [Header("Input")]
    public string horizontal_input = "Horizontal 1";
    public string vertical_input = "Vertical 1";
    public string dash_input = "Dash 1";


    private Vector2 wanted_direction;
    private Vector2 direction = new Vector2(1, 0);
    private Vector2 last_direction = new Vector2(0, 0);
    private float current_speed = 0;

    private bool turboing = false;

    Rigidbody2D rb;
    private SpriteRenderer Renderer2D;
    private Attack attack_script;




    void Start()
    {
        direction.x = 1;
        direction.Normalize();

        rotation_speed *= Mathf.Deg2Rad;

        rb = GetComponent<Rigidbody2D>();
        Renderer2D = GetComponent<SpriteRenderer>();
        attack_script = GetComponent<Attack>();

    }

    IEnumerator Turbo()
    {
        max_speed = turbo_max_speed;
        yield return new WaitForSeconds(1.0f); // Turbo duration
        max_speed = normal_max_speed;
        yield return new WaitForSeconds(2.0f); // Turbo cooldown
        turboing = false;

    }

    // Update is called once per frame
    void Update()
    {
        // Get wanted direction: Used only to know direction rotation and neutral/not neutral
        wanted_direction = new Vector2(Input.GetAxis(horizontal_input), Input.GetAxis(vertical_input)); 
        wanted_direction.Normalize();

        if(!turboing && Input.GetButton(dash_input))
        {
            turboing = true;
            StartCoroutine(Turbo());
        }
        

        // If the stick is neutral, go towards last direction and set wanted speed to 0
        float wanted_speed = max_speed;
        if (wanted_direction.magnitude < 0.001) {
            wanted_direction = last_direction;
            wanted_speed = 0;
        }

        if (attack_script.IsAttacking()) wanted_speed = 0;
        // Find acceleration
        // Normal acceleration
        float acceleration = (wanted_speed - current_speed) / time_to_accel;
        // Faster deceleration
        if (acceleration < 0) acceleration *= deceleration_multiplicator;


        // Find rotation direction
        float frame_rotation = rotation_speed * Time.deltaTime;
        float angle_diff = Vector2.SignedAngle(direction, wanted_direction);
        // Cap 
        if (wanted_speed == 0 || Mathf.Abs(angle_diff) < min_angle) frame_rotation = 0;
        else if(angle_diff < 0) frame_rotation = -frame_rotation;


        // Apply acceleration
        current_speed += acceleration * Time.deltaTime;
        // Cap
        if (current_speed > max_speed) current_speed = max_speed;
        if (current_speed < 0) current_speed = 0;

        // Apply rotation

        float current_angle = Mathf.Atan2(direction.y, direction.x);
        if (frame_rotation != 0){
            current_angle += frame_rotation;
            direction.x = Mathf.Cos(current_angle);
            direction.y = Mathf.Sin(current_angle);
        }

        //if(angle_diff > 30) current_speed = 0;

        current_angle *= Mathf.Rad2Deg;

        // Calculate this frame direction (should use "direction" var)
        direction.Normalize();
        last_direction = direction;
        Vector2 movement = direction * current_speed * Time.deltaTime * 1000 / 16;
     
        // Apply speed and set rotation
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, current_angle));
        Vector3 position = transform.position;



        //RaycastHit2D[] rayData = new RaycastHit2D[0];
        //capsule.Cast(new Vector2(movement.x, 0.0f), rayData);
        ////Debug.Log(rayData.Length);
        //if(rayData.Length != 0)
        //{
        //    movement.x = 0;
        //}


        //capsule.Cast(new Vector2(0.0f, movement.y), rayData);
        //if (rayData.Length != 0)
        //{
        //    movement.y = 0;
        //}

        //rb.velocity = movement;
        position.x += movement.x;
        position.y += movement.y;

        //// Move 
        rb.MovePosition(position);
        rb.MoveRotation(current_angle);
        //transform.SetPositionAndRotation(position, rotation);

        Renderer2D.flipY = direction.x < 0;

    }

    public Vector2 GetDirection() { return direction; }
    public Vector2 GetWantedDirection() { return wanted_direction; }
}
