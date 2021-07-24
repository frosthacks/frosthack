using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    
    
    public Camera cam;
    public float zoomStep, minCamSize, maxCamSize,speed;

    private float initialSize;
    private Vector2 origin;
    private Vector3 dragOrigin;
    private float distanceThreshold = 0.2f;


    private Vector2 target;
    private bool hasTarget=false;
    
    // Start is called before the first frame update
    void Start()
    {
        SetOrigin(transform.position);
        initialSize = cam.orthographicSize;
        
    }
    void SetOrigin(Vector2 origin)
    {
        this.origin = origin;
        SetTarget(origin);

    }

    void SetTarget(Vector2 target)
    {
        hasTarget = true;
        this.target = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasTarget)
        {
            Vector3 t = new Vector3(target.x, target.y, cam.transform.position.z);
            cam.transform.position = Vector3.Lerp(cam.transform.position, t, speed);

            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, initialSize, zoomStep);
            if ((Vector3.Distance(cam.transform.position, t) < distanceThreshold)&&(Mathf.Abs(cam.orthographicSize-initialSize)<distanceThreshold))
            {
                hasTarget = false;
            }


        }
        else
        {
            PanCamera();
            Zoom();
            

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetTarget(origin);
        }
        

        
        
    }
    void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(true);
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

        }
        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += difference;
            
        }
    }
    public void Zoom() 
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        if (zoom != 0f)
        {
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + Mathf.Sign(zoom) * zoomStep, minCamSize, maxCamSize);

        }
        
    }
}
