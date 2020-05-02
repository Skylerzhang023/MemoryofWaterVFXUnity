using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;
public class SliderControl : MonoBehaviour
{
    [Header("Reference")]
    public VisualEffect Mainpage;
    public static readonly string WATER_AMOUNT_NAME = "WaterAmount";
    public static readonly string RING5COLOR_NAME = "InnerBlueColor";
    public static readonly string RING2COLOR_NAME = "Ring2COlor";
    private float sliderValue;
    public Slider thisSlider;
    public Text waterAmountText;
    public VFXText MainPageText;


    private IEnumerator coroutine;
    private bool isrunout;

    [Header("Color Setting")]
    public Color AlertColorBright;
    public Color AlertColorDark;
    private Color OldRing5Color;
    private Color OldRing2Color;
    // Start is called before the first frame update
    void Start()
    {
        AlertColorBright *= 10.0f;
        OldRing5Color = Mainpage.GetVector4(RING5COLOR_NAME);
        OldRing2Color = Mainpage.GetVector4(RING2COLOR_NAME);
    }

    // Update is called once per frame
    void Update()
    {
        sliderValue = 1-thisSlider.value/200;
        // Wherever you want to stop explosion
        //print(sliderValue);
        Mainpage.SetFloat(WATER_AMOUNT_NAME, sliderValue);
        waterAmountText.text = "Water Amount:" + thisSlider.value.ToString();
        MainPageText.WordList[0] = System.Math.Round((thisSlider.value),1).ToString()+"L"; 
        if (sliderValue==1 && isrunout == false)
        {
            
            //print("runout");
            isrunout = true;
            Mainpage.SetVector4(RING5COLOR_NAME, AlertColorBright);
            coroutine = RunOutFlash(1.0f);
            StartCoroutine(coroutine);
            

        }
        else if(sliderValue != 1 )
        {
            isrunout = false;
            Mainpage.SetVector4(RING5COLOR_NAME, OldRing5Color);
            Mainpage.SetVector4(RING2COLOR_NAME, OldRing2Color);
        }
    }

    private IEnumerator RunOutFlash(float waitTime)
    {
        while (isrunout) { 
        yield return new WaitForSeconds(waitTime);
        Mainpage.SetVector4(RING5COLOR_NAME, AlertColorDark);
        Mainpage.SetVector4(RING2COLOR_NAME, AlertColorDark);
        yield return new WaitForSeconds(waitTime);
        Mainpage.SetVector4(RING5COLOR_NAME, AlertColorBright);
        Mainpage.SetVector4(RING2COLOR_NAME, AlertColorBright);
        }
    }
}
