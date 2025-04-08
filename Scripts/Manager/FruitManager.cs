using UnityEngine;

public class FruitManager 
{
    public string GetFruitByGrade(int grade)
    {
        switch (grade)
        {   
            case 1:
                return ResourcePaths.Apple; 
                break;
            case 2:
                return ResourcePaths.Orange; 
                break;
            case 3:
                return ResourcePaths.Kiwi; 
                break;
            case 4:
                return ResourcePaths.Paprika; 
                break;
            case 5:
                return ResourcePaths.BlueOrange; 
                break;
            case 6:
                return ResourcePaths.Strawberry; 
                break;
            case 7:
                return ResourcePaths.WaterMelon; 
                break;
            case 8:
                return ResourcePaths.BlueStar; 
                break;
            default:
                return "error";
        }
    }

    public void CreateFruit(Tile tile)
    {
        int randomNum = Random.Range(1, 5);
        string fruitName = Managers.Fruit.GetFruitByGrade(randomNum);
        Fruit fruit = Managers.Object.Spawn<Fruit>(fruitName);
        fruit.PlayAnimation("AppearFruit");
        tile.SetFruit(fruit);
    }
    
    public void CreateNextGradeFruit(int grade, Tile tile)
    {
        int randomNum = grade + 1;
        string fruitName = Managers.Fruit.GetFruitByGrade(randomNum);
        Fruit fruit = Managers.Object.Spawn<Fruit>(fruitName);
        fruit.PlayAnimation("AppearFruit");
        tile.SetFruit(fruit);
        
    }
}
