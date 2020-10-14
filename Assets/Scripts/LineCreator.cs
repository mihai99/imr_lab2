using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class LineCreator : MonoBehaviour
{
    public GameObject SceneObjects;
    public GameObject LinePrefab;
    private GameObject PenTip;
    public float Precision;
    private LineRenderer LineRenderer;
    private VRTK_InteractableObject InteractableObject;

    private void Start()
    {
        InteractableObject = GetComponent<VRTK_InteractableObject>();
        PenTip = transform.GetChild(0).transform.gameObject;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(InteractableObject.IsGrabbed() && LineRenderer == null)
            {
                GameObject lineObject = Instantiate(LinePrefab, Vector3.zero, Quaternion.identity);
                lineObject.transform.parent = SceneObjects.transform;
                LineRenderer = lineObject.GetComponent<LineRenderer>();
                LineRenderer.positionCount = 1;
                LineRenderer.SetPosition(0, PenTip.transform.position);
            } else if(LineRenderer != null)
            {
                LineRenderer = null;
            }

        }

        if(LineRenderer)
        {
            if(Vector3.Distance(LineRenderer.GetPosition(LineRenderer.positionCount-1), PenTip.transform.position) >= Precision)
            {
                LineRenderer.positionCount += 1;
                LineRenderer.SetPosition(LineRenderer.positionCount-1, PenTip.transform.position);
            }
        }
        
    }
}
