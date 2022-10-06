using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
public class FloorMove : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurface _surface;

    void Start()
    {
        transform.DOMoveX(-1, 5.0f).SetEase(Ease.InQuad).SetLink(gameObject).SetRelative();//.OnComplete(() => _surface.BuildNavMesh());
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.TryGetComponent(out PlayerNavigationController player)) {
            player.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.TryGetComponent(out PlayerNavigationController player)) {
            player.transform.SetParent(null);
        }
    }

    public void Update() {
        //if (Input.GetKeyDown(KeyCode.Space)) {
            // NavMesh‚ðƒrƒ‹ƒh‚·‚é
            _surface.BuildNavMesh();
        //}
    }
}
