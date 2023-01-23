using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    Rigidbody2D rigid;
    public float playerSpeed;
    public float jumpPower;
    public float jumpTime, TimeLimit;
    public bool isJumped;
    public int direction;

    public Transform wallChk;
    public float wallChkDistance;
    public LayerMask W_Layer;
    bool isWall;
    public float WallBouncePower;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        isJumped = false;
        TimeLimit = 0.4f;
        jumpTime = 0.1f;
        playerSpeed = 7.0f;
    }

    void Start()
    {
        
    }

    void Update()
    {
        PlayerMove();
        Jump();

        isWall = Physics2D.Raycast(wallChk.position, Vector2.right * direction, wallChkDistance, W_Layer);
        if (isWall)
        {
            rigid.velocity = new Vector2(-direction * WallBouncePower, 0.9f * WallBouncePower);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.blue;
        Gizmos.DrawRay(wallChk.position, Vector2.right * direction * wallChkDistance);
    }

    
    
    void PlayerMove()
    {
        if(!isJumped && !Input.GetKey(KeyCode.Space))
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector3(playerSpeed * Time.deltaTime, 0, 0));
                direction = 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector3(-playerSpeed * Time.deltaTime, 0, 0));
                direction = -1;
            }
        }

        if(isJumped)
        {
            transform.Translate(new Vector2(1.2f*direction * playerSpeed * Time.deltaTime, 0));
        }
        
    }
    void Jump()
    {
        //Space바를 누른 시간 저장
        if (Input.GetKey(KeyCode.Space) && !isJumped)
        {
            jumpTime += Time.deltaTime;
        }
        //점프구현
        if (Input.GetKeyUp(KeyCode.Space) && !isWall)
        {
            if (!isJumped)
            {
                Vector2 force = new Vector2(0, 0);
                force.y = jumpPower * (jumpTime > TimeLimit ? TimeLimit : jumpTime);

                rigid.AddForce(force, ForceMode2D.Impulse);//누른시간에 따라 점프 강도조절
                isJumped = true;
                jumpTime = 0.1f;

                if(Input.GetKey(KeyCode.A))
                {
                    direction = -1;
                }
                else if(Input.GetKey(KeyCode.D))
                {
                    direction = 1;
                }
                else
                {
                    direction = 0;
                } 
            }
        }
    }
    private void FixedUpdate()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground")
        {
            isJumped = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "FinishObj")
        {
            SceneManager.LoadScene("Ending Scene");
        }
    }
}
