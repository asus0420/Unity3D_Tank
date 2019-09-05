using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullets : MonoBehaviour
{
    public float moveSpeed = 10.0f; //子弹的移动速度
    public bool isPlayerBullet;  //是否是敌人的子弹
    public AudioClip barriarAudio;

    void Start()
    {
        
    }
    void Update()
    {
        transform.Translate( transform.up * moveSpeed * Time.deltaTime , Space.World );
    }
    /// <summary>
    /// 触碰检测
    /// </summary>
    /// <param name="collision">触发的游戏物体</param>
    private void OnTriggerEnter2D( Collider2D collision )
    {
        switch ( collision.tag )
        {
            case "Tank":
                if(!isPlayerBullet)
                {
                    collision.SendMessage( "TankDie" );
                    Destroy( this.gameObject );
                }
                break;
            case "Heart":
                Debug.Log( "子弹碰到boss了" );
                collision.SendMessage( "LossGame" );
                Destroy( gameObject );
                break;
            case "Wall":
                Destroy( collision.gameObject );
                Destroy( gameObject );
                break;
            case "Barriar":
                AudioSource.PlayClipAtPoint( barriarAudio , collision.gameObject.transform.position );
                Destroy( gameObject );
                break;
            case "AirBarriar":
                 Destroy( gameObject );
                 break;
            case "Grass":
                break;
            case "Enemy":
                if ( isPlayerBullet )
                {
                    collision.SendMessage( "TankDie" );
                    Destroy( this.gameObject );
                }
                break;
            default:
                break;
        }
    }
}
