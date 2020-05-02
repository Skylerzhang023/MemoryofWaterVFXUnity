using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using TMPro;

public class NotificationControll : MonoBehaviour
{
    // Start is called before the first frame update
    private float floating;
    public VisualEffect notification;
    public TMP_Text[] Elements;
    private Vector3[] unit;
    public float speed;
    public float FloatingScale;
    Vector3 newpostion;
    public void Start()
    {
        floating = 0;
        //
        unit = new Vector3[Elements.Length];
        for (int i = 0; i < Elements.Length; i++)
        {
            unit[i] = Elements[i].rectTransform.position;
            //print(unit[i]);
            //print(Elements[i].rectTransform.position);
        }
        FloatingScale = 0.2f;
        speed = 1;
    }

    // Update is called once per frame
    public void Update()
    {
        floating = FloatingScale*Mathf.Sin(Time.fixedTime* speed);
        notification.SetFloat("floating", floating);
        for (int i = 0; i < Elements.Length; i++)
        {
            //Elements[i].GetComponent<RectTransform>().set
           newpostion = new Vector3(unit[i].x , unit[i].y + floating, unit[i].z);
           //print(newpostion);
           Elements[i].GetComponent<RectTransform>().position= new Vector3(newpostion.x, newpostion.y, newpostion.z);
        }
    }
}
