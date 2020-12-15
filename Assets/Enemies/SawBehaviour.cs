using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBehaviour : MonoBehaviour
{
    [SerializeField] Transform lookAhead;
    [SerializeField] Transform lookDown;
    [SerializeField] LayerMask collisionLayer;
    [SerializeField] float speed;
    [SerializeField] bool isGroundAhead;
    [SerializeField] bool isWallAhead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _Look();
        _Move();
    }

    void _Move()
    {
        transform.position += -transform.right * speed;
    }

    void _Look()
    {
        _LookAhead();
        _LookDown();
        if (isWallAhead || !isGroundAhead)
        {
            _FlipX();
        }
    }

    private void _LookDown()
    {
        var groundHit = Physics2D.Linecast(transform.position, lookDown.position, collisionLayer);
        if (groundHit)
        {
            isGroundAhead = true;
        }
        else
        {
            isGroundAhead = false;
        }

        Debug.DrawLine(transform.position, lookDown.position, Color.green);
    }
    private void _LookAhead()
    {
        var groundHit = Physics2D.Linecast(transform.position, lookAhead.position, collisionLayer);
        if (groundHit)
        {
            isWallAhead = true;
        }
        else
        {
            isWallAhead = false;
        }

        Debug.DrawLine(transform.position, lookAhead.position, Color.red);
    }

    private void _FlipX()
    {
        //transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
        transform.right *= -1;
    }
}
