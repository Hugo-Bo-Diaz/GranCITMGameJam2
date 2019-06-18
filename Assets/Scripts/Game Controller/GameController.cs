using System.Collections;
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

   public GameObject player1;
   public GameObject player2;
   public GameObject player3;
   public GameObject player4;

    public Text score_alga;
    public Text score_coral;

    // Start is called before the first frame update
    void Start()
    {
        team_alga =  new Team(player1, player2, true, score_alga);
        team_alga.UpdateScore();
        team_coral = new Team(player1, player2, false, score_coral);
        team_coral.UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TeamScored(bool Team1Scored)
    {
        if (Team1Scored)
        {
            team_alga.score += 1;
        }
        if (!Team1Scored)
        {
            team_coral.score += 1;
        }
    }
}
