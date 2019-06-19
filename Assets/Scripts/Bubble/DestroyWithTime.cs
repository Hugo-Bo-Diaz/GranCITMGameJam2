using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithTime : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Miliseconds")]
    public float time_to_kill = 100;

    private float time_spawned = 0;

    // Update is called once per frame
    void Update()
    {
        time_spawned += Time.deltaTime;
        if (time_spawned > time_to_kill) Destroy(this.gameObject);
    }
}
