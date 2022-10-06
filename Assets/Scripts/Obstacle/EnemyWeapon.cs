using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField]
    private EnemyNavigationController navigationController;

    public EnemyNavigationController NavigationController { get => navigationController; }
}