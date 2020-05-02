using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;
using UnityEngine.EventSystems;


public class Map_Control : MonoBehaviour
{
    [Header("Camera")]
    public Camera ViewingCamera;
    public GameObject CameraRoot;
    public Vector2 CameraMinMaxDistance = new Vector2(1.0f, 2.0f);
    public Vector2 CameraMinMaxHeight = new Vector2(0.0f, 1.0f);
    public float CameraMinDistanceToUpVector = 1.0f;

    [Header("SPEEDS")]
    public float PanSpeed = 0.01f;
    public float OrbitSpeed = 0.01f;
    public float ScaleSpeed = 0.01f;

    [Header("MapHeightScale")]
    public float FWaterElevation;
    public float FElevation;
    public float FInputHeightMapScale;

    [Header("Visual Effect Configuration")]
    public VisualEffect VisualEffect;

    public Vector2 BasePosition = Vector2.zero;
    public Vector2 BaseWorldScale = Vector2.one;

    public Vector2 MinMaxWorldScale = new Vector2(1.0f, 5.0f);

    public Vector2 InputHeightLevel = new Vector2(0.1f, 5.0f);
    public Vector2 WaterElevationRange = new Vector2(0.1f, 1.0f);
    public Vector2 ElevationRange = new Vector2(0.2f, 1.0f);

    public Vector2 DeltaXRange = new Vector2(-0.02f, 0.02f);
    public Vector2 DeltaYRange = new Vector2(0.52f, 0.58f);
    private Vector2 NewDeltaXRange = new Vector2(-0.02f, 0.02f);
    private Vector2 NewDeltaYRange = new Vector2(-0.4f, 0.4f);

    //public VFXExpressionValues Position;
    [Header("Elevations")]
    public string Position = "Position";
    public string WorldSize = "WorldSize";
    public string InputHeightMapScale = "Input HeightMap Scale";
    public string WaterElevation = "Water Elevation";
    public string Elevation = "Elevation";

    [Header("Display location sign")]
    public float ScaleThreshold;
    public string SecondVisible = "SecondVisible";


    [Header("VFX Graph value")]
    public Vector2 m_Position;
    public Vector2 m_WorldSize;

    private Vector2 mousePos;
    private int clicked;
    public float mscale;

    private float newSize;
    


    private void Start()
    {
        m_Position = BasePosition;
        m_WorldSize = BaseWorldScale;
        mousePos = Input.mousePosition;
        clicked = -1;
        mscale = 1;

        Position = "Position";
        WorldSize = "WorldSize";
        InputHeightMapScale = "Input HeightMap Scale";
        WaterElevation = "Water Elevation";
        Elevation = "Elevation";
        SecondVisible = "SecondVisible";
    }

