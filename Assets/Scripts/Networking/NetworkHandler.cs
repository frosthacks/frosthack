using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class NetworkHandler : MonoBehaviour
{
    public enum BuildType { Server, WebClient, HostTest};
    public TMP_InputField usernameField;
    public NetworkManager manager;
    public BuildType BuildAs;
    public string username;

    private void Start()
    {
        if (BuildAs == BuildType.Server)
        {
            manager.StartServer();
        }else if (BuildAs == BuildType.HostTest)
        {
            manager.StartHost();
        }
    }

    public void joinGame()
    {
        username = usernameField.text == "" ? "John Doe" : usernameField.text; // send this over later
        manager.StartClient();
    }
}
