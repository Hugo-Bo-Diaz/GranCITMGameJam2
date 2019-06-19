using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForward2D : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 direction;
    void Start()
    {
        direction.x = Mathf.Cos(transform.rotation.eulerAngles.z);
        direction.y = Mathf.Sin(transform.rotation.eulerAngles.z);

        direction *= 30;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 new_pos = new Vector3(transform.position.x, transform.position.y);
        new_pos += direction;
        transform.position = new Vector3(new_pos.x, new_pos.y);
    }
}
