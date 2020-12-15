using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceBehaviour : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Transform playerPos;
    private Vector3 spawnPoint;

    [SerializeField] LayerMask platforms;
    [SerializeField] LayerMask hazards;
    public float jumpForce;
    public bool isGrounded;
    public bool isJumpCooldown;
    public float jumpCooldownTime;
    private float countDown;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerPos = FindObjectOfType<PlayerBehaviour>().GetComponent<Transform>();
        Vector3 spawn = gameObject.transform.position;
        spawnPoint = spawn;
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
    }

    void _Move()
    {
        if (isGrounded)
        {
            if (!isJumpCooldown)
            { //Jump Available
                var jumpVec = new Vector2(-0.5f, 1); //Jump Left
                if (playerPos.position.x > transform.position.x)
                { //player to the right
                    jumpVec.x *= -1;
                }
                Debug.Log("Mace Jumps " + jumpVec.x + " " + jumpVec.y);
                rb2D.AddForce(jumpVec * jumpForce);
                isJumpCooldown = true;
            }
            else
            { //Start JumpCooldown while grounded
                countDown += Time.deltaTime;
                if (countDown > jumpCooldownTime)
                {
                    isJumpCooldown = false;
                    countDown = 0;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (((1 << other.gameObject.layer) & platforms) != 0)
        {
            isGrounded = true;
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
        if (((1 << other.gameObject.layer) & hazards) != 0)
        {// Hazard hit, teleport back, lose life
            transform.position = spawnPoint;
            rb2D.velocity = new Vector2(0, 0);
        }
    }
}
