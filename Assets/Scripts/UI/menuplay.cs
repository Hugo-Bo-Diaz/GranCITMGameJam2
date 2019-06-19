using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuplay : MonoBehaviour
{
    // Start is called before the first frame update

	public bool controlsareopen = false;
	public GameObject controls;

    public void changemenuscene(string menuplay)
    {
            Application.LoadLevel (menuplay);
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
