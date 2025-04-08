using System.Collections.Generic;
using System.Linq;
using CWLib;
using UnityEngine;

public class ObjectManager : BaseObjectManager
{
    public Board Board {get; private set;}
    public HashSet<Tile> Tiles { get; private set; } = new HashSet<Tile>();
    public HashSet<Fruit> Fruits { get; private set; } = new HashSet<Fruit>();
    
    public override T Spawn<T>(string key)
    {
        System.Type type = typeof(T);

        if (type == typeof(Board))
        {
            GameObject go = Managers.Resource.Instantiate(key, pooling: false);
            Board board = go.GetComponent<Board>();

            Board = board;

            return board as T;
        }
        else if (type == typeof(Tile))
        {
            GameObject go = Managers.Resource.Instantiate(key, pooling: false);
            Tile tile = go.GetComponent<Tile>();
            
            Tiles.Add(tile);
            return tile as T;
        }
        else if (type == typeof(Fruit))
        {
            GameObject go = Managers.Resource.Instantiate(key, pooling: true);
            Fruit fruit = go.GetComponent<Fruit>();
            
            Fruits.Add(fruit);
            return fruit as T;
        }

        return null;
    }

    public override void Despawn<T>(T obj)
    {
        System.Type type = typeof(T);

        if (type == typeof(Board))
        {
            Board = null;
            Managers.Resource.Destroy(obj.gameObject);
        }
        else if (type == typeof(Tile))
        {
            Tiles.Remove(obj as Tile);
            Managers.Resource.Destroy(obj.gameObject);
        }
        else if (type == typeof(Fruit))
        {
            Fruits.Remove(obj as Fruit);
            Managers.Resource.Destroy(obj.gameObject);
        }
    }

    public Tile FindSelectedTile()
    {
       return Tiles.FirstOrDefault(tile => tile.Selected);
    }

    public void ClearObjects()
    {
        Board = null;

        var tilesToDespawn = Tiles.ToList();
        foreach (var tile in tilesToDespawn)
        {
            Despawn(tile); // 내부에서 Tiles.Remove(tile) 해도 안전
        }
        Tiles.Clear(); // 혹시 남은 게 있으면 마무리 제거

        var fruitsToDespawn = Fruits.ToList();
        foreach (var fruit in fruitsToDespawn)
        {
            Despawn(fruit); // 마찬가지로 내부 제거 안전
        }
    }
}
