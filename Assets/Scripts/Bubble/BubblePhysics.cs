using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePhysics : MonoBehaviour
{

    public float max_speed = 10.0f;
    public float min_speed = 0.1f;
    public float water_resistance = 0.1f;
    public float gravity_magnitude = 0.0f;

    float current_speed = 0.5f;
    
    Vector2 direction = new Vector2(0f, 0f);

    bool paused = false;

    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused) { 
            Vector2 new_direction = direction.normalized * current_speed + new Vector2(0f, 1f) * gravity_magnitude * Time.deltaTime * 1000 / 16;
            current_speed -= water_resistance * Time.deltaTime * 1000 / 16;

            current_speed = new_direction.magnitude;
            if (new_direction.y > 0)  current_speed = Mathf.Clamp(current_speed, min_speed, min_speed);
            else current_speed = Mathf.Clamp(current_speed, 0, max_speed);


            direction = new_direction;
            Vector3 new_position = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z);
            rb.MovePosition(new_position);
        }

    }

    public void ApplyHit(Vector2 new_direction, float speed)
    {
        direction = new_direction.normalized;
        current_speed = speed;
    }

    public void Pause(bool pause)
    {
        paused = pause;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("bum");
        if (collision.gameObject.tag == "Player")
        {
            
        }
    }
}
