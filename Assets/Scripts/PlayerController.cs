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
    public float increment;
    public float speed;
    private Vector2 moveAmount;

    private Vector2 targetPos;
    private Vector3 fp; //First touch position
    private Vector3 lp; //Last touch position
    private float dragDistance; //minimum distance for a swipe to be registered

    public int health;
    public int maxHealth;
    public int eggs;

    public GameObject moveEffect;
    public GameObject moveEffectSoundUp;
    public GameObject moveEffectSoundDown;
    public Animator playerAnim;
    public Text healthDisplay;

    public GameObject spawner;
    public GameObject restartDisplay;
    public Image playerHealthBar;
    public Image eggsCollectedBar;
    
    public Transform target;

    private SceneTransitions sceneTransitions;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = gameObject.GetComponent<Animator>();
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
        health = 100;
        maxHealth = 100;
        sceneTransitions = FindObjectOfType<SceneTransitions>();
        eggs = 0;
        // playerHealthBar = GameObject.Find("ProgressBarHeart");
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(0, Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;
        playerAnim.SetBool("swimming", true);

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
        playerAnim.SetFloat("swimmingUpOrDown", moveAmount.y);
        
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