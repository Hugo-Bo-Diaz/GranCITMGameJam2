using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuplay : MonoBehaviour
{
    // Start is called before the first frame update

    public void changemenuscene(string menuplay)
    {
            Application.LoadLevel (menuplay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
