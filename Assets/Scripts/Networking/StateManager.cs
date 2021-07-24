using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public abstract class StateManager : NetworkBehaviour
{
    public abstract void onBegin(Dictionary<NetworkIdentity, string> players);
    public abstract void onEnter(NetworkConnection conn, string username);
    public abstract void onLeave(NetworkConnection conn);
}
