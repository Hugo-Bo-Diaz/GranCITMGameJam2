using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePhysics : MonoBehaviour
{

    public float max_speed = 10.0f;
    public float min_speed = 0.1f;
    public float water_resistance = 0.1f;
    public float gravity_magnitude = 0.0f;

    public float time_ungravity = 1;
    public float time_ungravity_start = -10;

    public GameObject bubble_stella;


    float current_speed = 0.5f;
    Vector2 direction = new Vector2(0f, 0f);
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 new_direction = direction.normalized * current_speed + new Vector2(0f, 1f) * gravity_magnitude * Time.deltaTime * 1000 / 16;
        current_speed -= water_resistance * Time.deltaTime * 1000 / 16;

        current_speed = new_direction.magnitude;

        float frame_max_speed = min_speed;

        if (Time.time - time_ungravity_start < time_ungravity)
        {
            frame_max_speed = max_speed;
            Instantiate(bubble_stella, transform.position + new Vector3(Random.Range(-100,100), Random.Range(-100, 100)), transform.rotation);

        }

        if (new_direction.y > 0)  current_speed = Mathf.Clamp(current_speed, min_speed, frame_max_speed);
        else current_speed = Mathf.Clamp(current_speed, 0, max_speed);


        direction = new_direction;
        Vector3 new_position = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z);
        rb.MovePosition(new_position);

    }

    public void ApplyHit(Vector2 new_direction, float speed)
    {
        direction = new_direction.normalized;
        current_speed = speed;

        time_ungravity_start = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("bum");
        if (collision.gameObject.tag == "Player")
        {
            
        }
    }

    IEnumerator WiggleCorutine(float time)
    {
        float time_wiggling = 0;
        Vector3 initial_position = transform.position;

        while(time_wiggling < time)
        {
            time_wiggling += Time.deltaTime;
            transform.position = initial_position += new Vector3(Random.Range(-10, 10), Random.Range(-10, 10));
            yield return null;
        }
    }
    public void WiggleFor(float time)
    {
        StartCoroutine(WiggleCorutine(time));
    }
}
