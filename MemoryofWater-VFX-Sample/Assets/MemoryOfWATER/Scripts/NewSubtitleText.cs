using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class NewSubtitleText : MonoBehaviour
{
    public string[] subtitle;
    public GameObject textmeshproText;
    public float interval;
    public int CurrentIndex;
    private IEnumerator coroutine;
    public bool started,laststate;
    public float time;
    public GameObject Textfade1, Textfade2;
    // Start is called before the first frame update
    void Start()
    {
        started = false;
        laststate = false;
        textmeshproText.GetComponent<TextMeshPro>().SetText(subtitle[0]);

    }

    // Update is called once per frame
    void Update()
    {
        if (started == true && laststate == false)
        {
            laststate = true;
            time = 0;
        }
        if (started == true && laststate == true)
        {
            time += Time.unscaledDeltaTime;
            if (CurrentIndex != (int)(time / interval)) {
                print("call");
                coroutine = SubtitleSwitch(1.0f);
                StartCoroutine(coroutine);
                CurrentIndex  = (int)(time / interval);
            }
        }
        if (started == false)
            laststate = false;
        
    }
    private IEnumerator SubtitleSwitch(float waitTime)
    {
        print("fade");
        //Textfade1.GetComponent<PlayableDirector>().SetGenericBinding()
        //Textfade1.GetComponent<PlayableDirector>().Play();
        textmeshproText.GetComponent<TextMeshPro>().alpha = 0f;
        yield return new WaitForSeconds(waitTime);
        textmeshproText.GetComponent<TextMeshPro>().SetText(subtitle[CurrentIndex]);
        //Textfade2.GetComponent<PlayableDirector>().Play();
        textmeshproText.GetComponent<TextMeshPro>().alpha = 1.0f;
    }
    private void Textfade()
    {
        while (textmeshproText.GetComponent<TextMeshPro>().alpha < 1.0f)
        {
            textmeshproText.GetComponent<TextMeshPro>().alpha += Time.deltaTime * 0.1f;
            print(textmeshproText.GetComponent<TextMeshPro>().alpha);
            if (textmeshproText.GetComponent<TextMeshPro>().alpha >= 1.0f)
                break;
        }
    }

}
