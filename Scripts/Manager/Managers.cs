using System;
using CWLib;
using UnityEngine;

public class Managers : Singleton<Managers>
{
    ResourceManager resourceManager = new ResourceManager();
    ObjectManager objectManager = new ObjectManager();
    PoolManager poolManager = new PoolManager();
    UIManager uIManager = new UIManager();
    ScoreManager scoreManager = new ScoreManager();
    FruitManager fruitManager = new FruitManager();
    SoundManager soundManager = new SoundManager();

    public static ResourceManager Resource { get { return Instance.resourceManager; } }
    public static ObjectManager Object { get { return Instance.objectManager; } }
    public static PoolManager Pool { get { return Instance.poolManager; } }
    public static UIManager UI { get { return Instance.uIManager; } }
    public static ScoreManager Score { get { return Instance.scoreManager; } }
    public static FruitManager Fruit { get { return Instance.fruitManager; } }
    public static SoundManager Sound { get { return Instance.soundManager; } }

    private void Start()
    {
        Sound.Init();
    }
}
