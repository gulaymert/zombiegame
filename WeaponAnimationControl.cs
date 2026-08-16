using UnityEngine;

public class WeaponAnimationControl : MonoBehaviour
{
    private Animator anim;
    
    [Header("Hız Ayarları")]
    public float baseAnimationSpeed = 1f;   
    public float sprintAnimationSpeed = 2f; 

    

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");

        
        bool isMoving = Mathf.Abs(yatay) > 0.1f || Mathf.Abs(dikey) > 0.1f;

        
        anim.SetBool("isWalking", isMoving);

        if (isMoving)
        {
            
            bool isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            
            float targetAnimSpeed = isSprinting ? sprintAnimationSpeed : baseAnimationSpeed;

           
            anim.SetFloat("movementSpeed", targetAnimSpeed);
        }
        else
        {
            
            anim.SetFloat("movementSpeed", baseAnimationSpeed);
        }
    }
}