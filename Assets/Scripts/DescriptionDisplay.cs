using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DescriptionDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public Text info;
    public Text towerName;
    public Text cost;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Display(GameObject obj)
    {
        gameObject.SetActive(true);
        TowerData data = obj.GetComponent<Tower>().data;
        info.text = data.description;
        towerName.text = data.name;
        cost.text = data.cost.ToString();


    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
