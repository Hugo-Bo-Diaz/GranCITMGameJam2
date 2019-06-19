using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutScript : MonoBehaviour
{
    public GameController gameController;
    public GameObject prefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            gameController.TeamScored(gameController.LastTouchedPoint(), "out");
            Instantiate(prefab, collision.transform.position, collision.transform.rotation);
            collision.transform.position = new Vector3(100000, 100000, collision.transform.position.z);
        }
    }
}
