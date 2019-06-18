using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Colliders")]
    public Collider2D melee_attack;

    [Header("Attack vars (ms)")]
    public float startup;


    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float attack_input = Input.GetAxis("Melee Attack");

        if (attack_input == 1) Debug.Log("Trigger pressed");
    }
}
