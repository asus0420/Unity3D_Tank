using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class born : MonoBehaviour
{
    public GameObject playerPrefab;

    public GameObject[] enemyPrefabsList;//敌人预制体列表

    public bool creatPlayer = false;  //是否产生了玩家
    void Start()
    {
        Invoke( "BornTank" , 1.0f );
        Destroy( gameObject , 1.0f );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BornTank()
    {
        if ( creatPlayer )
        {
            Instantiate( playerPrefab , transform.position , Quaternion.identity );
        }
        else
        {
            int num = Random.Range( 0 , 3 );
            Instantiate( enemyPrefabsList[num] , transform.position , Quaternion.identity );
        }
       
    }
}
