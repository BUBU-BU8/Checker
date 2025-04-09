using UnityEngine;
using Mirror;

public class BoardManager : NetworkBehaviour
{
    public int boardSize = 8;
    public GameObject tilePrefab;
    public GameObject redPiecePrefab;
    public GameObject bluePiecePrefab;

    public Color color1 = Color.white;
    public Color color2 = Color.black;

    private Vector2 offset = new Vector2(-(8 - 1) / 2f, -(8 - 1) / 2f);

    void Start()
    {
        GenerateBoard();

        if (isServer)
        {
            PlacePieces();
        }
    }

    void GenerateBoard()
    {
        // สร้างกระดานขนาด 8x8
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                Vector3 spawnPos = new Vector3(x, y, 0) + (Vector3)offset;
                GameObject tile = Instantiate(tilePrefab, spawnPos, Quaternion.identity);
                tile.transform.parent = this.transform;

                tile.name = $"Tile_{x}_{y}";

                SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
                sr.color = (x + y) % 2 == 0 ? color1 : color2;
            }
        }
    }

    void PlacePieces()
    {
        Vector2 offset = new Vector2(-(boardSize - 1) / 2f, -(boardSize - 1) / 2f);

        // วางหมาก Blue
        for (int y = 0; y <= 1; y++)
        {
            for (int x = 0; x < boardSize; x++)
            {
                if ((x + y) % 2 != 0)  // วางหมากเฉพาะช่องที่เป็นสีเข้ม
                {
                    GameObject bluePiece = Instantiate(bluePiecePrefab, new Vector3(x, y, -1) + (Vector3)offset, Quaternion.identity, this.transform);
                    NetworkServer.Spawn(bluePiece); // Spawn หมายเลขบน Server

                    Piece piece = bluePiece.GetComponent<Piece>();
                    piece.SetTeam("Blue"); // ตั้งค่าทีมเป็น Blue
                }
            }
        }

        // วางหมาก Red
        for (int y = 6; y <= 7; y++)
        {
            for (int x = 0; x < boardSize; x++)
            {
                if ((x + y) % 2 != 0)  // วางหมากเฉพาะช่องที่เป็นสีเข้ม
                {
                    GameObject redPiece = Instantiate(redPiecePrefab, new Vector3(x, y, -1) + (Vector3)offset, Quaternion.identity, this.transform);
                    NetworkServer.Spawn(redPiece); // Spawn หมายเลขบน Server

                    Piece piece = redPiece.GetComponent<Piece>();
                    piece.SetTeam("Red"); // ตั้งค่าทีมเป็น Red
                }
            }
        }
    }
}
