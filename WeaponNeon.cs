using UnityEngine;

public class WeaponNeon : MonoBehaviour
{
    public float speed = 15f; 
    public float lifeTime = 2f; 
    public float damage = 40f; 

    void Start()
    {
        
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    
    void OnTriggerEnter(Collider other)
    {
        
        Zombie zombie = other.GetComponent<Zombie>();
        
        if (zombie != null)
        {
            zombie.TakeDamage(damage); 
            
            Destroy(gameObject); 
        }
    }
}