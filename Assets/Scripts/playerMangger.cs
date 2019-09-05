using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerMangger : MonoBehaviour
{
    //属性值
    public int Hp = 3;          //生命条数
    public int Scrore = 0;              //得分
    public bool isDeath = false;    //玩家是否死亡
    public bool isDefeat = false;  //游戏失败
    //引用
    public GameObject born;
    public Text playerScore;
    public Text playerHP;
    public GameObject isDefeatUI;

    private static playerMangger instance;

    public static playerMangger Instance
    {
        get
        { 
            return playerMangger.instance; 
        }
        set
        {
            playerMangger.instance = value;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( isDefeat )
        {
            isDefeatUI.SetActive( true );
            return;
        }
        if ( isDeath )
        {
            Recover();
        }
        playerScore.text = Scrore.ToString();
        playerHP.text = Hp.ToString();
    }

    private void Recover()
    {
        if ( Hp <= 0 )
        {
            //游戏失败
            isDefeat = true;
            Invoke( "ReturnToTheMainMenu" , 3 );
        }
        else
        {
            --Hp;
            GameObject playerGo = Instantiate( born , new Vector3( -2 , -8 , 0 ) , Quaternion.identity );
            playerGo.GetComponent<born>().creatPlayer = true;
            isDeath = false;
        }
    }

    private void ReturnToTheMainMenu()
    {
        SceneManager.LoadScene( 0 );
    }
}
