using UnityEngine;
using System.Collections; 

public class Zombie : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    public float zombiHizi = 3.5f; 

    [Header("Zombi Can Ayarları")]
    public float maxCan = 100f;
    private float guncelCan;

    [Header("Zombi Saldırı Ayarları")]
    public float saldiriGucu = 20f;
    public float saldiriHizi = 1.5f; 
    public float saldiriMesafesi = 2.5f;
    
    
    public float hasarGecikmesi = 0.5f; 
    
    private float sonSaldiriZamani = 0f;
    private bool oluMu = false;

    private UnityEngine.AI.NavMeshAgent agent;
    private Transform oyuncu;
    private Animator anim;
    
    private PlayerHealth oyuncuCanSistemi; 

    void Start()
    {
        guncelCan = maxCan; 

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();

        agent.speed = zombiHizi;
        agent.stoppingDistance = saldiriMesafesi - 0.5f; 

        oyuncuCanSistemi = FindObjectOfType<PlayerHealth>();

        if (oyuncuCanSistemi != null)
        {
            oyuncu = oyuncuCanSistemi.transform;
        }
    }

    void Update()
    {
         if (Time.timeScale == 0f) return;
         
        if (oluMu || oyuncu == null) return; 

        agent.SetDestination(oyuncu.position);
        
        float mesafe = Vector3.Distance(transform.position, oyuncu.position);

        if (mesafe <= saldiriMesafesi)
        {
            anim.SetBool("isRunning", false); 
            
            if (Time.time >= sonSaldiriZamani)
            {
                
                sonSaldiriZamani = Time.time + saldiriHizi;
                anim.SetTrigger("Attack"); 
                
                
                StartCoroutine(GecikmeliHasarVer());
            }
        }
        else
        {
            anim.SetBool("isRunning", true);
        }
    }

    
    IEnumerator GecikmeliHasarVer()
    {
        
        yield return new WaitForSeconds(hasarGecikmesi);

        
        if (!oluMu && oyuncuCanSistemi != null)
        {
            
            float mesafe = Vector3.Distance(transform.position, oyuncu.position);
            if (mesafe <= saldiriMesafesi + 0.5f) 
            {
                oyuncuCanSistemi.HasarAl(saldiriGucu); 
            }
        }
    }

    public void TakeDamage(float hasarMiktari)
    {
        if (oluMu) return; 

        guncelCan -= hasarMiktari;

        if (guncelCan <= 0)
        {
            ZombiOlum();
        }
    }

    public void ZombiOlum()
    {
        oluMu = true;
        
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        agent.enabled = false; 
        
        Collider zombiCollider = GetComponent<Collider>();
        if(zombiCollider != null) zombiCollider.enabled = false; 
        
        anim.SetTrigger("Die"); 

        GameManager.hayattakiZombiSayisi--;

        Destroy(gameObject, 5f);
    }
}