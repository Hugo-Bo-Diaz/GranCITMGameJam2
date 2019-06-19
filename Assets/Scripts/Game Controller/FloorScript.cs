using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    public string team = "";
    public GameController gameController;
    public GameObject prefab;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Ball")
        {
            gameController.TeamScored(team, "floor");

            Instantiate(prefab, other.transform.position,other.transform.rotation);
            other.transform.position = new Vector3(100000, 100000, other.transform.position.z);
        }
        

        
    }
}
