using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damage = 25;

    // ฟังก์ชันนี้ทำงานเมื่อกระสุน (Is Trigger) ไปชนกับอะไรสักอย่าง
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // เช็คว่าสิ่งที่ชนมีสคริปต์ EnemyAI แปะอยู่ไหม?
        EnemyAI enemy = hitInfo.GetComponent<EnemyAI>();

        if (enemy != null)
        {
            // ถ้าใช่ ให้เรียกฟังก์ชันรับดาเมจของศัตรู
            enemy.TakeDamage(damage);
        }

        // ชนปุ๊บ ทำลายกระสุนตัวเองทิ้งทันที
        Destroy(gameObject);
    }
}