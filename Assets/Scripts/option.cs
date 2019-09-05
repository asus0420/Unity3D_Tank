using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class option : MonoBehaviour
{
    private int choice = 1;
    public Transform posOne;
    public Transform posTwo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown( KeyCode.W ) || Input.GetKeyDown( KeyCode.UpArrow ) )
        {
            choice = 1;
            transform.position = posOne.position;
        }
        else if ( Input.GetKeyDown( KeyCode.S ) || Input.GetKeyDown( KeyCode.DownArrow ) )
        {
            choice = 2;
            transform.position = posTwo.position;
        }

        if ( choice == 1 && Input.GetKeyDown( KeyCode.Space ) )
        {
            SceneManager.LoadScene( 1 );
        }
    }
}
