using System;
using System.Collections;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public bool IsDragging { get; set; } = false;
    public Tile CurrentTile { get; set; }
    
    [SerializeField] private int _grade;
    private Animator _animator;

    public int GetGrade() => _grade;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(PlayRandomIdleAnimation());
    }

    public string GetGradeFruit()
    {
        switch (_grade)
        {   
            case 1:
                return ResourcePaths.Apple; 
            case 2:
                return ResourcePaths.Orange; 
            case 3:
                return ResourcePaths.Kiwi; 
            case 4:
                return ResourcePaths.Paprika; 
            case 5:
                return ResourcePaths.BlueOrange; 
            case 6:
                return ResourcePaths.Strawberry; 
            case 7:
                return ResourcePaths.WaterMelon; 
            case 8:
                return ResourcePaths.BlueStar; 
            default:
                return "error";
        }
    }

    public void PlayAnimation(string animKey)
    {
        _animator.Play(animKey);
    }

    private IEnumerator PlayRandomIdleAnimation()
    {
        while (true)
        {
            int randomDuration = UnityEngine.Random.Range(5, 10);
        
            int randomIdleState = UnityEngine.Random.Range(0, 3);
            
            _animator.SetFloat("IdleState", randomIdleState);
            
            yield return new WaitForSeconds(randomDuration);
        }
    }
    
}
