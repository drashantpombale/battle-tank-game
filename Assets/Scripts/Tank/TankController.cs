﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TankController : MonoSingletonGeneric<TankController>, Idamagable
{   
    [SerializeField]
    private Joystick joystick;
    [SerializeField]
    private GameObject tankExplosion;
    [SerializeField]
    private Transform firepoint;
    private Rigidbody rigidbody;
    private Transform tank;
    private ParticleSystem dust;
    private int hp;
    public int Kills;
    public HealthBar healthBar;

    private bool isdead = false;

    void Start()
    {
        healthBar.setMaxHealth(hp);
    }

    protected override void Awake()
    {
        Kills = 0;
        hp = 100;
        dust = GetComponent<ParticleSystem>();
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
                    dust.Play();
                    
                }
            }
            else
            {
                dust.Stop();
                rigidbody.velocity = Vector3.zero;
            }
        }

        GameEvents.Instance.OnFirstKill += FirstKill;
    }

    private void FirstKill()
    {
        Debug.Log("Achievement Unlocked: First Blood-Player got the first kill");
        GameEvents.Instance.OnFirstKill -= FirstKill;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("BossEnemy")) {
            StartCoroutine(DeathCoroutine());
            isdead = true;
            gameObject.layer = 11;
   
        }
    }

    private IEnumerator DeathCoroutine() {
        TankProvider.Instance.Boom(transform);
        yield return null;
        DestroyScene.Instance.DestroyAll();
        Destroy(gameObject);
        TankProvider.Instance.Boom(transform);
    }

    private void Fire()
    {
        GameObject obj = Shoot.Instance.Fire(firepoint);
        obj.layer = 9;
    }

    public void takeDamage() {
        hp -= 30;
        healthBar.setHealth(hp);
        if (hp <= 0)
        {
            isdead = true;
            StartCoroutine(DeathCoroutine());
        }

    }
}

