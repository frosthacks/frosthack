using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillTower : Tower
{
    public int moneyAmount;
    public int moneyInterval = 10;

    float lastTime = -1;
    private void Update()
    {
        if (isServer)
        {

            if (Time.fixedUnscaledTime - lastTime >= moneyInterval && GameHandler.Global.roundCountDown == -1)
            {
                lastTime = Time.fixedUnscaledTime;
                creator.incrementMoney(moneyAmount);

            }
        }
    }

}
