using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    private BoxCollider2D groudCollider;
    private float groundHLength;

    // Start is called before the first frame update
    void Start()
    {
        groudCollider = GetComponent<BoxCollider2D>();
        groundHLength = groudCollider.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -groundHLength)
        {
            RepositionBackground();
        }
    }

    private void RepositionBackground()
    {
        var groundOffset = new Vector2(groundHLength * 2f, 0);
        transform.position = (Vector2)transform.position + groundOffset;
    }
}
