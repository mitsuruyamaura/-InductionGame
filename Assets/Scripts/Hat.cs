using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : MonoBehaviour
{
    [SerializeField]
    private PlayerNavigationController navigationController;

    public PlayerNavigationController PlayerNavigationController { get => navigationController; }
}