using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {
    public GameObject turret;
    public GameObject bullet;
    public float bulletSpeed = 50.0f;

    float moveX;
    float moveY;
    float mouseX;
    float mouseY;
    float angle = 0.0f;
    float turretAngle = 0.0f;

    private Rigidbody2D m_Rigidbody2D;
    private float m_MovementSmoothing = .05f;
    private Vector3 m_Velocity = Vector3.zero;
    
    void Start() {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        //Debug.Log(moveX);
        //Debug.Log(moveY);
        //Debug.Log(mouseX);
        //Debug.Log(mouseY);
        
        turretAngle = (mouseX >= 0 ? 1 : -1) * Vector3.Angle(Vector3.up, new Vector3(mouseX, mouseY));
        angle = (moveX >= 0 ? 1 : -1) * Vector3.Angle(Vector3.up, new Vector3(moveX, moveY));

        //Debug.Log(turretAngle);


    }

    void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector2(moveX * 10f, moveY * 10f);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        Quaternion from = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z);
        Quaternion to = Quaternion.Euler(0f, 0f, -angle);

        if (Input.GetAxisRaw("Horizontal") != 0.0 || Input.GetAxisRaw("Vertical") != 0.0)
        {
            transform.rotation = Quaternion.Lerp(from, to, Time.fixedDeltaTime * 50.0f);
        }

        Quaternion turretFrom = Quaternion.Euler(0f, 0f, turret.transform.rotation.eulerAngles.z);
        Quaternion turretTo = Quaternion.Euler(0f, 0f, -turretAngle);

        turret.transform.rotation = Quaternion.Lerp(turretFrom, turretTo, Time.fixedDeltaTime * 50.0f);

        if (Input.GetButtonDown("Jump"))
        {
            //Debug.Log(turret.transform.rotation);

            var newBullet = Instantiate(bullet, transform.position, turret.transform.rotation);
            var bulletAngle = (turretAngle + 90.0) * Math.PI / 180.0f;
            
            //Debug.Log(new Vector3((float)Math.Cos(bulletAngle), (float)Math.Sin(bulletAngle));
            newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector3(-(float)Math.Cos(bulletAngle), (float)Math.Sin(bulletAngle)) * bulletSpeed);
        }
    }
}
