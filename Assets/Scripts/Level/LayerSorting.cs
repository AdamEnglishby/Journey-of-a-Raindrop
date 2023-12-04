using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LayerSorting : MonoBehaviour
{
    private SpriteRenderer _renderer;

    private void Start()
    {
        this._renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _renderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100f);
    }
}