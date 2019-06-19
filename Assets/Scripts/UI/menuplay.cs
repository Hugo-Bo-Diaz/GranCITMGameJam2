using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuplay : MonoBehaviour
{
    // Start is called before the first frame update

	public bool controlsareopen = false;
	public GameObject controls;

    public void changemenuscene(string menuplay)
    {
            SceneManager.LoadScene(menuplay);
    }

	public void opencontrolsmenu()
	{
		controls.SetActive(true);
		controlsareopen=true;
	}

	public void closecontrolsmenu()
	{
		controls.SetActive(false);
		controlsareopen=false;
	}

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

	public void CloseGame()
	{
		Application.Quit();
	}

    // Update is called once per frame
    void Update()
    {
		if (controlsareopen == true && Input.anyKey)
		{
			closecontrolsmenu();
		}
    }
}
