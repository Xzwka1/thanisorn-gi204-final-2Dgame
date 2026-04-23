using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("สเตตัสศัตรู")]
    public int health = 50;
    public float speed = 2f;

    [Header("ระบบตรวจจับขอบ (Edge Detection)")]
    public float rayDistance = 1f;    // ความยาวของลำแสงที่ยิงลงพื้น
    public Transform groundCheck;     // จุดที่ใช้ยิงลำแสง (ควรอยู่หน้าเท้าศัตรู)
    public LayerMask groundLayer;     // เลือก Layer ที่เป็นพื้น (เช่น Ground)
    private bool movingRight = true;

    [Header("ระบบตรวจจับและยิง")]
    public float detectRange = 5f;
    public GameObject enemyBulletPrefab;
    public Transform firePoint;
    public float fireRate = 1.5f;
    private float nextFireTime = 0f;

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectRange)
        {
            ShootPlayer();
        }
        else
        {
            PatrolByEdge();
        }
    }

    void PatrolByEdge()
    {
        // 1. สั่งให้เดินไปตามทิศทางปัจจุบัน
        rb.linearVelocity = new Vector2(movingRight ? speed : -speed, rb.linearVelocity.y);

        // 2. ยิง Raycast ลงพื้นเพื่อเช็คขอบเหว
        // ยิงจากตำแหน่ง groundCheck ลงข้างล่าง (Vector2.down)
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, rayDistance, groundLayer);

        // วาดลำแสงในหน้า Scene เพื่อให้เราเห็น (Gizmos)
        Debug.DrawRay(groundCheck.position, Vector2.down * rayDistance, Color.red);

        // 3. ถ้า Raycast ไม่ชนพื้น (groundInfo.collider == null) แปลว่าเจอขอบเหว
        if (groundInfo.collider == null)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        // กลับด้านตัวละคร (Scale แกน X)
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void ShootPlayer()
    {
        // หยุดเดินเมื่อเจอผู้เล่น
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

        // หันหน้าหาผู้เล่น
        if ((player.position.x > transform.position.x && !movingRight) ||
            (player.position.x < transform.position.x && movingRight))
        {
            Flip();
        }

        if (Time.time >= nextFireTime)
        {
            Vector2 shootDir = (player.position - firePoint.position).normalized;
            GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().AddForce(shootDir * 10f, ForceMode2D.Impulse);
            nextFireTime = Time.time + fireRate;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Destroy(gameObject);
    }
}