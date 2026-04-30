using UnityEngine;

public class SimpleTarget_y : MonoBehaviour
{
    [Header("การเคลื่อนที่ (ซ้าย-ขวา)")]
    public float moveSpeed = 2f;    // ความเร็วในการสไลด์
    public float moveRange = 3f;    // ระยะทางที่สไลด์ออกไป (ยิ่งเยอะยิ่งเดินกว้าง)
    private Vector3 startPos;

    [Header("ฟิสิกส์ตอนโดนยิง")]
    public float torqueForce = 15f;   // แรงหมุน
    public float destroyDelay = 1.5f; // โดนยิงแล้วกี่วินาทีถึงจะหายไป
    private bool isHit = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position; // จำตำแหน่งเริ่มต้นไว้

        // ตั้งค่า Rigidbody ให้ไม่ร่วง และหมุนได้อิสระ
        if (rb != null)
        {
            rb.gravityScale = 0;
        }
    }

    void Update()
    {
        // ถ้ายังไม่โดนยิง ให้เดินซ้ายขวา
        if (!isHit)
        {
            // 🌟 จุดที่เปลี่ยน: เปลี่ยนมาคำนวณแกน X แทนแกน Y
            float newX = startPos.x + Mathf.Sin(Time.time * moveSpeed) * moveRange;

            // อัปเดตตำแหน่ง โดยให้แกน Y และ Z อยู่คงที่เหมือนเดิม
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }

    // ฟังก์ชันโดนยิง (เหมือนเดิมเป๊ะ)
    public void OnHit()
    {
        if (isHit) return;

        isHit = true;
        Debug.Log("🎯 เป้าโดนยิง!");

        // 1. ใส่แรงหมุน (Torque)
        if (rb != null)
        {
            rb.AddTorque(torqueForce, ForceMode2D.Impulse);
        }

        // 2. เพิ่มคะแนน
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScore(1);
        }

        // 3. สั่งให้หายไปหลังจากเวลาที่กำหนด
        Destroy(gameObject, destroyDelay);
    }
}