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
    Vector2 test_hit = new Vector2(-0.5f, -1f);


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 new_direction = direction.normalized * current_speed + new Vector2(0f, 1f) * gravity_magnitude * Time.deltaTime * 1000 / 16;
        current_speed -= water_resistance * Time.deltaTime * 1000 / 16;
        current_speed = Mathf.Clamp(current_speed, min_speed, max_speed);
        float magnitude = Mathf.Clamp(new_direction.magnitude, min_speed, max_speed);


        direction = new_direction.normalized * magnitude * Time.deltaTime * 1000 / 16;
        Vector3 new_position = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z);
        transform.SetPositionAndRotation(new_position, transform.rotation);

        if (Input.GetKeyDown("space"))
        {
            direction = test_hit;
            current_speed = max_speed;
        }
    }

    public void ApplyHit(Vector2 new_direction, float speed)
    {
        direction = new_direction.normalized;
        current_speed = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("bum");
        if (collision.gameObject.tag == "Player")
        {
            
        }
    }
}
