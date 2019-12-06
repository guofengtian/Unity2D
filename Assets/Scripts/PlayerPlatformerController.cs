using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPlatformerController : PhysicsObject
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    public Text countText;
    public Text winText;
    public Text timeText;
    public Text boxText;
    private int count;
    private int boxCount;
    public float timer;
    public bool isSmiling;
    public AudioSource jumpAudio;
    public AudioSource crystalAudio;
    public AudioSource boxAudio;
    public AudioSource switchAudio;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Use this for initialization
    void Awake()
    {
        isSmiling = false;
        count = 0;
        boxCount = 0;
        countText.text = "Crystal No You Have:" + count.ToString();
        boxText.text = "Crystal No In Box:" + boxCount.ToString();
        winText.text = "";
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        
        if (collision.gameObject.tag.Equals("Crystal"))
        {
            crystalAudio.Play();
            Destroy(collision.gameObject);
            count = count + 1;
            countText.text = "Crystal No You Have:" + count.ToString();
        }
        if (collision.gameObject.tag.Equals("BigCrystal"))
        {
            crystalAudio.Play();
            Destroy(collision.gameObject);
            count = count + 5;
            countText.text = "Crystal No You Have:" + count.ToString();
        }
        if (collision.gameObject.tag.Equals("Finish"))
        {
            Destroy(gameObject);
            winText.text = "WASTED";
        }
        if (collision.gameObject.tag.Equals("LeverUp"))
        {
            switchAudio.Play();
            collision.gameObject.SetActive(false);
            var sphere = GameObject.FindWithTag("RisePlatform");
            string message = "true";
            sphere.SendMessage("RiseMethod",message);
            var sphere2 = GameObject.FindWithTag("LeverDown");
            sphere2.SendMessage("ShowMethod", message);
        }
        if (collision.gameObject.tag.Equals("Box"))
        {
            if(count > 0)
            {
                boxAudio.Play();
            }
            boxCount += count;
            boxText.text = "Crystal No In Box:" + boxCount.ToString();
            count = 0;
            countText.text = "Crystal No You Have:" + count.ToString();

        }
        if (boxCount == 9)
        {
            
            var sphere = GameObject.FindWithTag("PortalOn");
            string message = "true";
            sphere.SendMessage("ShowMethod", message);
            if (collision.gameObject.tag.Equals("PortalOn"))
            {
                winText.text = "YOU WIN!!!";
                Destroy(gameObject);
            }

        }
        

    }



    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        timer -= Time.deltaTime;
        timeText.text = timer.ToString("00");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumpAudio.Play();
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }


        if (timer < 0)
        {
            Destroy(gameObject);
            winText.text = "WASTED";
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            isSmiling = true;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            isSmiling = false;
        }





        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("grounded", grounded);
        animator.SetBool("smiled", isSmiling);
        animator.SetBool("isClimbing", isClimbing);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed * Time.deltaTime;
    }

}