using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using TMPro;
using UnityEngine.Playables;


public class WaterMeasureControl : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Reference")]
    public VisualEffect mesureVFX;
    public VFXText MainTextController;
    public PlayableDirector Timeline;
    public TMP_Text[] time;
    public TMP_Text sign;

    [Header("DATA")]
    public float overallscore;
    public bool isStarted = true;
    public bool isfinished;
    public float speed = 1;
    public TMP_Text[] Elements;
    private string[] unit;
    private IEnumerator coroutine;
    public bool coroutinestarted;
    // set the timer in script so it won't increse when the calculation stop
    private float timer;


    void Start()
    {
        // initial the properties
        overallscore = 0;
        isfinished = false;
        coroutinestarted = false;
        unit = new string[Elements.Length];
        for (int i = 0; i < Elements.Length; i++)
        {
            unit[i] = Elements[i].text;
        }
        // set the initial time base on today
        time[0].SetText(System.DateTime.Now.ToLongTimeString());
        time[1].SetText(System.DateTime.Now.AddYears(200).ToShortDateString());
        time[2].SetText(System.DateTime.Now.DayOfWeek.ToString());
        //System.DayOfWeek
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        time[0].SetText(System.DateTime.Now.ToLongTimeString());
        time[1].SetText(System.DateTime.Now.AddYears(200).ToShortDateString());
        time[2].SetText(System.DateTime.Now.DayOfWeek.ToString());
        if (isStarted == true && isfinished == false)
        {
            overallscore += Time.deltaTime * speed;
            timer += Time.deltaTime;
            if (!coroutinestarted) {
                coroutinestarted = !coroutinestarted;
                coroutine = WaitAndPrint(1.0f);
                StartCoroutine(coroutine);
            }


            if (overallscore > 100)
            {
                overallscore = 100.0f;
                //isStarted = false;
                isfinished = true;

                mesureVFX.SetBool("Isstarted", false);
                mesureVFX.SetBool("CalculationFinished", true);
                
            }
            else {
                //overallscore = (float)System.Math.Round(overallscore, 1);
                mesureVFX.SetFloat("Score", (float)System.Math.Round(overallscore, 1));
                mesureVFX.SetFloat("Timer", timer);
                MainTextController.WordList[0] = ((float)System.Math.Round(overallscore, 1)).ToString() + "%";
                //MainTextController.WordList[0] =  "%";
            }

            //Element 0, COD ( 200 mg / l - 2.8 mg / l COD )
            //Element 1, FE (40 000 μg / l - 22.0 μg / l)
            //Element 2, Mn(8 000 μg / l Mn - 5.0 μg / l)
            //Element 3, Conductivity (4 000 μS / cm - 289 μS / cm)
            //Element 4, PH(2.0-7.1)
            //Element 5, Coliform Bakteria (30 000 CFU / l - 0 CFU / l )
            Elements[0].text = System.Math.Round((200 - (200 - 2.8) * overallscore / 100) * Random.Range(0.8f, 1.2f), 1).ToString() + " " + unit[0];
            mesureVFX.SetFloat("COD", 1 - (float)System.Math.Round(overallscore / 100, 1));

            Elements[1].text = System.Math.Round((40000 - (40000 - 22) * overallscore / 100) * Random.Range(0.8f, 1.2f), 1).ToString() + " " + unit[1];
            mesureVFX.SetFloat("Fe", 1 - (float)System.Math.Round(overallscore / 100, 1));

            Elements[2].text = System.Math.Round((8000 - (8000 - 5) * overallscore / 100) * Random.Range(0.8f, 1.2f), 1).ToString() + " " + unit[2];
            mesureVFX.SetFloat("Mn", 1 - (float)System.Math.Round(overallscore / 100, 1));

            Elements[3].text = System.Math.Round((4000 - (4000 - 289) * overallscore / 100) * Random.Range(0.8f, 1.2f), 1).ToString() + " " + unit[3];
            mesureVFX.SetFloat("Conductivity", (float)System.Math.Round(overallscore / 100, 1));

            Elements[4].text = System.Math.Round((7.1 - (7.1 - 2.0) * overallscore / 100) * Random.Range(0.8f, 1.2f), 1).ToString() + " " + unit[4];
            mesureVFX.SetFloat("PH", 1 - (float)System.Math.Round(overallscore / 100, 2));

            Elements[5].text = System.Math.Round((30000 - (30000 - 0) * overallscore / 100) * Random.Range(0.8f, 1.2f), 1).ToString() + " " + unit[5];
            mesureVFX.SetFloat("Conliform", 1 - (float)System.Math.Round(overallscore / 100, 1));
        }
        if (isfinished)
        {
            sign.SetText("Result:Pure Water");
            MainTextController.WordList[0] = "PURE";
         }
        if(isfinished == false && isStarted != true)
        {
            
        }


    }
    public void Onstartplay()
    {
        if (isStarted)
            coroutinestarted = !coroutinestarted;
        isStarted = !isStarted;
        mesureVFX.SetBool("Isstarted", isStarted);

    }
    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (isStarted && !isfinished)
        {
            sign.SetText("Testing...");
            yield return new WaitForSeconds(waitTime);
            sign.SetText("Testing..");
            yield return new WaitForSeconds(waitTime);

        }
    }
    public void ResetMeasurement()
    {
        if (isStarted)
            coroutinestarted = !coroutinestarted;
        isStarted = false;
        overallscore = 0;
        isfinished = false;
        timer = 0.0f;
        mesureVFX.SetBool("CalculationFinished", isfinished);

        //Reset the number for each testing elements
        Elements[0].text = System.Math.Round((200 - (200 - 2.8) * overallscore / 100) * Random.Range(0.8f, 1.2f), 1).ToString() + " " + unit[0];
        mesureVFX.SetFloat("COD", 1 - (float)System.Math.Round(overallscore / 100, 1));

        Elements[1].text = System.Math.Round((40000 - (40000 - 22) * overallscore / 100) * Random.Range(0.8f, 1.2f), 1).ToString() + " " + unit[1];
        mesureVFX.SetFloat("Fe", 1 - (float)System.Math.Round(overallscore / 100, 1));

        Elements[2].text = System.Math.Round((8000 - (8000 - 5) * overallscore / 100) * Random.Range(0.8f, 1.2f), 1).ToString() + " " + unit[2];
        mesureVFX.SetFloat("Mn", 1 - (float)System.Math.Round(overallscore / 100, 1));

        Elements[3].text = System.Math.Round((4000 - (4000 - 289) * overallscore / 100) * Random.Range(0.8f, 1.2f), 1).ToString() + " " + unit[3];
        mesureVFX.SetFloat("Conductivity", (float)System.Math.Round(overallscore / 100, 1));

        Elements[4].text = System.Math.Round((7.1 - (7.1 - 2.0) * overallscore / 100) * Random.Range(0.8f, 1.2f), 1).ToString() + " " + unit[4];
        mesureVFX.SetFloat("PH", 1 - (float)System.Math.Round(overallscore / 100, 2));

        Elements[5].text = System.Math.Round((30000 - (30000 - 0) * overallscore / 100) * Random.Range(0.8f, 1.2f), 1).ToString() + " " + unit[5];
        mesureVFX.SetFloat("Conliform", 1 - (float)System.Math.Round(overallscore / 100, 1));

        sign.SetText("Result:Pure Water");
        MainTextController.WordList[0] = "PURE";
        mesureVFX.SetFloat("Score", (float)System.Math.Round(overallscore, 1));
        mesureVFX.SetFloat("Timer", timer);
        MainTextController.WordList[0] = ((float)System.Math.Round(overallscore, 1)).ToString() + "%";
    }
}
