using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnPlayerColorChanged))]
    public string playerColor;

    private static int playerCount = 0;

    public override void OnStartServer()
    {
        base.OnStartServer();
        playerColor = (playerCount % 2 == 0) ? "Red" : "Blue";
        playerCount++;
    }

    void OnPlayerColorChanged(string oldColor, string newColor)
    {
        Debug.Log($"Player {netId} color changed to {newColor}");
        // อัปเดต UI หรือสีได้
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    void HandleClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject clickedObject = hit.collider.gameObject;
            Debug.Log($"Clicked on {clickedObject.name}");

            // ตรวจสอบเงื่อนไขเพิ่มเติมก่อนส่งคำสั่งไป Server
            CmdHandleClick(clickedObject.name);
        }
    }

    [Command]
    void CmdHandleClick(string clickedObjectName)
    {
        Debug.Log($"{playerColor} clicked on {clickedObjectName}");
        // Server ตรวจสอบการเล่นตามกติกา
    }
}
