using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkPlayer : NetworkBehaviour
{
    public UserManager dataSingleton;
    public PlayerUIInfo uiAdjust;

    // initialize these variables and they replicate
    [SyncVar(hook = nameof(nameChanged))]
    public string username = "";

    public void nameChanged(string oldValue, string newValue)
    {
        if (uiAdjust != null)
        {
            uiAdjust.username.text = newValue;
        }
    }

    [SyncVar(hook = nameof(locationChanged))]
    public Vector3 location = new Vector3();

    public void locationChanged(Vector3 oldValue, Vector3 newValue)
    {
        if (isLocalPlayer)
        {
            if (uiAdjust != null)
            {
                GameObject cam = GameObject.Find("Main Camera");
                cam.transform.position = newValue + new Vector3(0, 0, -10);
                cam.GetComponent<CameraController>().SetOrigin(new Vector2(newValue.x, newValue.y));
            }
        }
    }


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

    public void setClientMoney(int newAmount)
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

    [TargetRpc]
    public void RpcMoney(int newAmount)
    {
        setClientMoney(newAmount);    
    }
}
