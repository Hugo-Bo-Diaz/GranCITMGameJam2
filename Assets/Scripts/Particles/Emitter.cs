using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    public GameObject to_emmit;
    // Start is called before the first frame update
    void Start()
    {
        float initial_spawn_angle = 0;
        while (initial_spawn_angle < 360)
        {
            Instantiate(to_emmit, transform.position, Quaternion.Euler(0,0,initial_spawn_angle));
            initial_spawn_angle += 36;
        }
    }
}
