using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    GameObject tower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tower != null)
        {
            tower.transform.position = ScreenToWorld(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("left Click");
                tower.GetComponent<Tower>().placed = true;
                tower = null;

            }
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("right click");
                Destroy(tower);
                tower = null;

            }

        }



    }
    public void OnCreate(GameObject prefab)
    {
        tower = Instantiate(prefab, ScreenToWorld(Input.mousePosition), Quaternion.identity);
        tower.transform.SetParent(GameObject.Find("/Towers").transform);
        
        
        

    }
    public Vector3 ScreenToWorld(Vector3 mousePos)
    {
        Vector3 stw = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return new Vector3(stw.x, stw.y, 0);

    }
}
