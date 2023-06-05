using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Data", menuName = "Scriptable Object/Weapon Data", order = int.MaxValue)]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    public string weaponName;

    [SerializeField]
    public Sprite sprite;
    public GameObject bullet;

    [SerializeField]
    public int id;
    
    [SerializeField]
    public float damage;

    [SerializeField]
    public float speed;

    [SerializeField]
    public float bulletSpeed;

}
