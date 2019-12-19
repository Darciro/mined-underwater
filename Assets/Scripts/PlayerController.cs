using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private TrailRenderer playerTrail;
    public float speed;
    public float speedUpDown;

    private Vector3 touchPosition;
    private Vector3 direction;
    private Vector2 moveAmount;

    public int health;
    public int maxHealth;
    public int eggs;

    public GameObject moveEffect;
    public Animator playerAnim;

    public Image playerHealthBar;
    public Image eggsCollectedBar;
    
    public Transform target;

    private SceneTransitions sceneTransitions;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTrail = GetComponent<TrailRenderer>();
        playerAnim = gameObject.GetComponent<Animator>();
        health = 100;
        maxHealth = 100;
        sceneTransitions = FindObjectOfType<SceneTransitions>();
        eggs = 0;
    }

    private void Update()
    {
        playerAnim.SetBool("swimming", true);
    }

    private void FixedUpdate()
    {
        #if UNITY_ANDROID
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            direction = (touchPosition - transform.position);
            rb.velocity = new Vector2(direction.x, direction.y) * speedUpDown;
            
            playerAnim.SetFloat("swimmingUpOrDown", direction.y);

            if (touch.phase == TouchPhase.Ended)
            {
                rb.velocity = Vector2.zero;
                playerAnim.SetFloat("swimmingUpOrDown", 0f);
            }
                
        }

        #endif
        
        /*Vector2 moveInput = new Vector2(0, Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;
        playerAnim.SetBool("swimming", true);
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
        playerAnim.SetFloat("swimmingUpOrDown", moveAmount.y);*/
        
    }
    
    public void TakeDamage(int amount)
    {
        // Instantiate(hurtSound, transform.position, Quaternion.identity);
        health -= amount;
        UpdateHealthUI(health);
        // hurtAnim.SetTrigger("hurt");
        if (health <= 0)
        {
            // spawner.SetActive(false);
            // restartDisplay.SetActive(true);
            
            Destroy(this.gameObject);
            sceneTransitions.LoadScene("Loose");
            // sceneTransitions.LoadScene("Lose");
        }
    }
    
    void UpdateHealthUI(float currentHealth) {
        /*Debug.Log( maxHealth / 100 );
        Debug.Log( currentHealth / 100 );
        Debug.Log( (maxHealth / 100) - (currentHealth / 100 ) );

        float subtract = (float) maxHealth - currentHealth;
        Debug.Log( "subtract " + subtract );
        
        playerHealthBar.fillAmount -= subtract / 1.0f * Time.deltaTime;*/
        playerHealthBar.fillAmount = currentHealth / 100;

    }
    
    public void GetEgg(int amount)
    {
        eggs += amount;
        
        Debug.Log("Eggs now: " + eggs);
        UpdateEgghUI(eggs);
        if (eggs >= 10)
        {
            // WIN
        }
    }
    
    void UpdateEgghUI(float currentHealth) {
        Debug.Log("Eggs updating: " + currentHealth);
        eggsCollectedBar.fillAmount = currentHealth / 10;

    }
    
}