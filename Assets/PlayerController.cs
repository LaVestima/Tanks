using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {
    public GameObject turret;
    public GameObject bullet;
    public GameObject healthbar;
    public GameObject defeatScreen;
    public float bulletSpeed = 50.0f;
    public int health = 5;

    int maxHealth;
    float moveX;
    float moveY;
    float mouseX;
    float mouseY;
    float angle = 0.0f;
    float turretAngle = 0.0f;
    bool bulletFired = false;
    bool gameOver = false;
    
    private Rigidbody2D m_Rigidbody2D;
    private float m_MovementSmoothing = .05f;
    private Vector3 m_Velocity = Vector3.zero;
    
    void Start() {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        maxHealth = health;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.R))
        {
            health = maxHealth;
            gameOver = false;
            defeatScreen.SetActive(false);
        }

        if (gameOver) return;

        healthbar.transform.localScale = new Vector3(1.0f * health / maxHealth, healthbar.transform.localScale.y, healthbar.transform.localScale.y);
        healthbar.transform.localPosition = new Vector3(-0.02f * ((maxHealth - health) / (1f * maxHealth)), healthbar.transform.localPosition.y, healthbar.transform.localPosition.z);
        
        if (health <= 0)
        {
            defeatScreen.SetActive(true);
            gameOver = true;

            return;
        }

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
    }

    void FixedUpdate()
    {
        if (gameOver) return;

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
        
        if (Input.GetAxisRaw("Fire1") == 0 && Input.GetAxisRaw("Fire2") == 0)
        {
            bulletFired = false;
        }

        if ((Input.GetAxisRaw("Fire1") == 1 || Input.GetAxisRaw("Fire2") == 1) && bulletFired == false)
        {
            bulletFired = true;

            var newBullet = Instantiate(bullet, transform.position, turret.transform.rotation);
            var bulletAngle = (turretAngle + 90.0) * Math.PI / 180.0f;
            
            newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector3(-(float)Math.Cos(bulletAngle), (float)Math.Sin(bulletAngle)) * bulletSpeed);
        }

        if (Input.GetButtonDown("Jump"))
        {
            health--;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: kolizje są wyłączone
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullets"))
        {
            health--;
            Destroy(collision.gameObject);
        }
    }
}
