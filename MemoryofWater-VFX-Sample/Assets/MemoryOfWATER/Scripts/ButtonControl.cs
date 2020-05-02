using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.VFX;
using UnityEngine.UI;
using TMPro;

public class ButtonControl : MonoBehaviour
{
    [Header("General")]
    public PlayableDirector playableDirector;
    public GameObject VFXObject;
    public GameObject instruction;
    public TMP_Text[] TextMesh;
    public TMP_Text[] InputTextTMP;
    public Slider Slider1;
    private bool isactive;
    public float AnimationDelay=0;
    private IEnumerator coroutine;

    [Header("For Faucet")]
    public Button GetwaterButton;
    [Header("Extra Reference")]
    public Button[] ExtraButtons;
    [Header("CLose Only Objects")]
    public TMP_Text[] CloseOnlyTextMesh;
    // Start is called before the first frame update
    void Start()
    {
        isactive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonToggleClicked() {
        isactive = !isactive;
        if (isactive)
        {
            if(VFXObject)
                VFXObject.GetComponent<VisualEffect>().enabled = true;
            if (playableDirector)
            {
                coroutine = WaitAndPrint(AnimationDelay);
                StartCoroutine(coroutine);
                
            }
            if (instruction)
            {
                instruction.GetComponent<Text>().enabled = true;
               
            }
            if(TextMesh.Length != 0)
            {   
                foreach(TMP_Text k in TextMesh)
                {
                    k.GetComponent<MeshRenderer>().enabled = true;
                }
            }
            if (Slider1)
            {
                foreach (Transform child in this.transform)
                {

                    if (child.GetComponent<Image>())
                    {
                        child.GetComponent<Image>().enabled = true;
                    }


                    //Slider1.GetComponentInChildren<Image>().enabled = true;
                }
            }
            if (GetwaterButton)
            {
                GetwaterButton.GetComponent<Image>().enabled = true;

            }
            // use TextMeshProUGUI for the input TextMeshpro
            if (InputTextTMP.Length>0)
            {
                GameObject.Find("SearchInputField").GetComponent<TMP_InputField>().enabled = true;
                GameObject.Find("SearchInputField").GetComponent<TMP_InputField>().text = "";
                foreach (TMP_Text n in InputTextTMP)
                {
                    n.GetComponent<TextMeshProUGUI>().enabled = true;
                }
            }

            if(ExtraButtons.Length>0)
            {
                for(int i =0;i<ExtraButtons.Length;i++)
                {
                    ExtraButtons[i].GetComponent<Image>().enabled = true;
                }
            }


        }
        else
        {
            if (VFXObject)
                VFXObject.GetComponent<VisualEffect>().enabled = false;
            if (playableDirector)
            {
                playableDirector.Stop();

            }
            if (instruction)
                instruction.GetComponent<Text>().enabled = false;
            if (TextMesh.Length != 0)
            {
                foreach (TMP_Text k in TextMesh)
                {
                    //print(k.name);
                    k.GetComponent<MeshRenderer>().enabled = false;
                    if(k.GetComponentInChildren<MeshRenderer>())
                        k.GetComponentInChildren<MeshRenderer>().enabled = false;
                    if(k.transform.childCount>0)
                        k.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                }
            }
            if (GetwaterButton)
            {
                GetwaterButton.GetComponent<Image>().enabled = false;

            }
            if (Slider1)
            {
                    foreach (Transform child in this.transform)
                    {

                        if (child.GetComponent<Image>())
                        {
                            child.GetComponent<Image>().enabled = false;
                        }
                    }
            }
            if (ExtraButtons.Length > 0)
            {
                for (int i = 0; i < ExtraButtons.Length; i++)
                {
                    ExtraButtons[i].GetComponent<Image>().enabled = false;
                }
            }
            // only for the TextMsh that isn´t init by the normal button control but something else
            if (CloseOnlyTextMesh.Length != 0)
            {
                foreach (TMP_Text k in CloseOnlyTextMesh)
                {
                    k.GetComponent<MeshRenderer>().enabled = false;
                }
            }
            if (InputTextTMP.Length > 0)
            {
                foreach (TMP_Text n in InputTextTMP)
                {
                    if (n.name.Contains("InputText"))
                    {
                        print("clear");
                        //GameObject.Find("SearchInputField").GetComponent<TMP_InputField>().textComponent.SetText("\n");
                        //GameObject.Destroy(GameObject.Find("SearchInputField").GetComponent<TMP_InputField>());
                        GameObject.Find("SearchInputField").GetComponent<TMP_InputField>().text = " ";



                    }
                    n.GetComponent<TextMeshProUGUI>().enabled = false;
                    GameObject.Find("SearchInputField").GetComponent<TMP_InputField>().enabled = false;

                }
            }
        }
    }
    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        playableDirector.Play();
    }
}

