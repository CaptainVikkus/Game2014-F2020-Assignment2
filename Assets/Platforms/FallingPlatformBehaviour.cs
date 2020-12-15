using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformBehaviour : MonoBehaviour
{
    [SerializeField] SpriteRenderer circle;
    [SerializeField] BoxCollider2D platformCollider;
    [SerializeField] float fallTime;
    [SerializeField] float fallHeight;
    [SerializeField] float fallspeed;
    private float fallCounter;
    public bool falling;
    [SerializeField] float respawnTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (falling) { _Fall(); }
    }

    void _Fall()
    {
        fallCounter += Time.deltaTime;
        circle.color = new Color(
            circle.color.r,                 //r
            circle.color.g,                 //g
            circle.color.b,                 //b
            (1 - fallCounter / fallTime));    //a

        if (fallCounter >= fallTime)
        {
            fallCounter = 0;
            StartCoroutine(ExecuteFall());
        }
    }

    IEnumerator ExecuteFall()
    {
        falling = false;
        //Disable collider and fall
        platformCollider.enabled = false;
        float originalheight = transform.position.y;
        while (transform.position.y > fallHeight)
        {
            transform.position += Vector3.down * fallspeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = new Vector3(transform.position.x, fallHeight, transform.position.z);

        //Respawn and enable collider after time
        yield return new WaitForSeconds(respawnTime);
        transform.position = new Vector3(transform.position.x, originalheight, transform.position.z);
        platformCollider.enabled = true;
        circle.color = new Color(circle.color.r, circle.color.g, circle.color.b, 1);             
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            falling = true;
            Debug.Log("Fall Started");
        }
    }
}
