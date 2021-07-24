using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkPlayer : NetworkBehaviour
{
    public UserManager dataSingleton;
    public PlayerUIInfo uiAdjust;

    // initialize these variables and they replicate
    [SyncVar]
    public string username;

    [SyncVar]
    public Vector3 location;

    // Hp synchronization calls a client hook to handle UI
    [SyncVar(hook = nameof(healthChanged))]
    public int hp = 100;
    
    public void healthChanged(int oldValue, int newValue)
    {
        if (isLocalPlayer)
        {
            if (dataSingleton != null)
            {

                dataSingleton.health = newValue;
            }
        }
        if (uiAdjust != null)
        {
            uiAdjust.healthText.text = newValue.ToString();
            uiAdjust.healthFill.localScale = new Vector3(newValue == 0 ? 0 : newValue/100f, 1, 1);

        }
    }

    // gets synchronized locally with targetRPCs to avoid other players seeing your money balance
    public int money = 200;
    [Server]
    public void incrementMoney(int amount)
    {
        money += amount;
        RpcMoney(money);
    }

    [TargetRpc]
    public void RpcMoney(int newAmount)
    {
        money = newAmount;
        if (dataSingleton != null)
        {
            dataSingleton.money = newAmount;
        }

        if (uiAdjust != null && uiAdjust.cashText != null)
        {
            uiAdjust.cashText.text = "$" + newAmount.ToString();
        }
    }
}
