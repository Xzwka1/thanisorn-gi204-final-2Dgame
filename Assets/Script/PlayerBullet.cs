using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [Header("ตั้งค่ากระสุน")]
    public float lifeTime = 3f; 

    void Start()
    {
        
        Destroy(gameObject, lifeTime);
    }

    
    void OnTriggerEnter2D(Collider2D hitInfo)
    {


        BeeTarget bee = hitInfo.GetComponent<BeeTarget>();
        if (bee != null)
        {
            bee.OnHit();
        }

        Destroy(gameObject);
        
        FishTarget fish = hitInfo.GetComponent<FishTarget>();
        if (fish != null)
        {
            fish.OnHit();
        }

       
        FrogTarget frog = hitInfo.GetComponent<FrogTarget>();
        if (frog != null)
        {
            frog.OnHit();
        }

        
        MouseTarget mouse = hitInfo.GetComponent<MouseTarget>();
        if (mouse != null)
        {
            mouse.OnHit();
        }

       
        Destroy(gameObject);
    }
}