using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SerializableInterface のテストクラス
/// </summary>
public class TestInterface : MonoBehaviour, IEntryRun
{
    public void EntryRun() {
        Debug.Log("Entry Run 実行");
    }
}