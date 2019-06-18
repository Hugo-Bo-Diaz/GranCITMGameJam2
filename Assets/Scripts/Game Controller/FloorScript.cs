using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    public string team = "";
    public GameController gameController;


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Ball")
        {
            gameController.TeamScored(team, "floor");
        }
    }
}
