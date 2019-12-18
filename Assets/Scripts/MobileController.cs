using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator playerAnim;

    public Transform player;
    public float speed = 15.0f;
    // public GameObject bulletPrefab;

    /*public Transform circle;
    public Transform outerCircle;*/

    private Vector2 startingPoint;
    private int leftTouch = 99;

    private Text debugText;
    
    void Start()
    {
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        playerAnim = GameObject.Find("Player").GetComponent<Animator>();
        debugText = GameObject.Find("Debug").GetComponent<Text>();
    }


    // Update is called once per frame
    void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            Vector2 touchPos = getTouchPosition(t.position); // * -1 for perspective cameras
            if (t.phase == TouchPhase.Began)
            {
                if (t.position.x > Screen.width / 2)
                {
                    shootBullet();
                }
                else
                {
                    leftTouch = t.fingerId;
                    startingPoint = touchPos;
                }
            }
            else if (t.phase == TouchPhase.Moved && leftTouch == t.fingerId)
            {
                Vector2 offset = touchPos - startingPoint;
                Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);

                moveCharacter(direction);

                /*circle.transform.position = new Vector2(outerCircle.transform.position.x + direction.x,
                    outerCircle.transform.position.y + direction.y);*/
            }
            else if (t.phase == TouchPhase.Ended && leftTouch == t.fingerId)
            {
                leftTouch = 99;
                /*circle.transform.position =
                    new Vector2(outerCircle.transform.position.x, outerCircle.transform.position.y);*/
            }

            ++i;
        }
    }

    Vector2 getTouchPosition(Vector2 touchPosition)
    {
        return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
    }

    void moveCharacter(Vector2 direction)
    {
        player.Translate(new Vector2( 0, direction.y ) * speed * Time.deltaTime );
        debugText.text = direction.y.ToString() + " leftTouch: " + leftTouch;
        
        // rb.MovePosition(rb.position + direction * Time.deltaTime);
        // float swimmingUpOrDown = Mathf.Abs( direction.y);
        playerAnim.SetFloat("swimmingUpOrDown", direction.y);
    }

    void shootBullet()
    {
        Debug.Log("Shooting!");
        /*GameObject b = Instantiate(bulletPrefab) as GameObject;
        b.transform.position = player.transform.position;*/
    }
}