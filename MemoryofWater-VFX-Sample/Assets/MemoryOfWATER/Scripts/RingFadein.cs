using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingFadein : MonoBehaviour
{
    public float opacity;
    public float speed;
    public Material ring;
    private bool isfadein, isfadeout;
    // Start is called before the first frame update
    void Start()
    {
        opacity = 1;
        isfadein = false;
        isfadeout = false;
        ring.SetVector("_opacity", new Vector2(opacity, opacity));
    }


    // Update is called once per frame
    void Update()
    {
        if(isfadein)
        {
            if (opacity >= 0)
            opacity -= speed * Time.deltaTime;
        else
            opacity = 0;
        }

        if (isfadein)
        {
            if (opacity <= 1)
                opacity += speed * Time.deltaTime;
            else
                opacity = 1;
        }



        ring.SetVector("_opacity", new Vector2(opacity, opacity));

    }
    void fadein()
    {
        isfadein = true;
        isfadeout = false;
    }
    void fadeout()
    {
        isfadein = false;
        isfadeout = true;
    }
}

