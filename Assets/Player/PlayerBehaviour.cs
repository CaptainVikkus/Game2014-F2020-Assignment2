using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerAnimType
{
    IDLE,
    RUN,
    JUMPUP,
    JUMPDOWN
}

public class PlayerBehaviour : MonoBehaviour
{
    public LivesBar lives;
    public ScoreBar score;
    public Joystick joystick;
    public GameObject camera;
    public float joystickHorizontalSensitivity;
    public float joystickVerticalSensitivity;
    public float horizontalForce;
    public float verticalForce;
    public bool isGrounded;
    public bool isJumping;
    public Transform spawnPoint;

    public AudioSource coinSound;
    public AudioSource hurtSound;

    public LayerMask platforms;
    public LayerMask hazards;

    private Rigidbody2D m_rigidBody2D;
    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;

    //private RaycastHit2D groundHit;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _Move();
        _Camera();
    }

    void _Move()
    {
        if (isGrounded)
        {
            if (!isJumping)
            {
                if (joystick.Horizontal > joystickHorizontalSensitivity)
                {
                    // move right
                    m_rigidBody2D.AddForce(Vector2.right * horizontalForce * Time.deltaTime);
                    m_spriteRenderer.flipX = false;
                    m_animator.SetInteger("AnimState", (int)PlayerAnimType.RUN);
                }
                else if (joystick.Horizontal < -joystickHorizontalSensitivity)
                {
                    // move left
                    m_rigidBody2D.AddForce(Vector2.left * horizontalForce * Time.deltaTime);
                    m_spriteRenderer.flipX = true;
                    m_animator.SetInteger("AnimState", (int)PlayerAnimType.RUN);
                }
                else
                {
                    m_animator.SetInteger("AnimState", (int)PlayerAnimType.IDLE);
                }
            }

            if ((joystick.Vertical > joystickVerticalSensitivity) && (!isJumping))
            {
                // jump
                m_rigidBody2D.AddForce(Vector2.up * verticalForce);
                m_animator.SetInteger("AnimState", (int)PlayerAnimType.JUMPUP);
                isJumping = true;
            }
            else
            {
                isJumping = false;
            }
        }
        else if (m_rigidBody2D.velocity.y < 0)
        {
            m_animator.SetInteger("AnimState", (int)PlayerAnimType.JUMPDOWN);
        }
    }

    void _Camera()
    {
        camera.transform.position = transform.position
            + new Vector3(joystick.Horizontal / 2, joystick.Vertical / 2, camera.transform.position.z);
    }

    public void Die()
    {
        hurtSound.Play();
        transform.position = spawnPoint.position;
        m_rigidBody2D.velocity = new Vector2(0, 0);
        lives.LoseLife();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (((1 << other.gameObject.layer) & platforms) != 0)
        {
            isGrounded = true;
        }
        if (other.gameObject.tag == "Enemy")
        {
            Die();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (((1 << other.gameObject.layer) & platforms) != 0)
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1<<other.gameObject.layer) & hazards) != 0)
        {// Hazard hit, die
            Die();
        }
        if (other.gameObject.tag == "Coin")
        {// Get Points, Kill Coin
            ScoreBar.score += 50;
            Destroy(other.gameObject);
            coinSound.Play();
        }
    }
}
