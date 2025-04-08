using System;
using System.Collections;
using CWLib;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameScene : MonoBehaviour
{
    private Vector3 _dragOffset;
    private Camera _mainCamera;
    private bool _isDragging = false;
    private Fruit _selectedFruit;

    public int GameScore { get; set; }
    public bool IsGameOver = false;
    
    // TODO 나중에 UI로 
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private float gameTime;
    
    private bool IsWarning = false;
    private Sequence _seq;
    
    
    private void Awake()
    {
        Managers.Resource.LoadAllAsync<Object>("Preload", (key, count, totalCount) =>
        {
            Debug.Log($"{key} {count}/{totalCount}");

            if (count == totalCount)
            {
                StartLoad();
            }
        });
        

        Init();
    }

    private void Update()
    {
        if(IsGameOver) return;
        
        DragFruit();
        UpdateTimeText();
    }

    private void Init()
    {
        _mainCamera = Camera.main;

    }
    

    private void StartLoad()
    {
        Managers.Object.Spawn<Board>(ResourcePaths.Board);
        
        Managers.Sound.Play(BaseDefine.Sound.Bgm, "ArcadeGameBGM", 1f, .5f);
    }

    private void DragFruit()
    {
        Vector3 inputPosition = Vector3.zero;
        bool isInputDown = false;
        bool isInputHold = false;
        bool isInputUp = false;

        // PC (마우스) 입력 처리
        if (Input.GetMouseButtonDown(0))
        {
            isInputDown = true;
            inputPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            isInputHold = true;
            inputPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isInputUp = true;
        }

        // 모바일 (터치) 입력 처리
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            inputPosition = touch.position;

            if (touch.phase == TouchPhase.Began) isInputDown = true;
            if (touch.phase == TouchPhase.Moved) isInputHold = true;
            if (touch.phase == TouchPhase.Ended) isInputUp = true;
        }

        // 드래그 시작
        if (isInputDown)
        {
            Vector3 worldPos = _mainCamera.ScreenToWorldPoint(inputPosition);
            worldPos.z = 0f; // Z값 보정
            Collider2D hitCollider = Physics2D.OverlapPoint(worldPos);

            if (hitCollider != null)
            {
                Fruit fruit = hitCollider.GetComponentInParent<Fruit>();
                if (fruit != null)
                {
                    _selectedFruit = fruit;
                    _isDragging = true;

                    Vector3 worldMousePos = _mainCamera.ScreenToWorldPoint(new Vector3(
                        inputPosition.x, inputPosition.y, _mainCamera.WorldToScreenPoint(fruit.transform.position).z
                    ));
                    
                    _dragOffset = fruit.transform.position - worldMousePos;
                }
            }
        }

        // 드래그 중
        if (isInputHold && _isDragging && _selectedFruit != null)
        {
            _selectedFruit.IsDragging = true;
            Vector3 newPosition = _mainCamera.ScreenToWorldPoint(inputPosition);
            newPosition.z = 0f; // Z값 보정
            _selectedFruit.transform.position = newPosition + _dragOffset;
        }

        // 드래그 종료
        if (isInputUp)
        {
            if (_isDragging && _selectedFruit != null)
            {
                MergeFruit();
            }
            _isDragging = false;
            _selectedFruit = null;
        }
    }

    private void MergeFruit()
    {
        Vector2 checkPoint = _selectedFruit.transform.position;
                
        // 모든 콜라이더 중에서 Fruit 레이어만 가져옴
        Collider2D[] hits = Physics2D.OverlapPointAll(checkPoint, LayerMask.GetMask("Fruit"));
                
        foreach (var hit in hits)
        {
            if (hit == null)
            {
                ReturnFruit(_selectedFruit);
                continue;
            }
            
            Fruit hitFruit = hit.GetComponentInParent<Fruit>();
                    
            // 자기 자신 무시
            if (hitFruit != null && hitFruit == _selectedFruit)
            {
                ReturnFruit(_selectedFruit);
                continue;
            }
                
            if (hitFruit != null && _selectedFruit.GetGrade() == hitFruit.GetGrade())
            {
                if (_selectedFruit.GetGrade() == 8)
                {
                    ReturnFruit(_selectedFruit);
                    break;
                }
                
                Tile selectedFruitTile = _selectedFruit.CurrentTile;
                Tile hitFruitTile = hitFruit.CurrentTile;
                
                Managers.Object.Despawn(_selectedFruit);
                Managers.Object.Despawn(hitFruit);

                // update Score
                GameScore += hitFruit.GetGrade();
                UpdateGameScore();
                
                // create new Fruit
                StartCoroutine(DelayedCreateFruit(selectedFruitTile));
                Managers.Fruit.CreateNextGradeFruit(hitFruit.GetGrade(), hitFruitTile);
                
                // sound
                Managers.Sound.Play(BaseDefine.Sound.Effect, "MergeSFX", 1f, 1f);
                
                
                break;
            }
        }
    }
    
    private IEnumerator DelayedCreateFruit(Tile tile)
    {
        yield return new WaitForSeconds(3f);
        Managers.Fruit.CreateFruit(tile);
    }

    private void ReturnFruit(Fruit fruit)
    {
        fruit.transform.position = fruit.CurrentTile.transform.position;
    }

    private void UpdateGameScore()
    {
        _scoreText.text = GameScore.ToString();
    }

    private void UpdateTimeText()
    {
        gameTime -= Time.deltaTime;
        _timeText.text = "Time : " + gameTime.ToString("F0");

        if (gameTime <= 0)
        {
            GameOver();

            return;
        }
        
        if (gameTime <= 10 && !IsWarning)
        {
            IsWarning = true;
            _timeText.color = Color.red;
            
            _seq = DOTween.Sequence();
            _seq.Append(_timeText.transform.DOScale(1.3f, 0.15f).SetEase(Ease.OutBack));
            _seq.Append(_timeText.transform.DOScale(1f, 0.15f).SetEase(Ease.InBack));
            _seq.SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void GameOver()
    {
        IsGameOver = true;
        gameTime = 0;
        _seq.Kill();
        _timeText.color = Color.white;
        Managers.UI.ShowPopup<GameOverPopup>();
    }
}
