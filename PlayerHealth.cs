using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maksimumCan = 100f;
    private float guncelCan;

    void Start()
    {
        guncelCan = maksimumCan;
        
        
        FindObjectOfType<GameManager>().CanGuncelle(guncelCan);
    }

    public void HasarAl(float miktar)
    {
        guncelCan -= miktar;
        Debug.Log("Oyuncu Hasar Aldı! Kalan Can: " + guncelCan);

        
        
        FindObjectOfType<GameManager>().CanGuncelle(guncelCan);
    }
}