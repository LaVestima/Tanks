using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float moveX;
    float moveY;

    private Rigidbody2D m_Rigidbody2D;
    private float m_MovementSmoothing = .05f;
    private Vector3 m_Velocity = Vector3.zero;

    void Start() {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        Debug.Log(m_Rigidbody2D);
    }

    void Update() {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector2(moveX * 10f, moveY * 10f);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }
}
