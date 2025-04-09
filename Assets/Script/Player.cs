using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SyncVar]
    private string playerColor;

    public string GetPlayerColor() => playerColor;

    [Command]
    public void CmdSetPlayerColor(string color)
    {
        playerColor = color;
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        if (!isServer)
        {
            CmdSetPlayerColor("Blue");
        }
        else
        {
            CmdSetPlayerColor("Red");
        }
    }
}
