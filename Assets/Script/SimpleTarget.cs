using UnityEngine;

public class SimpleTarget : MonoBehaviour
{
    [Header("การเคลื่อนที่")]
    public float floatSpeed = 1f;    // ความเร็วการลอย
    public float floatRange = 0.5f; // ระยะที่ลอยขึ้นลง
    private Vector3 startPos;

    [Header("ฟิสิกส์ตอนโดนยิง")]
    public float torqueForce = 15f;   // แรงหมุน
    public float destroyDelay = 1f; // โดนยิงแล้วกี่วินาทีถึงจะหายไป
    private bool isHit = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;

        // ตั้งค่า Rigidbody ให้ไม่ร่วง และหมุนได้อิสระ
        rb.gravityScale = 0;
    }

    void Update()
    {
        if (!isHit)
        {
            // ทำให้วัตถุลอยขึ้นลงช้าๆ แบบ Sine Wave
            float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatRange;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    // ฟังก์ชันนี้ถูกเรียกจากสคริปต์กระสุน
    public void OnHit()
    {
        if (isHit) return; // ป้องกันการโดนซ้ำ

        isHit = true;
        Debug.Log("🎯 เป้าโดนยิง!");

        // 1. ใส่แรงหมุน (Torque)
        rb.AddTorque(torqueForce, ForceMode2D.Impulse);

        // 2. เพิ่มคะแนน (เรียก ScoreManager ที่เราทำไว้คราวก่อน)
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScore(1);
        }

        // 3. สั่งให้หายไปหลังจากเวลาที่กำหนด
        Destroy(gameObject, destroyDelay);
    }
}