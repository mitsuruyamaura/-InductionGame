using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObstacleBase : MonoBehaviour
{
    protected NavMeshObstacle navMeshObstacle;

    [SerializeField]
    protected ObstacleType obstacleType;

    public ObstacleType ObstacleType { get => obstacleType; }


    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (!TryGetComponent(out navMeshObstacle)) {
            Debug.Log("NavMeshObstacle æ“¾o—ˆ‚Ü‚¹‚ñB");
        } else {
            OnEnterObstacle();
        }
    }

    protected virtual void OnEnterObstacle() {
        Debug.Log("On Enter");
    }

    protected virtual void OnExitObstacle() {

    }
}
