using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private BoxCollider2D _collider;
    
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

    private SceneTransitions sceneTransitions;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
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

        if (health <= 0)
        {
            return;
        }
        
        // For tests purposes 
        #if UNITY_EDITOR_WIN
        Vector2 moveInput = new Vector2(0, Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;
        playerAnim.SetBool("swimming", true);
        _rb.MovePosition(_rb.position + moveAmount * Time.fixedDeltaTime);
        playerAnim.SetFloat("swimmingUpOrDown", moveAmount.y);
        #endif
        // For tests purposes 

        #if UNITY_ANDROID
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if(touch.position.x > Screen.width / 2){
                // Right side
            }else{
                // Left side
                
                touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;
                direction = (touchPosition - transform.position);
                _rb.velocity = new Vector2(direction.x, direction.y) * speedUpDown;
            
                playerAnim.SetFloat("swimmingUpOrDown", direction.y);

                if (touch.phase == TouchPhase.Ended)
                {
                    _rb.velocity = Vector2.zero;
                    playerAnim.SetFloat("swimmingUpOrDown", 0f);
                }
                
            }

        }

        #endif

    }
    
    public void TakeDamage(int amount)
    {
        health -= amount;
        UpdateHealthUI(health);
        playerAnim.SetTrigger("hurt");
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        playerAnim.SetTrigger("dead");
        _rb.isKinematic = true;
        _collider.enabled = false;
        StartCoroutine(ShowLooseScreen());
    }
    
    IEnumerator ShowLooseScreen() {
        yield return new WaitForSeconds(3f);
        
        Destroy(this.gameObject);
        sceneTransitions.LoadScene("Loose");
    }
    
    void UpdateHealthUI(float currentHealth) {
        playerHealthBar.fillAmount = currentHealth / 100;

    }
    
    public void GetEgg(int amount)
    {
        eggs += amount;
        UpdateEgghUI(eggs);
        if (eggs >= 10)
        {
            sceneTransitions.LoadScene("Won");
        }
    }
    
    void UpdateEgghUI(float currentHealth) {
        eggsCollectedBar.fillAmount = currentHealth / 10;

    }
    
}