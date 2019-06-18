using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    GUIText score;
    Team team;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (team.isTeam1)
            score.text = "Team 1 Score: " + team.score.ToString();
        else
            score.text = "Team 2 Score: " + team.score.ToString();
    }

    
}
