using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapCreation : MonoBehaviour
{
    //地图的实例化  20*16
    private const  int ITEM_COUNT = 30;


    //用来实例化地图的数组
    // 0 .老家， 1. 墙 ，3 .出生特效 4.河流 5. 草  6 .空气墙
    public GameObject[] mapItem;

    private List<Vector3> itemPositionList = new List<Vector3>(); // 装已经放了物品的位置


    private void Awake()
    {
        InitMap();
    }
    /// <summary>
    /// 初始化地图
    /// </summary>
    private void InitMap()
    {
        #region 固定地图
        //实例化boss
        //Instantiate( mapItem[0] , new Vector3( 0 , -8 , 0 ) , Quaternion.identity );
        CreateMapItem( mapItem[0] , new Vector3( 0 , -8 , 0 ) , Quaternion.identity );
        //boss周围的墙
        CreateMapItem( mapItem[1] , new Vector3( -1 , -8 , 0 ) , Quaternion.identity );
        CreateMapItem( mapItem[1] , new Vector3( 1 , -8 , 0 ) , Quaternion.identity );
        for ( int i = -1 ; i < 2 ; i++ )
        {
            CreateMapItem( mapItem[1] , new Vector3( i , -7 , 0 ) , Quaternion.identity );
        }
        //实例化外围空气墙
        for ( int i =-11 ; i <= 11 ; i++ )
        {
            CreateMapItem( mapItem[6] , new Vector3( i , 9 , 0 ) , Quaternion.identity );
            CreateMapItem( mapItem[6] , new Vector3( i , -9 , 0 ) , Quaternion.identity );
        }
        for ( int i = -8 ; i <= 8 ; i++ )
        {
            CreateMapItem( mapItem[6] , new Vector3( 11 , i , 0 ) , Quaternion.identity );
            CreateMapItem( mapItem[6] , new Vector3( -11 , i , 0 ) , Quaternion.identity );
        }
        //实例化玩家
        GameObject playerGo = Instantiate( mapItem[3] , new Vector3( -2 , -8 , 0 ) , Quaternion.identity );  //设置玩家的位置
        playerGo.GetComponent<born>().creatPlayer = true;//将玩家设为true

        //实例化敌人
        Instantiate( mapItem[3] , new Vector3( -10 , 8 , 0 ) , Quaternion.identity );
        Instantiate( mapItem[3] , new Vector3( 0 , 8 , 0 ) , Quaternion.identity );
        Instantiate( mapItem[3] , new Vector3( 10 , 8 , 0 ) , Quaternion.identity );

        InvokeRepeating( "CreateEnemy" , 4 , 5 );
        #endregion

        #region 随机地图的实现
        for ( int i = 0 ; i < ITEM_COUNT ; i++ )
        {
            CreateMapItem( mapItem[1] , CreateRandomPosition() , Quaternion.identity );
        }
        for ( int i = 0 ; i < ITEM_COUNT ; i++ )
        {
            CreateMapItem( mapItem[2] , CreateRandomPosition() , Quaternion.identity );
        }
        for ( int i = 0 ; i < ITEM_COUNT ; i++ )
        {
            CreateMapItem( mapItem[4] , CreateRandomPosition() , Quaternion.identity );
        }
        for ( int i = 0 ; i < ITEM_COUNT ; i++ )
        {
            CreateMapItem( mapItem[5] , CreateRandomPosition() , Quaternion.identity );
        }
        #endregion
    }

    /// <summary>
    /// 封装函数，创建地图，使得创建时不会在hierarchy产生大量物品
    /// </summary>
    /// <param name="createItem"> 需要创建的对象</param>
    /// <param name="createPosition"> 创建的位置</param>
    /// <param name="createRotation"> 创建时旋转的角度</param>
    private void CreateMapItem(GameObject createItem,Vector3 createPosition,Quaternion createRotation)
    {
        GameObject itmeGo = Instantiate( createItem , createPosition , createRotation );
        itmeGo.transform.SetParent( gameObject.transform );
        itemPositionList.Add( createPosition );
    }
    /// <summary>
    /// 产生随机坐标付给物体
    /// </summary>
    private Vector3 CreateRandomPosition()
    {
        //在x=10,-10 和y = -8,8的位置不生成
        while ( true )
        {
            Vector3 createPosition = new Vector3( Random.Range( -9 , 10 ) , Random.Range( -7 , 8 ) , 0 );
            if (! itemPositionList.Contains( createPosition ) )
            {
                return createPosition;
            }
        }
    }

    private void CreateEnemy()
    {
        int num = Random.Range( 0 , 3 );
        Vector3 EnemyPos = new Vector3();
        if ( num == 0 )
        {
            EnemyPos = new Vector3( -10 , 8 , 0 );
        }
        else if ( num == 1 )
        {
            EnemyPos = new Vector3( 0 , 8 , 0 );
        }
        else
        {
            EnemyPos = new Vector3( 10 , 8 , 0 );
        }
        CreateMapItem( mapItem[3] , EnemyPos , Quaternion.identity );
    }
}
