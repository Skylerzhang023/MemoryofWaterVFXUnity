using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocationTextMesh : MonoBehaviour
{
    public float ColorPower=2;
    private TextMeshPro textmeshPro;
    // Start is called before the first frame update
    void Start()
    {
        textmeshPro = GetComponent<TextMeshPro>();
        textmeshPro.color = ColorPower * textmeshPro.color;
    }

    // Update is called once per frame
    void Update()
    {
        textmeshPro.color = ColorPower * textmeshPro.color;
    }
}
