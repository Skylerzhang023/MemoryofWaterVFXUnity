using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class WaterFaucetButton : MonoBehaviour
{
    public bool isplaying;
    public TMP_Text available;
    public TMP_Text used;
    public Slider wateramountSlider;
    private float currentwater;
    public float remaining;
    public float usednumber;
    public float WaterAmount;

    // Start is called before the first frame update
    void Start()
    {
        isplaying = false;
        remaining = 482.0f;
        usednumber = 0;
        available.SetText(System.Math.Round(remaining, 1).ToString());
        used.SetText(System.Math.Round(usednumber, 1).ToString());
        WaterAmount = wateramountSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (!isplaying)
        {
            WaterAmount = wateramountSlider.value;
        }
        
        if (isplaying)
        {
            remaining -= Time.deltaTime;
            usednumber += Time.deltaTime;
            WaterAmount -= Time.deltaTime;
            wateramountSlider.value = WaterAmount;
            available.SetText(System.Math.Round(remaining, 1).ToString());
            used.SetText(System.Math.Round(usednumber, 1).ToString());
        }
    }
    public void OnToggleClicked()
    {
        isplaying = !isplaying;
    }
}
