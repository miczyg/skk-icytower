using UnityEngine;
using static Assets.Scripts.Constants;

public class Player : MonoBehaviour
{
    #region Public
    public float upForce = 200f;

    #endregion Public

    #region Private
    private bool isDead = false;


    #endregion Private

    #region Components

    private Rigidbody2D rb2d;
    private Animator anim;

    #endregion Components


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (Input.GetKeyDown(KeyNames.Space))
            {
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(0, upForce));
                anim.SetTrigger(Animations.Flap);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // If the bird collides with something set it to dead...
        if (collision.GetComponent<Column>()) return;
        isDead = true;
        anim.SetTrigger(Animations.Die);
        GameController.Instance.BirdDied();
    }
}
