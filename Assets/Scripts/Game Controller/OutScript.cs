using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutScript : MonoBehaviour
{
    public GameController gameController;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            gameController.TeamScored(gameController.LastTouchedPoint(), "floor");
        }
    }
}
