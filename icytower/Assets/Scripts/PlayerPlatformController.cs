using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformController : MonoBehaviour
{

    #region Serialized
    [SerializeField] private float minGroundNormalY = .65f;
    [SerializeField] private float gravityModifier = 1f;
    [SerializeField] private float maxSpeed = 7;
    [SerializeField] private float jumpTakeOffSpeed = 7;
    #endregion Serialized

    #region Components
    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    private SpriteRenderer spriteRenderer;
    private bool isDead = false;
    #endregion Components

    #region Constants
    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;
    #endregion Constants

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    void Update()
    {
        targetVelocity = Vector2.zero;
        if (isDead) return;
        targetVelocity = ComputeVelocity();
    }

    void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != Constants.Tags.Finish) return;
        isDead = true;
        GameController.Instance.PlayerDead();
    }

    #region Privates

    private Vector2 ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis(Constants.Axes.Horizontal);

        if (Input.GetButtonDown(Constants.KeyNames.Jump) && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp(Constants.KeyNames.Jump))
        {
            if (velocity.y > 0)
            {
                velocity.y *= 0.5f;
            }
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x < 0.01f) : (move.x > 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        rb2d.rotation = 0;

        return move * maxSpeed;
    }

    private void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                // one way platform handling
                BoxCollider2D platform = hitBuffer[i].collider.GetComponent<BoxCollider2D>();
                if (!platform || (hitBuffer[i].normal == Vector2.up && velocity.y < 0 && yMovement))
                {

                    hitBufferList.Add(hitBuffer[i]);

                }
            }

            distance = ComputeOverallDistance(yMovement, distance);

        }

        rb2d.position += move.normalized * distance;
    }

    private float ComputeOverallDistance(bool yMovement, float distance)
    {
        for (int i = 0; i < hitBufferList.Count; i++)
        {
            Vector2 currentNormal = hitBufferList[i].normal;
            if (currentNormal.y > minGroundNormalY)
            {
                grounded = true;
                if (yMovement)
                {
                    groundNormal = currentNormal;
                    currentNormal.x = 0;
                }
            }

            float projection = Vector2.Dot(velocity, currentNormal);
            if (projection < 0)
            {
                velocity -= projection * currentNormal;
            }

            float modifiedDistance = hitBufferList[i].distance - shellRadius;
            distance = modifiedDistance < distance ? modifiedDistance : distance;
        }

        return distance;
    }

    #endregion Privates

}