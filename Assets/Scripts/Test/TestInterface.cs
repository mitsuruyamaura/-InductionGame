using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SerializableInterface �̃e�X�g�N���X
/// </summary>
public class TestInterface : MonoBehaviour, IEntryRun
{
    public void EntryRun() {
        Debug.Log("Entry Run ���s");
    }
}