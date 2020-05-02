using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class callmaterial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pod;
    public bool start;
    void Start()
    {
        if (start)
            pod.SendMessage("fadein");
        else
            pod.SendMessage("fadeout");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
