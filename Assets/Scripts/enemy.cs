using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    //属性值
    public float moveSpeed = 3.0f;          //坦克移动速度
    private Vector3 bulletEularAngles; // 子弹需要转的度数
    private float horizontalMove;           //水平移动
    private float verticalMove= -1;     //垂直移动


    //引用
    private SpriteRenderer sr;
    public Sprite[] tankSprite;//上，右，下，左
    public GameObject bulletPerfab;//子弹的引用
    public GameObject explosionPrefabs; //爆炸的预制体

    //计时器
    private float timeVal;          //坦克发射子弹的cd时间
    private float timeValChangeDirection = 2.0f; // 坦克转向时间


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();//获取组件
    }

    // Update is called once per frame

    void Update()
    {
        if ( timeVal >= 1.5 )
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        TankMove();
    }

    /// <summary>
    /// 坦克的死亡方法
    /// </summary>
    public void TankDie()
    {
        //产生爆炸特效
        Instantiate( explosionPrefabs , transform.position , transform.rotation );
        //每次死亡得分++
        playerMangger.Instance.Scrore += 1;
        Debug.Log( "您的的得分"+playerMangger.Instance.Scrore );
        //销毁玩家
        Destroy( gameObject );
    }

    private void OnCollisionEnter2D( Collision2D collsion )
    {
        if ( collsion.gameObject.tag == "Enemy" )
        {
            timeValChangeDirection = 2.0f;
        }
    }

    /// <summary>
    /// 坦克攻击方法
    /// </summary>
    private void Attack()
    {
        //实例化生成一个子弹
        Instantiate( bulletPerfab , transform.position , Quaternion.Euler( transform.eulerAngles + bulletEularAngles ) );
        timeVal = 0;
    }
    /// <summary>
    /// 坦克移动
    /// </summary>
    private void TankMove()
    {
        if ( timeValChangeDirection >= 1.5 ) // 如果时间超过3.5秒则转向
        {
            int num = Random.Range( 0 , 8 );//随机产生数值，让敌人转向，并且保持敌人向下的转向几率更大
            if ( num >= 5 )
            {
                horizontalMove = 0;
                verticalMove = -1;
            }
            else if ( num == 0 )
            {
                horizontalMove = 0;
                verticalMove = 1;
            }
            else if ( num > 0 && num <= 2 )
            {
                horizontalMove = -1;
                verticalMove = 0;
            }
            else if ( num > 2 && num <= 4 )
            {
                horizontalMove = 1;
                verticalMove = 0;
            }
            timeValChangeDirection = 0;
        }
        else
        {
            timeValChangeDirection += Time.fixedDeltaTime;
        }


      // horizontalMove = Input.GetAxisRaw( "Horizontal" );
        transform.Translate( Vector3.right * horizontalMove * moveSpeed * Time.fixedDeltaTime , Space.World );
        if ( horizontalMove < 0 )
        {
            sr.sprite = tankSprite[3];
            bulletEularAngles = new Vector3( 0 , 0 , 90 );  //向左旋转
        }
        else if ( horizontalMove > 0 )
        {
            sr.sprite = tankSprite[1];
            bulletEularAngles = new Vector3( 0 , 0 , -90 );//向右旋转
        }
        if(horizontalMove!=0)        //优先级判断，如果水平方向有输入，则只执行水平位移，否则才执行垂直位移
        {
            return ;
        }
        //verticalMove = Input.GetAxisRaw( "Vertical" );
        transform.Translate( Vector3.up * verticalMove * moveSpeed * Time.fixedDeltaTime , Space.World );
        if ( verticalMove < 0 )
        {
            sr.sprite = tankSprite[2];
            bulletEularAngles = new Vector3( 0 , 0 , -180 );//向下旋转
        }
        else if ( verticalMove > 0 )
        {
            sr.sprite = tankSprite[0];
            bulletEularAngles = new Vector3( 0 , 0 , 0 );//向上旋转
        }
    }
}
