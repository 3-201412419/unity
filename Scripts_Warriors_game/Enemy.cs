using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Rigidbody2D rigid;
    CapsuleCollider2D cap;
    public Sprite[] sprites;
    public int nextMove;
    bool death = false;
    Animator animator;
    SpriteRenderer spriteRenderer;
    public string enemyname;
    public int health;
    public int enemydamage;
    

 



    public void OnEnable()
    {
        switch(enemyname)
        {
            case "G":
                health = 9;
                enemydamage = 1;
                
                break;
        }
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        cap = GetComponent<CapsuleCollider2D>();
        Invoke("Think", 3); 

    }


    
    void FixedUpdate()
    {
        if (death == false)
        {
            rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
            Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y);
            Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D raycast = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));


            if (raycast.collider == null)
            {
                Turn();
            }

           
        }
            else if(death == true)
        {
            
            return;
        }

    }

    void Think()
    {
        if (death == false)
        {

            nextMove = Random.Range(-1, 2);

            animator.SetInteger("Isrun", nextMove);

            if (nextMove == -1)
            {

                spriteRenderer.flipX = true;
            }
            else if (nextMove == 1)

                spriteRenderer.flipX = false;

            float time = Random.Range(2f, 5f);

            Invoke("Think", time);
        }
    }

    void Turn()
    {

        if (death == false)
        {


            nextMove = nextMove * (-1);
            spriteRenderer.flipX = nextMove == 1;

            if (nextMove == -1)
            {
                spriteRenderer.flipX = true;
            }
            else if (nextMove == 1)

                spriteRenderer.flipX = false;


            CancelInvoke();
            Invoke("Think", 2);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Attack")
        {
            OnDamaged(collision.transform.position);
          
        }
    }

    
    void OnDamaged(Vector2 targetpos)
    {
        
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        int dirc = transform.position.x - targetpos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 1.5f, ForceMode2D.Impulse);
        animator.SetTrigger("Ishit");
        Invoke("OffDamaged", 1);
        

    }

    void OffDamaged()
    {
       
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }


    public void TakeDamage(int damage)
    {
        if(health <= 0)
        {
            return;
        }

        health -= damage;

        if(health == 0)
        {
            death = true;
            animator.SetTrigger("IsDead");
            Invoke("Deadanim", 0.3f);
            

        }
        
    }
     void Deadanim()
    {
        gameObject.tag = "Dead";
        gameObject.layer = 12;
       animator.enabled = false;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        spriteRenderer.sprite = sprites[0];
    }


}