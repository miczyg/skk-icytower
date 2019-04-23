using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    private BoxCollider2D groudCollider;
    private float groundVLen;

    // Start is called before the first frame update
    void Start()
    {
        groudCollider = GetComponent<BoxCollider2D>();
        groundVLen = groudCollider.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -groundVLen)
        {
            RepositionBackground();
        }
    }

    private void RepositionBackground()
    {
        var groundOffset = new Vector2(0, groundVLen * 2f - 1);
        transform.position = (Vector2)transform.position + groundOffset;
    }
}
