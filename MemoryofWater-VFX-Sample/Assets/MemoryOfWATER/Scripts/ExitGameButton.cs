using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonClicked()
    {
        print("quit");
        Application.Quit();
    }
}
