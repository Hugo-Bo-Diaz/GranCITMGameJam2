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

    void UpdateScore()
    {
        if (isTeam1)
            gui_score.text = "Team 1" +score.ToString();
        else
            gui_score.text = score.ToString() + "Team 2" ;
    }
}

public class GameController : MonoBehaviour
{
   Team team1;
   Team team2;

   public GameObject player1;
   public GameObject player2;
   public GameObject player3;
   public GameObject player4;

    public Text score1;
    public Text score2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TeamScored(bool Team1Scored)
    {
        gameObject.BroadcastMessage("teamScored", Team1Scored);
    }
}
