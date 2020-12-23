using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField]
    private Joystick joystick;
    private Transform turret;
    private Transform aim;
    private float firecd = 0f;
    private void Awake()
    {
        turret = gameObject.transform;

        aim = gameObject.transform.Find("FirePoint");
        if (aim != null) Debug.Log("aim");
    }

    private void Update()
    {
        if (joystick.pressed)
        {
           turret.rotation = Quaternion.LookRotation(new Vector3(joystick.Horizontal * 10f, 0, joystick.Vertical * 10f));
        }

        if (Mathf.Abs(joystick.Horizontal) > 0.3 || Mathf.Abs(joystick.Vertical) > 0.3) {
            if (firecd <= 0)
            {
                Shoot.Instance.Fire(aim);
                firecd = 1f;
            }
            else firecd -= Time.deltaTime;
        }
      
    }
}

