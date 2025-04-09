using UnityEngine;
using Mirror;

public class Piece : NetworkBehaviour
{
    [SyncVar] public bool isRed;           // เช็คว่าเป็นหมากแดงหรือไม่
    [SyncVar] public bool isKing = false;  // เช็คว่าเป็นหมากคิงหรือไม่

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdatePieceAppearance();
    }

    // ฟังก์ชันสำหรับการเซ็ตทีมของหมาก
    public void SetTeam(string team)
    {
        isRed = team == "Red";  // ถ้าเป็น "Red" ก็จะตั้งค่า isRed เป็น true

        UpdatePieceAppearance();  // อัพเดตการแสดงผลตามทีมที่เลือก
    }

    // ฟังก์ชันอัพเดตการแสดงผลของหมากตามทีมและสถานะการเป็นคิง
    void UpdatePieceAppearance()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        // กำหนดสีของหมากตามทีม
        spriteRenderer.color = isRed ? Color.red : Color.blue;

        // หากเป็นคิงให้เปลี่ยนสีหรือเพิ่มกราฟิกอื่นๆ
        if (isKing)
        {
            // สามารถเพิ่มการเปลี่ยนแปลงกราฟิก เช่น การเพิ่มวงกลมหรือสิ่งอื่นๆ
            spriteRenderer.sortingOrder = 1;  // อาจจะใช้เปลี่ยนลำดับการแสดงผล
        }
    }

    // ฟังก์ชันที่ตรวจสอบว่าหมากสามารถกลายเป็นคิงได้หรือไม่
    public void PromoteToKing()
    {
        isKing = true;  // ตั้งค่าให้หมากเป็นคิง
        UpdatePieceAppearance();  // อัพเดตการแสดงผลหลังจากการโปรโมท
    }
}
