using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    float speed = 2f, maxspeed=3f,jumpforce=50f,health=100f;
    Rigidbody2D rb2d;
    bool facingRight = true,grounded=false, jump = false,biggy=false;
    Animator anim;
    public Text h,t1;
    

    // Use this for initialization
    void Start () {

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	
	}

    void Update()
    {
        if (health > 0)
        {
            h.text = "Health " + Mathf.Round(health);

            if (Input.GetButtonDown("Jump") && grounded)
            {
                // Debug.Log("hi1");
                jump = true;
            }
            if (Input.GetButtonDown("Fire1"))
            {
                anim.SetTrigger("punch");
            }

            if (Input.GetButtonDown("Fire3"))
            {
                anim.SetBool("biggy", true);
            }
            if (Input.GetButtonDown("Fire2"))
            {
                anim.SetBool("biggy", false);
            }

        }
        else
        {
            t1.text = "GAME OVER";
        }
        //
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (health > 0)
        {
            float h = Input.GetAxis("Horizontal");
            //Debug.Log("" + rb2d.velocity.x);
            anim.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));

            if (Mathf.Abs(rb2d.velocity.x) < maxspeed)
            {
                rb2d.AddForce(Vector2.right * speed * Input.GetAxis("Horizontal"));
            }
            if (facingRight && Mathf.Abs(rb2d.velocity.x) > 1)
            {
                rb2d.AddForce(Vector2.right * speed / 8 * (-1));
            }
            else
                if (!facingRight && Mathf.Abs(rb2d.velocity.x) > 1)
            {
                rb2d.AddForce(Vector2.right * speed / 8);
            }

            if (jump)
            {
                // Debug.Log("hi2");
                anim.SetBool("grounded", false);

                rb2d.AddForce(new Vector2(0, jumpforce));
                jump = false;
                grounded = false;
            }



            if (h > 0 && !facingRight)
            {
                Flip();
            }
            else
            {
                if (h < 0 && facingRight)
                {
                    Flip();
                }
            }
        }
	}

    void OnTriggerStay2D(Collider2D o)
    {
        if (o.CompareTag("enemy"))
        {
            anim.SetTrigger("minihurt");
            health -= 15*Time.deltaTime;
        }
        else
        {
            if (o.CompareTag("Finish"))
            {
                t1.text = "YOU WIN";
            }
            else
            {
                grounded = true;
                anim.SetBool("grounded", true);
            }
        }
        // Debug.Log("touch");
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
