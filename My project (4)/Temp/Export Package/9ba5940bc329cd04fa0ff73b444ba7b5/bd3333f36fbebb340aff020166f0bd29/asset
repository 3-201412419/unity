using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Warrior : MonoBehaviour
{
    public float speed;
    public int hp;
    public int damage;
    public GameObject Attackarea;
    public Sprite[] sprites;
    public Transform pos;
    public Vector2 boxSize;
    public GameObject Enemy;

    bool death = false;

   public string enemys;
    Animator anim;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    public float jumpPower;
    


    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;

    }


    void Update()
    {
        if (death == false)
        {
            move();
            attack();
            jump();

        }

        else if(death == true)

        { 

            return;
        }
        

    }



    void FixedUpdate()
    {
       
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Ground"));
      
        if (rigid.velocity.y < 0)
        { 
            if (rayHit.collider != null)
            { 
                if (rayHit.distance < 0.8f)
                    anim.SetBool("Jump", false); 
                   

            }
        }


    }

    void move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0f) * speed * Time.deltaTime;
        transform.position = curPos + nextPos;

        /*
        if (Input.GetButtonDown("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        */

        if (h > 0)
            transform.localScale = new Vector3(2.5f, 2.5f, 1);
        else if (h < 0)
            transform.localScale = new Vector3(-2.5f, 2.5f, 1);

        transform.Translate(new Vector3(h, 0, 0) * Time.deltaTime);


        

        if (h != 0)
        {
            anim.SetBool("Input", true);
        }
        else
        {
            anim.SetBool("Input", false);
        }

       

    }

    void attack()
    {
        if (Input.GetButton("Fire1") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.SetTrigger("Attack");
            Attackarea.SetActive(true);
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
            foreach(Collider2D collider in collider2Ds)
            {
                if(collider.tag == "Enemy")
                {
                    collider.GetComponent<Enemy>().TakeDamage(damage);
                }
            }

            

        }

        
        else if (!Input.GetButton("Fire1") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            Attackarea.SetActive(false);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

   
    void jump()
    {
        if (Input.GetButtonDown("Jump") && !anim.GetBool("Jump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("Jump", true);

           
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        

        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Trap")
        {
            
            enemys = collision.gameObject.name;
            OnDamaged(collision.transform.position);

            if(enemys == "Goblin")
            {
                hp -= 1;
                Dead();    
            }
        }

    }

    void OnDamaged(Vector2 targetpos)
    {
        gameObject.layer = 9;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        int dirc = transform.position.x - targetpos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 1.5f, ForceMode2D.Impulse);
        anim.SetTrigger("doDamaged");
        if (anim.GetBool("Jump"))
        {
            anim.SetBool("Jump", false);
        }
        Invoke("OffDamaged", 1.5f);
       
    }

    void OffDamaged()
    {
        if (death == false)
        {
            gameObject.layer = 8;
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
        else
        {
            return;
        }
    }


    void Dead()
    {
        if(hp <= 0)

        {
            death = true;
            anim.SetTrigger("Dead");
            Invoke("Deadanim", 0.8f);
        }
    }

    void Deadanim()
    { 
        gameObject.tag = "Dead";
        gameObject.layer = 12;
        anim.enabled = false;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        spriteRenderer.sprite = sprites[0];
    }
}