using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    [SerializeField] private float _xOffset = -1.8f; 
    [SerializeField] private float _yOffset = -1.5f; 
    [SerializeField] private float _boardSize = 6; 
    [SerializeField] private float _boardSpacing = .7f; 
    
    private void Start()
    {
        CreateBoard();
    }

    void CreateBoard()
    {
    
        for (int x = 0; x < _boardSize; x++)
        {
            for (int y = 0; y < _boardSize; y++)
            {
                Tile tile = Managers.Object.Spawn<Tile>(ResourcePaths.Tile);
                tile.transform.SetParent(transform, true);
                tile.transform.position = new Vector3(x * _boardSpacing + _xOffset, y * _boardSpacing + _yOffset, 0);
            }
        }

        SetFruit();
    }

    private void SetFruit()
    {
        foreach (var tile in Managers.Object.Tiles)
        {
            int randomNum = Random.Range(1, 5);
            string fruitName = Managers.Fruit.GetFruitByGrade(randomNum);
            Fruit fruit = Managers.Object.Spawn<Fruit>(fruitName);
            fruit.PlayAnimation("AppearFruit");
            tile.SetFruit(fruit);
        }
    }
    
 


}
