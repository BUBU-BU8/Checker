# Multiplayer Checker Game (Unity 2D)

## รายละเอียดเกม
เกมหมากฮอส 2D (Checker) เล่นได้ 2 คน แบบ Multiplayer โดยผู้เล่นแต่ละคนจะควบคุมหมากฝั่งของตัวเอง (Red / Blue) ผ่านระบบออนไลน์ สามารถเดินหมาก และกินหมากฝ่ายตรงข้ามได้ตามกติกา

---

## วิธีการเล่น
- ผู้เล่นฝั่ง Red = Host  
- ผู้เล่นฝั่ง Blue = Client  
- คลิกเลือกหมากของตัวเอง → แล้วคลิกที่ช่องที่ต้องการเดิน  
- เดินเฉียงได้ทีละ 1 ช่อง  
- ถ้ากระโดดข้ามหมากศัตรูได้ (2 ช่อง) จะเป็นการกินหมาก  
- ผลัดกันเดินทีละตา (Turn-Based)  
- เมื่อหมากฝ่ายใดหมดก่อน ถือว่าแพ้  

---

## โครงสร้างระบบ Multiplayer ที่ใช้
- Engine: Unity 2D
- Multiplayer: Mirror Networking
- ฟีเจอร์หลักที่ Sync ข้าม Network :
  - การเดินหมาก (Move Piece)
  - การกินหมาก (Jump & Eat)
  - การเช็คเทิร์น (Turn Manager)
  - Sync ตำแหน่งหมากแบบ Real-Time

---

## ปัญหาที่พบ และวิธีการแก้ไข

| ปัญหา | วิธีแก้ |
|-------|---------|
| NullReferenceException (เจอใน GameManager.cs) | เพิ่มการเช็ค player != null ก่อนใช้งาน และเชื่อม Object Player ให้ถูกต้องผ่าน Inspector |
| ปัญหา Host / Client เห็นหมากไม่ตรงกัน | ใช้ NetworkTransform + SyncVar ของ Mirror เพื่อ Sync ตำแหน่งหมาก |
| Host เดินหมาก Blue ได้ / Client เดินหมาก Red ได้ | เพิ่มระบบเช็ค PlayerColor ใน Player.cs และให้หมากเดินได้เฉพาะสีของตัวเองเท่านั้น |
| ระบบ Eat Piece (กินหมาก) ไม่ Sync ข้าม Network | ใช้ Command กับ ClientRpc ของ Mirror ในการ Destroy หมากที่ถูกกิน |
