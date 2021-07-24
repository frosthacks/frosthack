using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    private Vector3 dragOrigin;
    [SerializeField]
    Camera cam;
    public Vector2 origin;
    public float speed = 0.1f;
    public Vector2 target;
    public bool hasTarget=false;
    public float distanceThreshold = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        SetOrigin(transform.position);
        
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
        if (!hasTarget)
        {
            PanCamera();


        }
        else
        {
            Vector3 t = new Vector3(target.x, target.y, cam.transform.position.z);
            cam.transform.position = Vector3.Lerp(cam.transform.position,t , speed);
            if (Vector3.Distance(cam.transform.position, t) < distanceThreshold)
            {
                hasTarget = false;
            }

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
}
