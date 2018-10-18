using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    int hitCount = 0;

    void Start()
    {
        Debug.Log(LayerMask.NameToLayer("Bullets"));
        Debug.Log(LayerMask.NameToLayer("Tanks"));
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Bullets"), LayerMask.NameToLayer("Tanks"));
    }

    void Update()
    {

        //Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.contacts[0].normal);
        //Debug.Log(collision.gameObject.GetComponent<SpriteRenderer>().sortingLayerName);

        if (collision.gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "Environment")
        {
            //Debug.Log("Hit!!!!!!!!");

            if (hitCount == 0)
            {
                // Odbij pocisk
                gameObject.GetComponent<Rigidbody2D>().MoveRotation(transform.rotation.z + 90.0f);

                hitCount++;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
