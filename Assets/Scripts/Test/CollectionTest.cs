using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public enum EnemyType {
    Normal,
    Boss
}

[System.Serializable]
public class EnemyData {
    public int no;
    public EnemyType enemyType;
}

public class CollectionTest : MonoBehaviour
{
    public List<EnemyData> enemyDataList = new();

    public List<EnemyData> normalDataList = new();
    public List<EnemyData> bossDataList = new();

    void Start()
    {
        normalDataList = GetEnemyTypeList(EnemyType.Normal);
        bossDataList = GetEnemyTypeList(EnemyType.Boss);
    }


    private List<EnemyData> GetEnemyTypeList(EnemyType chooseEnemyType) {
        return enemyDataList.Where(x => x.enemyType == chooseEnemyType).ToList();
    }
}
