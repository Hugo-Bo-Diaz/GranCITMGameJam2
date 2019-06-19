﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Team
{
    public Team(GameObject _player1, GameObject _player2, bool _isTeam1, Text _gui_score) : this()
    {
        player1 = _player1;
        player2 = _player2;
        score = 0;
        touches = 0;
        isTeam1 = _isTeam1;
        gui_score = _gui_score;
    }

    public Text gui_score;
    public bool isTeam1;
    public int score;
    public int touches;

    public GameObject player1;
    public GameObject player2;

    public void UpdateScore()
    {
        if (isTeam1)
            gui_score.text = "Team Alga  " +  score.ToString();
        else
            gui_score.text = score.ToString() + "  Team Coral" ;
    }
}

public class GameController : MonoBehaviour
{
   Team team_alga;
   Team team_coral;

   public Text score_announcer;
   public Animator score_animator;
   public GameObject player1;
   public GameObject player2;
   public GameObject player3;
   public GameObject player4;

    public GameObject prefab;
    public Text score_alga;
    public Text score_coral;

    public GameObject ball;
    public GameObject coral_ball_spawn;
    public GameObject alga_ball_spawn;

    // Start is called before the first frame update
    void Start()
    {
        score_announcer.enabled = false;
        team_alga =  new Team(player1, player2, true, score_alga);
        team_alga.UpdateScore();
        team_coral = new Team(player1, player2, false, score_coral);
        team_coral.UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        // Alga touched too many times
        if (team_alga.touches >= 4)
        {
            TeamScored("coral", "touches");
            Instantiate(prefab, ball.transform.position, ball.transform.rotation);
            ball.GetComponent<SpriteRenderer>().enabled = false;
        }

        // Coral touched too many times
        if (team_coral.touches >= 4)
        {
            TeamScored("alga", "touches");
            Instantiate(prefab, ball.transform.position, ball.transform.rotation);
            ball.GetComponent<SpriteRenderer>().enabled = false;
        }

    }

    public void AddTouchTo(string team)
    {
        if (team == "alga")
        {
            team_alga.touches += 1;
            team_coral.touches = 0;
        }
        else
        {
            team_coral.touches += 1;
            team_alga.touches = 0;
        }
    }

    IEnumerator ResetGame(string team, string point_type)
    {
        score_announcer.enabled = true;
        score_animator.SetTrigger("start");
        // Display UI
        if(point_type == "floor")
        {
            score_announcer.text = "Don't drop the ball !!!";
        }

        if (point_type == "out")
        {
            score_announcer.text = "Don't let the ball go outside the field!!!";
        }

        if(point_type == "touches")
        {
            score_announcer.text = "Don't hit the ball more than three times !!!";
        }

        yield return new WaitForSeconds(2.0f);
        score_animator.SetTrigger("next");

        if (team == "alga")
        {
            score_announcer.text = "Team Alga Scores !!!";
        }
        else
            score_announcer.text = "Team Coral Scores !!!";

        yield return new WaitForSeconds(2.0f);
        if (team == "alga") ball.transform.SetPositionAndRotation(alga_ball_spawn.transform.position, Quaternion.identity);
        else
        {
            ball.transform.SetPositionAndRotation(coral_ball_spawn.transform.position, Quaternion.identity);

        }
        ball.GetComponent<SpriteRenderer>().enabled = true;
        score_announcer.enabled = false;
        score_animator.SetTrigger("end");
    }
    public void TeamScored(string team, string type)
    {
        if (team == "alga")
        {
            team_alga.score += 1;
            team_alga.UpdateScore();

        }
        else
        {
            team_coral.score += 1;
            team_coral.UpdateScore();
        }

        team_alga.touches = 0;
        team_coral.touches = 0;


        // Reset ball
        StartCoroutine(ResetGame(team, type));
    }

    public string LastTouchedPoint()
    {
        if (team_alga.touches != 0) return "coral";
        else return "alga";
    }
}