    private void Update()
    {
        // Mouse Management
        Vector2 delta = (Vector2)Input.mousePosition - mousePos;
        mousePos = Input.mousePosition;
        Vector3 worldScaleVector = delta.x * ViewingCamera.transform.right + delta.y * ViewingCamera.transform.forward;

        //mouse wheel scroll
        if (Input.mouseScrollDelta.y != 0)
        {

            float oldSize = m_WorldSize.x;
            newSize = Mathf.Clamp(ScaleSpeed * Input.mouseScrollDelta.y*10.0f + oldSize, MinMaxWorldScale.x, MinMaxWorldScale.y);
            m_WorldSize = new Vector2(newSize, newSize);
            mscale = m_WorldSize.x / BaseWorldScale.x;  
        }

        if (CheckParameters())
        {
            if (clicked == -1)
            {
                if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
                    clicked = 0;
                else if (Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject())
                    clicked = 1;
            }
            else // Manage Click
            {
                if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
                    clicked = -1;
                else
                {
                    if (clicked == 0) // Pan/Scale
                    {
                        var planeVector = new Vector2(worldScaleVector.x, worldScaleVector.z);
                        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) // scale
                        {
                            float oldSize = m_WorldSize.x;
                            newSize = Mathf.Clamp(ScaleSpeed * delta.y + oldSize, MinMaxWorldScale.x, MinMaxWorldScale.y);
                            
                            m_WorldSize = new Vector2(newSize, newSize);
                            //NewDeltaXRange = DeltaXRange *(newSize/BaseWorldScale.x);
                            //NewDeltaYRange = DeltaYRange *(newSize/BaseWorldScale.y);
                            mscale = m_WorldSize.x / BaseWorldScale.x;

                        }
                       
                        else//pan
                        {
                            float yrange = DeltaYRange.y - DeltaYRange.x;  
                            float yMiddle = 0.5f * (DeltaYRange.y + DeltaYRange.x);
                            float xrange = DeltaXRange.y - DeltaXRange.x;
                            float xMiddle = 0.5f * (DeltaXRange.y + DeltaXRange.x);
                            m_Position += planeVector * (PanSpeed / m_WorldSize.x);
                            if (mscale < 2)
                            {
                                m_Position.x = Mathf.Clamp(m_Position.x, xMiddle - (xrange * mscale * mscale / 2), xMiddle + (xrange * mscale * mscale / 2));
                                m_Position.y = Mathf.Clamp(m_Position.y, yMiddle - (yrange * mscale * mscale / 2), yMiddle + (yrange * mscale * mscale / 2));
                            }
                            else
                            {
                                m_Position.x = Mathf.Clamp(m_Position.x, xMiddle - (xrange * mscale), xMiddle + (xrange * mscale));
                                m_Position.y = Mathf.Clamp(m_Position.y, yMiddle - (yrange * mscale), yMiddle + (yrange * mscale));
                            }
                        }

                    }
                    else if (clicked == 1) // Orbit
                    {
                        //print(delta.x);
                        float distance = (ViewingCamera.transform.position - CameraRoot.transform.position).magnitude;
                        ViewingCamera.transform.position -= OrbitSpeed * delta.x * ViewingCamera.transform.right + OrbitSpeed * delta.y * ViewingCamera.transform.up;

                        Vector3 direction = (ViewingCamera.transform.position - CameraRoot.transform.position).normalized;
                        ViewingCamera.transform.position = CameraRoot.transform.position + distance * direction;

                        float height = Mathf.Clamp(ViewingCamera.transform.position.y, CameraMinMaxHeight.x, CameraMinMaxHeight.y);
                        Vector2 upAxisOffset = new Vector2(ViewingCamera.transform.position.x, ViewingCamera.transform.position.z);

                        upAxisOffset = upAxisOffset.normalized * Mathf.Max(upAxisOffset.magnitude, CameraMinDistanceToUpVector);

                        ViewingCamera.transform.position = new Vector3(upAxisOffset.x, height, upAxisOffset.y);
                    }
                }
            }


            float dist = (ViewingCamera.transform.position - CameraRoot.transform.position).magnitude;
            Vector3 dir = (ViewingCamera.transform.position - CameraRoot.transform.position).normalized;

            if (Input.mouseScrollDelta.y != 0)
            {

                dist += Input.mouseScrollDelta.y * 0.1f;

            }

            dist = Mathf.Clamp(dist, CameraMinMaxDistance.x, CameraMinMaxDistance.y);
            ViewingCamera.transform.position = CameraRoot.transform.position + dist * dir;
            

            VisualEffect.SetVector2(Position, m_Position);
            VisualEffect.SetVector2(WorldSize, m_WorldSize);

            // Sliders
            float inputHeightMapScale = Mathf.Lerp(InputHeightLevel.x, InputHeightLevel.y, FInputHeightMapScale);
            float elevation = Mathf.Lerp(ElevationRange.x, ElevationRange.y, FElevation);
            float waterElevation = Mathf.Lerp(WaterElevationRange.x, WaterElevationRange.y, FWaterElevation);

            //CameraRoot.transform.position = new Vector3(CameraRoot.transform.position.x, waterElevation, CameraRoot.transform.position.z);
            ViewingCamera.transform.LookAt(CameraRoot.transform);

            VisualEffect.SetFloat(InputHeightMapScale, inputHeightMapScale);
            VisualEffect.SetFloat(Elevation, elevation);
            VisualEffect.SetFloat(WaterElevation, waterElevation);

            //check whether the secondary location sign should be shown
            if (m_WorldSize.x > ScaleThreshold) { 
                VisualEffect.SetBool(SecondVisible, true);
                //print("show");
            }
            else
                VisualEffect.SetBool(SecondVisible, false);



        }
    }

    private bool CheckParameters()
    {
        return CameraRoot != null &&
            ViewingCamera != null &&

            VisualEffect != null &&
            VisualEffect.HasVector2(Position) &&
            VisualEffect.HasVector2(WorldSize) &&
            VisualEffect.HasFloat(InputHeightMapScale) &&
            VisualEffect.HasFloat(WaterElevation) &&
            VisualEffect.HasFloat(Elevation);
    }
}

