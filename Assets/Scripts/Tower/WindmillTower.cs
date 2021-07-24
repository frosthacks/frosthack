using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillTower : Tower
{
    public int moneyAmount;
    public int moneyInterval = 10;

    void Start() {

        StartCoroutine(generateMoney());
    }

    private IEnumerator generateMoney() {
        
        while (true) {

            /* check to see if round is ongoing first */

            yield return new WaitForSeconds(moneyInterval);

            UserManager.Global.gainMoney(moneyAmount);

        }

    }

}
