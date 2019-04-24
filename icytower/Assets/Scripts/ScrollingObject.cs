using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{

    #region Components
    private Rigidbody2D rb2d;
    #endregion Components

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, GameController.Instance.ScrollSpeed);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.GameOver)
        {
            rb2d.velocity = Vector2.zero;
        }
        rb2d.velocity = new Vector2(0, GameController.Instance.ScrollSpeed);
    }


}
