using System;
using UnityEngine;
using UnityEngine.UI;

public class GameEvents : MonoSingletonGeneric<GameEvents>
{
    public  event Action OnPlayerKill;
    public  event Action OnFirstKill;
    public  event Action OnFirstWaveClear;
    int count;
    public Text text;

    private void Start()
    {
        count = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < count) {
            count = GameObject.FindGameObjectsWithTag("Enemy").Length;
            OnPlayerKill?.Invoke();
            text.text = "KILLS: " + (TankController.Instance.Kills*100);
        }
    }
    public void FirstKillTrigger()
    {
        OnFirstKill?.Invoke();
    }
}
