using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Player vars")]
    public float max_speed = 0;
    public float time_to_accel = 0;

    public float rotation_speed = 0;

    private Vector2 direction = new Vector2(0,0);
    private float current_speed = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get wanted direction
        Vector2 wanted_direction = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        float wanted_speed = 0;

        if (wanted_direction.magnitude != 0) wanted_speed = max_speed;

        // Accelerate speed 
        float acceleration = (wanted_speed - current_speed);

        
    }
}
