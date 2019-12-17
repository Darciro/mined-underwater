using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private Vector2 moveAmount;

    private Vector2 targetPos;
    private Vector3 fp; //First touch position
    private Vector3 lp; //Last touch position
    private float dragDistance; //minimum distance for a swipe to be registered

    public int health;
    public int maxHealth;

    public GameObject moveEffect;
    public GameObject moveEffectSoundUp;
    public GameObject moveEffectSoundDown;
    public Animator playerAnim;
    public Text healthDisplay;

    public GameObject spawner;
    public GameObject restartDisplay;
    public Image playerHealthBar;
    
    public Transform target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = gameObject.GetComponent<Animator>();
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
        health = 100;
        maxHealth = 100;
        // playerHealthBar = GameObject.Find("ProgressBarHeart");
    }

    private void Update()
    {
        // playerHealthBar.fillAmount -= 1.0f / 10f * Time.deltaTime;

        
        Vector2 moveInput = new Vector2(0, Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;

        playerAnim.SetBool("swimming", true);
        
        /*if (moveInput == Vector2.zero)
        {
            playerAnim.SetBool("swimming", false);
        }*/
        
        
        /*if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Cima");
            playerAnim.SetBool("swimmingUp", true);
            playerAnim.SetBool("swimmingDown", false);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Baixo");
            playerAnim.SetBool("swimmingUp", false);
            playerAnim.SetBool("swimmingDown", true);
        }*/


        #if UNITY_ANDROID
        // URL https://forum.unity.com/threads/simple-swipe-and-tap-mobile-input.376160/

        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list
 
                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            Debug.Log("Right Swipe");
                        }
                        else
                        {   //Left swipe
                            Debug.Log("Left Swipe");
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");

                            Instantiate(moveEffect, transform.position, Quaternion.identity);
                            targetPos = new Vector2(transform.position.x, transform.position.y + increment);
                            playerAnim.Play("PlayerMovingUp");
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");

                            Instantiate(moveEffect, transform.position, Quaternion.identity);
                            targetPos = new Vector2(transform.position.x, transform.position.y - increment);
                            playerAnim.Play("PlayerMovingDown");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
        #endif
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
        float swimmingUpOrDown = Mathf.Abs( moveAmount.y);
        // Debug.Log(moveAmount.y);
        playerAnim.SetFloat("swimmingUpOrDown", swimmingUpOrDown);

        /*Debug.Log(Input.GetAxisRaw("Vertical"));
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            playerAnim.SetTrigger("swimmingUp");
        } else if (Input.GetAxisRaw("Vertical") < 0)
        {
            playerAnim.SetBool("swimmingDown", true);
        }*/

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
    
}