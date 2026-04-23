using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 10;

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // ตรวจจับว่ากระสุนโดน Player หรือไม่ (เช็คจาก Tag)
        if (hitInfo.CompareTag("Player"))
        {

            Player2DController player = hitInfo.GetComponent<Player2DController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            Debug.Log("อั๊ค! โดนยิงเข้าให้แล้ว! เลือดลด: " + damage);
            // อนาคตคุณสามารถเรียกโค้ดลดเลือด Player ตรงนี้ได้เลย 
            // เช่น hitInfo.GetComponent<PlayerHealth>().TakeDamage(damage);
        }

        // ชนแล้วกระสุนหายไป (ไม่ว่าชนกำแพงหรือคน)
        Destroy(gameObject);
    }
}