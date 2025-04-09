using UnityEngine;
using Mirror;

public class GameManager : MonoBehaviour
{
    public Camera cam;
    public LayerMask tileLayer;
    public LayerMask pieceLayer;
    public Player player; // อ้างอิง Player ที่ควบคุมหมากฝั่งตัวเอง

    public Piece selectedPiece;
    public bool isRedTurn = true;

    private Vector3 offset = new Vector3(-(8 - 1) / 2f, -(8 - 1) / 2f, -1);

    void Start()
    {
        // Auto หา Player ตัวเอง
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    void HandleClick()
    {
        if (player == null) return; // ยังไม่มี Player ให้กดอะไรไม่ได้เลย

        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D tileHit = Physics2D.Raycast(mousePos, Vector2.zero, 1f, tileLayer);

        if (tileHit.collider != null)
        {
            int x = Mathf.RoundToInt(tileHit.collider.transform.position.x - offset.x);
            int y = Mathf.RoundToInt(tileHit.collider.transform.position.y - offset.y);

            Debug.Log("Click Tile : " + x + "," + y);

            TryMoveOrSelect(x, y);
        }
    }

    void TryMoveOrSelect(int x, int y)
    {
        Debug.Log("Click at : " + x + "," + y);

        Vector2 clickPos = new Vector2(x + offset.x, y + offset.y);
        Collider2D pieceHit = Physics2D.OverlapPoint(clickPos, pieceLayer);

        if (pieceHit != null)
        {
            Piece p = pieceHit.GetComponent<Piece>();

            if (p != null)
            {
                if (player != null && ((player.GetPlayerColor() == "Red" && p.isRed) || (player.GetPlayerColor() == "Blue" && !p.isRed)))
                {
                    selectedPiece = p;
                    Debug.Log("Selected Piece : " + p.name);
                }
                else
                {
                    Debug.Log("ไม่ใช่ตาของหมากนี้ หรือ Player ยังไม่พร้อม");
                }
            }
        }
        else if (selectedPiece != null)
        {
            Debug.Log("Move Piece To : " + x + "," + y);

            int fromX = Mathf.RoundToInt(selectedPiece.transform.position.x - offset.x);
            int fromY = Mathf.RoundToInt(selectedPiece.transform.position.y - offset.y);

            // Move 1 ช่อง เฉียง
            if (Mathf.Abs(fromX - x) == 1 && Mathf.Abs(fromY - y) == 1)
            {
                selectedPiece.transform.position = new Vector3(x, y, -1) + offset;

                isRedTurn = !isRedTurn;
                selectedPiece = null;

                Debug.Log("Moved!!");
            }
            // Jump 2 ช่อง เฉียง
            else if (Mathf.Abs(fromX - x) == 2 && Mathf.Abs(fromY - y) == 2)
            {
                int midX = (fromX + x) / 2;
                int midY = (fromY + y) / 2;

                Vector2 midPos = new Vector2(midX + offset.x, midY + offset.y);
                Collider2D midPiece = Physics2D.OverlapPoint(midPos, pieceLayer);

                if (midPiece != null)
                {
                    Piece targetPiece = midPiece.GetComponent<Piece>();

                    if (targetPiece != null && targetPiece.isRed != selectedPiece.isRed)
                    {
                        Destroy(targetPiece.gameObject);

                        selectedPiece.transform.position = new Vector3(x, y, -1) + offset;

                        isRedTurn = !isRedTurn;
                        selectedPiece = null;

                        Debug.Log("Jump & Eat!!");
                    }
                }
            }
        }
    }
}
