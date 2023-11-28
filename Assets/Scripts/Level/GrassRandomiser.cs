using System;
using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(SpriteRenderer))]
public class GrassRandomiser : MonoBehaviour
{
    [SerializeField] private Sprite[] randomTiles;

    private void Start()
    {
        var rnd = new Random();
        GetComponent<SpriteRenderer>().sprite = randomTiles[rnd.Next(randomTiles.Length)];
    }
}