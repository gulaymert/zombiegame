using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    [Header("Ayarlar")]
    public GameObject zombiPrefab; 
    public Transform[] spawnNoktalari; 
    
    [Header("Arayüz (UI) Yazıları")]
    public TextMeshProUGUI roundYazisi; 
    public TextMeshProUGUI canYazisi;   

    [Header("Menü Ekranları (Paneller)")]
    public GameObject oyunIciUIPanel;
    public GameObject mainMenuPanel;
    public GameObject controlsPanel;
    public GameObject gameOverPanel;

    [Header("Raunt Bilgileri")]
    public int guncelRound = 1;
    public int baslangicZombiSayisi = 5; 

    [Header("Oyuncu Kontrol Ayarları")]
    
    public MonoBehaviour oyuncuYurumeKodu; 
    public MonoBehaviour oyuncuKameraKodu;
    
    private int buRoundKacZombiDogacak;
    private int doganZombiSayisi = 0;
    public static int hayattakiZombiSayisi = 0; 

    private bool oyunDurduMu = true; 
    private bool gameOverOlduMu = false;

    void Start()
    {
        buRoundKacZombiDogacak = baslangicZombiSayisi;
        hayattakiZombiSayisi = 0; 
        
        
        AnaMenuyuGoster();
    }

   void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Tab) && !gameOverOlduMu)
        {
            if (oyunDurduMu) OyunaDevamEt();
            else AnaMenuyuGoster();
        }

        
        if (!oyunDurduMu && !gameOverOlduMu)
        {
            if (doganZombiSayisi >= buRoundKacZombiDogacak && hayattakiZombiSayisi <= 0)
            {
                guncelRound++;
                buRoundKacZombiDogacak += 3; 
                doganZombiSayisi = 0;
                StartCoroutine(YeniRoundBaslat());
            }
        }
    }
    
    

    public void AnaMenuyuGoster()
    {
        oyunDurduMu = true;
        Time.timeScale = 0f; 

        if (oyuncuYurumeKodu != null) oyuncuYurumeKodu.enabled = false;
        if (oyuncuKameraKodu != null) oyuncuKameraKodu.enabled = false;

        mainMenuPanel.SetActive(true);
        controlsPanel.SetActive(false); 
        oyunIciUIPanel.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

 public void OyunaDevamEt() 
    {
        if (gameOverOlduMu) return;

        oyunDurduMu = false;
        Time.timeScale = 1f; 

        
        if (oyuncuYurumeKodu != null) oyuncuYurumeKodu.enabled = true;
        if (oyuncuKameraKodu != null) oyuncuKameraKodu.enabled = true;

        mainMenuPanel.SetActive(false);
        controlsPanel.SetActive(false);
        oyunIciUIPanel.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (guncelRound == 1 && doganZombiSayisi == 0)
        {
            StartCoroutine(YeniRoundBaslat());
        }
    }

    public void KontrolleriGoster() 
    {
        mainMenuPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void KontrollerdenCik() 
    {
        controlsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void OyundanCik() 
    {
        Debug.Log("Oyundan Çıkıldı!");
        Application.Quit(); 
    }

    public void YenidenBaslat() 
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    

    IEnumerator YeniRoundBaslat()
    {
        if(roundYazisi != null) roundYazisi.text = "ROUND: " + guncelRound;
        yield return new WaitForSeconds(4f);

        while (doganZombiSayisi < buRoundKacZombiDogacak)
        {
            int rastgeleIndex = Random.Range(0, spawnNoktalari.Length);
            Transform secilenNokta = spawnNoktalari[rastgeleIndex];

            Instantiate(zombiPrefab, secilenNokta.position, secilenNokta.rotation);
            doganZombiSayisi++;
            hayattakiZombiSayisi++;

            yield return new WaitForSeconds(2f);
        }
    }

    public void CanGuncelle(float guncelCan)
    {
        if(canYazisi != null) canYazisi.text = "HEALTH: " + guncelCan;
        
        if (guncelCan <= 0 && !gameOverOlduMu)
        {
            gameOverOlduMu = true;
            Time.timeScale = 0f; 
            
            
            oyunIciUIPanel.SetActive(false);
            gameOverPanel.SetActive(true); 

            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}