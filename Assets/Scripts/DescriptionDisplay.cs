using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DescriptionDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text info;
    public TMP_Text entityName;
    public TMP_Text cost;
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
        if (obj.CompareTag("Tower"))
        {
            TowerData data = obj.GetComponent<Tower>().data;
            info.text = data.description;
            entityName.text = data.name;
            cost.text = data.cost.ToString();

        }
        else
        {
            EnemyData data = obj.GetComponent<Enemy>().data;
            info.text = data.description;
            entityName.text = data.name;
            cost.text = data.cost.ToString();

        }
        


    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
