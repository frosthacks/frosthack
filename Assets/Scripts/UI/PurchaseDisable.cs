using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PurchaseDisable : MonoBehaviour
{
    public GameObject prefab;
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        
    }
    int GetPrefabCost()
    {
        if (prefab.CompareTag("Enemy"))
        {
            return prefab.GetComponent<Enemy>().data.cost;

        }
        else
        {
            return prefab.GetComponent<Tower>().data.cost;

        }
        

    }

    // Update is called once per frame
    void Update()
    {

        
        if (prefab==null||UserManager.Global.money < GetPrefabCost())
        {
            button.interactable = false;

        }
        else
        {
            button.interactable = true;
        }

    }
}
