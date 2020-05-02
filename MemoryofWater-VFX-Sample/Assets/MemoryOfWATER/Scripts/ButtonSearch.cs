using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;

public class ButtonSearch : MonoBehaviour
{
    [Header("Reference")]
    public VisualEffect SearchVFX;
    private bool ShowResult;
    public TMP_InputField SearchContent;
    public TMP_Text[] ResultText;
    public PlayableDirector LoadingTimeLine;

    [Header("DATA")]
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        ShowResult = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonToggle()
    {
        if (SearchContent.text.Length >= 1)
        {
            //print(SearchContent.text);
            SearchVFX.SetBool("ShowResult",true);
            coroutine = WaitAndPrint(2.0f);
            StartCoroutine(coroutine);
        }
        else
        {
            SearchVFX.SetBool("ShowResult", false);
        }
    }

    // when resarch wait for a few seconds then show the result
    private IEnumerator WaitAndPrint(float waitTime)
    {
        // set the border invisible as well as the text
        SearchVFX.SetBool("ShowResult", false);
        SearchVFX.SetBool("SearchLoading", true);

        //play the loading timeline to init the rotating stuff
        //this doesn't need to be stopped
        LoadingTimeLine.Play();

        if (SearchContent.text.Length != 0)
        {
            foreach (TMP_Text k in ResultText)
            {
                k.GetComponent<MeshRenderer>().enabled = false;
                if (k.name.Contains("Amount"))
                {
                    k.SetText("1 Result" + " of '" + SearchContent.text+ "'");
                }
            }
        }
        yield return new WaitForSeconds(waitTime);
        // initial the text
        SearchVFX.SetBool("ShowResult", true);
        SearchVFX.SetBool("SearchLoading", false);
        if (ResultText.Length != 0)
        {
            foreach (TMP_Text k in ResultText)
            {
                k.GetComponent<MeshRenderer>().enabled = true;
            }
        }
        yield return new WaitForSeconds(waitTime);

        
    }
}
