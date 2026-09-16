using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject Sender {  get; private set; }

    private BattleManager _battleManager;

    private bool _wasInited = false;

    public void Init(GameObject sender, BattleManager bm)
    {
        if(!_wasInited)
        {
            Sender = sender;
            _wasInited = true;
            _battleManager = bm;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       _battleManager.OnProjectileCollisionEnter(this, collision);
        Destroy(gameObject);
    }
}
