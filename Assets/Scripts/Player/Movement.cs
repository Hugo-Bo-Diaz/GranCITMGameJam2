using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Player vars")]
    public float max_speed = 0;
    public float time_to_accel = 0;
    public float deceleration_multiplicator = 0;
    public float min_angle = 10;

    public float rotation_speed = 0;

    private Vector2 direction = new Vector2(1, 0);
    private Vector2 last_direction = new Vector2(0, 0);
    private float current_speed = 0;


    void Start()
    {
        direction.x = 1;
        direction.Normalize();

        rotation_speed *= Mathf.Deg2Rad;
    }

    // Update is called once per frame
    void Update()
    {
        // Get wanted direction: Used only to know direction rotation and neutral/not neutral
        Vector2 wanted_direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); 
        wanted_direction.Normalize();

        // If the stick is neutral, go towards last direction and set wanted speed to 0
        float wanted_speed = max_speed;
        if (wanted_direction.magnitude < 0.001) {
            wanted_direction = last_direction;
            wanted_speed = 0;
        }
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
        Vector2 movement = direction * current_speed;
     
        // Apply speed and set rotation
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, current_angle));
        Vector3 position = transform.position;

        position.x += movement.x;
        position.y += movement.y;

        // Move 
        transform.SetPositionAndRotation(position, rotation);
    }

    public Vector2 GetDirection() { return direction; }
}
