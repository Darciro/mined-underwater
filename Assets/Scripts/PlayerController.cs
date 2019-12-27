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
    private Vector3 _touchPosition;
    private Vector3 _direction;
    private Vector2 _moveAmount;
    
    public float speed;
    public float speedUpDown;
    public int health;
    public int eggs;
    public GameObject moveEffect;
    public Animator playerAnim;
    public Image playerHealthBar;
    public Image eggsCollectedBar;
    public GameObject damageTaken;
    public GameObject eggCollectedPlus;

    private SceneTransitions sceneTransitions;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        playerAnim = gameObject.GetComponent<Animator>();
        sceneTransitions = FindObjectOfType<SceneTransitions>();
        health = 100;
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
        _moveAmount = moveInput.normalized * speed;
        playerAnim.SetBool("swimming", true);
        _rb.MovePosition(_rb.position + _moveAmount * Time.fixedDeltaTime);
        playerAnim.SetFloat("swimmingUpOrDown", _moveAmount.y);
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
                
                _touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                _touchPosition.z = 0;
                _direction = (_touchPosition - transform.position);
                _rb.velocity = new Vector2(_direction.x, _direction.y) * speedUpDown;
            
                playerAnim.SetFloat("swimmingUpOrDown", _direction.y);

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
        var go= Instantiate(damageTaken, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = amount.ToString();
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
        var curHealth = playerHealthBar.transform.parent.Find("CurrentHealth");
        curHealth.GetComponent<Text>().text = currentHealth.ToString();
        
        playerHealthBar.fillAmount = currentHealth / 100;
        

    }
    
    public void GetEgg(int amount)
    {
        eggs += amount;
        UpdateEgghUI(eggs);
        Instantiate(eggCollectedPlus, transform.position, Quaternion.identity, transform);
        if (eggs >= 10)
        {
            sceneTransitions.LoadScene("Won");
        }
    }
    
    void UpdateEgghUI(float eggsCurCollected) {
        var eggsCollected = eggsCollectedBar.transform.parent.Find("CurrentCollectedEggs");
        eggsCollected.GetComponent<Text>().text = eggsCurCollected.ToString();
        
        eggsCollectedBar.fillAmount = eggsCurCollected / 10;

    }
    
}