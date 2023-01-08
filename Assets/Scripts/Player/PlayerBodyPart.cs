using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyPart : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    public void TakeDamage(float damage, Damage damageObject){
        _player.CheckTakeDamage(damage, damageObject);
    }
}
