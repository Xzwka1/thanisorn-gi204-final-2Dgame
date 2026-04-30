using UnityEngine;

public class BeeTarget : MonoBehaviour
{
    [Header("การเคลื่อนที่ (ลอยขึ้น-ลง แกน Y)")]
    public float moveSpeed = 3f;    // ปรับให้ผึ้งบินกระพือปีกเร็วขึ้นนิดนึง
    public float moveRange = 2f;    // ระยะบินขึ้นลง
    private Vector3 startPos;

    [Header("ฟิสิกส์ตอนโดนยิง")]
    public float torqueForce = 20f; // ผึ้งตัวเล็ก อาจจะตั้งให้โดนยิงแล้วหมุนติ้วแรงกว่าปลานิดนึง
    public float destroyDelay = 1.5f;
    private bool isHit = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;

        // เซ็ตให้ผึ้งลอยตัวได้ ไม่ตกพื้น
        if (rb != null) rb.gravityScale = 0;
    }

    void Update()
    {
        // บินขึ้นลงตามจังหวะ Sine Wave
        if (!isHit)
        {
            float newY = startPos.y + Mathf.Sin(Time.time * moveSpeed) * moveRange;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    public void OnHit()
    {
        if (isHit) return;
        isHit = true;
        Debug.Log("🎯 ผึ้งโดนยิงร่วงแล้ว!");

        if (rb != null)
        {
            // 🌟 ล็อคคะแนนฟิสิกส์การหมุน (Torque = I * alpha) 🌟
            float I = rb.inertia;
            float alpha = torqueForce;
            float calculatedTorque = I * alpha;
            rb.AddTorque(calculatedTorque, ForceMode2D.Impulse);
        }

        // เพิ่มคะแนนข้าม Scene
        if (ScoreManager.totalScore >= 0)
        {
            ScoreManager.instance.AddScore(1);
        }

        Destroy(gameObject, destroyDelay);
    }
}