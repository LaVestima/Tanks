using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float timeAlive = 5.0f;

    // TODO: pochodzenie pocisku, czy możesz się zabić swoim?
    int hitCount = 0;
    string collisionSortingLayerName;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Bullets"), LayerMask.NameToLayer("Tanks"));
    }

    void Update()
    {
        timeAlive -= Time.deltaTime;

        if (timeAlive <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collisionSortingLayerName = collision.gameObject.GetComponent<SpriteRenderer>().sortingLayerName;

        if (collisionSortingLayerName == "Environment")
        {
            //Debug.Log("Hit!!!!!!!!");

            if (hitCount == 0)
            {
                // TODO: Odbij pocisk
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
