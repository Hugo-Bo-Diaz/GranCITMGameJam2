using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    public bool isTeamAlga = false;
    public GameController gameController;


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Ball")
        {
            gameController.TeamScored(!isTeamAlga, "floor");
        }
    }
}
