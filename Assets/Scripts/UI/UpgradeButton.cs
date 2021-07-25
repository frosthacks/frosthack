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
        GameObject upgrade = TowerManager.Global.getTowerUpgrade();
        img.enabled = (TowerManager.Global.isSelectingTower() && upgrade != null);
        purchaseDisabler.prefab = upgrade;

    }
}
