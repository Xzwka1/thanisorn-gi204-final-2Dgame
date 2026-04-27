using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [Header("ตั้งค่ากระสุน")]
    public float lifeTime = 3f; // เวลาที่จะทำลายตัวเองถ้าไม่ชนอะไรเลย (กันเกมกระตุก)

    void Start()
    {
        // เริ่มมาปุ๊บ นับเวลาถอยหลัง 3 วิ ถ้าไม่โดนอะไรเลยให้พังตัวเองทิ้ง
        Destroy(gameObject, lifeTime);
    }

    // ฟังก์ชันนี้จะทำงานเมื่อกระสุน (ที่ติ๊ก Is Trigger) ไปชนกับ Collider2D อื่นๆ
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // 1. ปริ้นท์บอกใน Console ทันทีว่าชนอะไร จะได้รู้ว่าฟิสิกส์ทำงานไหม
        Debug.Log("💥 กระสุนชนกับ: " + hitInfo.name);

        // 2. เช็คว่าสิ่งที่ชน มีสคริปต์ SimpleTarget แปะอยู่ไหม
        SimpleTarget target = hitInfo.GetComponent<SimpleTarget>();

        if (target != null)
        {
            Debug.Log("🎯 ยิงโดนเป้าหมายแล้ว! สั่งเป้าให้หมุนและบวกคะแนน!");
            target.OnHit(); // เรียกฟังก์ชันในเป้าหมายให้ทำงาน
        }

        // 3. ไม่ว่าจะชนเป้าหมาย ชนพื้น หรือชนกำแพง ให้ทำลายกระสุนตัวเองทิ้งทันที
        Destroy(gameObject);
    }
}