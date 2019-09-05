using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class heart : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite breakenHeart;

    public AudioClip dieAudio;
    public GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 游戏失败
    /// </summary>
    public void LossGame()
    {
        Instantiate( explosionPrefab , transform.position , transform.rotation );
        sr.sprite = breakenHeart;
        playerMangger.Instance.isDefeat = true;
        AudioSource.PlayClipAtPoint( dieAudio , transform.position );
        Invoke( "HeartBoom" , 3 );
    }

    private void HeartBoom()
    {
        SceneManager.LoadScene( 0 );
    }
}
