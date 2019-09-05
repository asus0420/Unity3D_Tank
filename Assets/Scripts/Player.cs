using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    //属性值
    public float moveSpeed = 3.0f;          //坦克移动速度
    private Vector3 bulletEularAngles; // 子弹需要转的度数
    private float timeVal;          //坦克发射子弹的cd时间
    private float timeDefended = 3; //无敌的时长
    private bool isDefended = true;  //是否无敌
    //引用
    private SpriteRenderer sr;
    public Sprite[] tankSprite;//上，右，下，左
    public GameObject bulletPerfab;//子弹的引用
    public GameObject explosionPrefabs; //爆炸的预制体
    public GameObject defendedPrefabs;//防御盾的预制体
    public AudioSource moveAudio;  //移动时的音效
    public AudioClip[] tankAudio;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();//获取组件
    }

    // Update is called once per frame

    void Update()
    {
        if ( isDefended )
        {
            defendedPrefabs.SetActive( true );
            timeDefended -= Time.deltaTime;
            if ( timeDefended <= 0 )
            {
                isDefended = false;
                defendedPrefabs.SetActive( false );
            }
        }
     
    }
    void FixedUpdate()
    {
        if ( playerMangger.Instance.isDefeat )
        {
            return;
        }
        TankMove();
        #region  攻击cd
        if ( timeVal >= 0.4f )
        {
            Attack();
        }
        else
        {
            timeVal += Time.fixedDeltaTime;
        }
        #endregion
    }

    /// <summary>
    /// 坦克的死亡方法
    /// </summary>
    public void TankDie()
    {
        if ( isDefended )
        {
            return;
        }
        //产生爆炸特效
        Instantiate( explosionPrefabs , transform.position , transform.rotation );
        //玩家生命值减1
        playerMangger.Instance.isDeath = true;
        //销毁玩家
        Destroy( gameObject );
    }

    /// <summary>
    /// 坦克攻击方法
    /// </summary>
    private void Attack()
    {
        if ( Input.GetKeyDown( KeyCode.Space ) )
        {
            //实例化生成一个子弹
            Instantiate( bulletPerfab , transform.position , Quaternion.Euler(transform.eulerAngles+bulletEularAngles) );
            timeVal = 0;

        }
    }
    /// <summary>
    /// 坦克移动
    /// </summary>
    private void TankMove()
    {
        float  h = Input.GetAxisRaw( "Horizontal" );
        transform.Translate( Vector3.right * h * moveSpeed * Time.fixedDeltaTime , Space.World );
        if ( h < 0 )
        {
            sr.sprite = tankSprite[3];
            bulletEularAngles = new Vector3( 0 , 0 , 90 );  //向左旋转
        }
        else if ( h > 0 )
        {
            sr.sprite = tankSprite[1];
            bulletEularAngles = new Vector3( 0 , 0 , -90 );//向右旋转
        }
        if ( Mathf.Abs( h ) > 0.05f )
        {
            moveAudio.clip = tankAudio[1];
            if ( !moveAudio.isPlaying )
            {
                moveAudio.Play();
            }
        }
        else if ( Mathf.Abs( h ) < 0.05f )
        {
            moveAudio.clip = tankAudio[0];
            if ( !moveAudio.isPlaying )
            {
                moveAudio.Play();
            }
        }
        if(h!=0)        //优先级判断，如果水平方向有输入，则只执行水平位移，否则才执行垂直位移
        {
            return ;
        }
        float v = Input.GetAxisRaw( "Vertical" );
        transform.Translate( Vector3.up * v * moveSpeed * Time.fixedDeltaTime , Space.World );
        if ( v < 0 )
        {
            sr.sprite = tankSprite[2];
            bulletEularAngles = new Vector3( 0 , 0 , -180 );//向下旋转
        }
        else if ( v > 0 )
        {
            sr.sprite = tankSprite[0];
            bulletEularAngles = new Vector3( 0 , 0 , 0 );//向上旋转
        }
        if ( Mathf.Abs( v ) > 0.05f )
        {
            moveAudio.clip = tankAudio[1];
            if ( !moveAudio.isPlaying )
            {
                moveAudio.Play();
            }
        }
        else if ( Mathf.Abs( v ) < 0.05f )
        {
            moveAudio.clip = tankAudio[0];
            if ( !moveAudio.isPlaying )
            {
                moveAudio.Play();
            }
        }
    }
}
