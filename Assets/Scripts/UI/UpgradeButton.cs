using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(PurchaseDisable))]
public class UpgradeButton : MonoBehaviour
{

    Button button;
    Image img;
    PurchaseDisable purchaseDisabler;

    void Start() {
        button = GetComponent<Button>(); 
        img = button.gameObject.GetComponent<Image>();
        purchaseDisabler = button.gameObject.GetComponent<PurchaseDisable>();
    }

    void Update() {

        /* don't show button if there nothing selected */
        // img.enabled = TowerManager.Global.isSelectingTower();
        purchaseDisabler.prefab = TowerManager.Global.getTowerUpgrade();

    }
}
