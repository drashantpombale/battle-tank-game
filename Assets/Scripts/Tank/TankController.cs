﻿using System.Collections;
using UnityEngine;


public class TankController : MonoSingletonGeneric<TankController>
{   
    [SerializeField]
    private Joystick joystick;
    [SerializeField]
    private GameObject tankExplosion;
    private Rigidbody rigidbody;
    private Transform tank;
    private bool isdead = false;

    protected override void Awake()
    {
        base.Awake();
        tank = gameObject.transform;
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if(!isdead){
            if (joystick.pressed)
            {
                if (Mathf.Abs(joystick.Horizontal) > 0.3f || Mathf.Abs(joystick.Vertical) > 0.3f)
                {
                    rigidbody.velocity = new Vector3(joystick.Horizontal * 15f, 0, joystick.Vertical * 15f);
                    tank.rotation = Quaternion.LookRotation(new Vector3(joystick.Horizontal * 10f, rigidbody.velocity.y, joystick.Vertical * 10f));
                }
            }
            else
            {
                rigidbody.velocity = new Vector3();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("BossEnemy")) {
            Debug.Log("Collision detected");
            StartCoroutine(DeathCoroutine());
            isdead = true;
            gameObject.layer = 11;
        }
    }

    private IEnumerator DeathCoroutine() {
        Debug.Log("Coroutine started");
        TankProvider.Instance.Boom(transform);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        TankProvider.Instance.Boom(transform);
        Debug.Log("Coroutine finished");
    } 
    
}

