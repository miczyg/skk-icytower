using UnityEngine;
using static Assets.Scripts.Constants;

public class Player : MonoBehaviour
{
    #region Public
    public float upForce = 200f;
    [SerializeField] private float sideForce = 100f;

    #endregion Public

    #region Private
    private bool isDead = false;


    #endregion Private

    #region Components

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    #endregion Components


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDead)
        {
            if (Input.GetKeyUp(KeyNames.Space))
            {
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(0, upForce));
                //anim.SetTrigger(Animations.Flap);
            }

            var moveX = Input.GetAxis("Horizontal");
            rb2d.AddForce(new Vector2(moveX * sideForce, 0));

            FlipSprite(moveX);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.GetComponent<ScrollingObject>()) return;
        isDead = true;
        //anim.SetTrigger(Animations.Die);
        GameController.Instance.PlayerDead();
    }

    private void FlipSprite(float moveX)
    {
        bool flipSprite = (spriteRenderer.flipX ? (moveX > 0.01f) : (moveX < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
}
