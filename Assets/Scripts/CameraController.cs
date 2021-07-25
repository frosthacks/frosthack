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
    public GameObject bounds;
    public GameObject worldBound;

    // Start is called before the first frame update
    void Start()
    {
        SetOrigin(transform.position);
        initialSize = cam.orthographicSize;
        bounds.transform.position = new Vector3(origin.x, origin.y, 0);

    }
    public void SetOrigin(Vector2 origin)
    {
        this.origin = origin;
        SetTarget(origin);

    }

    public void SetTarget(Vector2 target)
    {
        hasTarget = true;
        this.target = target;
    }
    public Vector2 getOrigin()
    {
        return origin;
    }
    public bool InBounds(Vector3 pos)
    {
        
        BoxCollider2D collider = bounds.GetComponent<BoxCollider2D>();
        return collider.bounds.Contains(pos);

    }
    public bool InWorldBounds(Vector3 pos)
    {
        BoxCollider2D collider = worldBound.GetComponent<BoxCollider2D>();
        return collider.bounds.Contains(pos);

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
        bounds.transform.position = new Vector3(origin.x, origin.y, 0);




    }
    void PanCamera()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(true);
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

        }
        if (Input.GetMouseButton(1))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            if (InWorldBounds(cam.transform.position + difference))
            {
                cam.transform.position += difference;

            }
            
            
        }
    }
    public void Zoom() 
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        if (zoom != 0f)
        {
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - Mathf.Sign(zoom) * zoomStep, minCamSize, maxCamSize);

        }
        
    }
}
