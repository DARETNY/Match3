using System;
using System.Collections.Generic;
using System.Linq;
using CameraHandle;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class BoardController : MonoBehaviour
    {
        private Board _board;

        [SerializeField] private int width, height;
        [SerializeField] private SpriteRenderer cellViewprefab;
        [SerializeField] private float cellSize;
        [SerializeField] private Transform boardHandle;


        private SpriteRenderer[,] _views;
        
        private void Start()
        {
            Setup();
            UpdateCamera();

            PlayerInput.OnSwipe += Swipe;
        }

        private void OnDestroy()
        {
            PlayerInput.OnSwipe -= Swipe;
        }

        private void Swipe(Vector2 fp, Vector2 lp)
        {
            var startIndex = fp.ToGridIndex(cellSize);
            var endIndex = lp.ToGridIndex(cellSize);

            if (!startIndex.HasCell(_board) || !endIndex.HasCell(_board) || Equals(startIndex, endIndex)) return;

            // Swap
            _board.Swap(startIndex, endIndex);
            _views.Swap(startIndex, endIndex);
            
            // View
            var sequence = DOTween.Sequence().Pause();
            sequence = UpdateView();

            // Find match
            if (!TryFindMatches())
            {
                // Reverse swap
                _board.Swap(startIndex, endIndex);
                _views.Swap(startIndex, endIndex);
                
                // View
                sequence.Append(UpdateView());
            }

            sequence.Play();
        }

        private bool TryFindMatches()
        {
            List<Tuple<int, int>> matches = _board.FindMatches();
            
            if (matches.Count == 0)
            {
                return false;
            }

            // Explode matches
            foreach (var match in matches)
            {
                var x = match.Item1;
                var y = match.Item2;
                    
                var cellType = GetRandomCellType();
                var defaultScale = _views[x, y].transform.localScale;
                _board.Grid[x, y].CellType = cellType;
                    
                _views[x, y].transform
                    .DOScale(_views[x, y].transform.localScale * 1.25f, .5f)
                    .SetEase(Ease.OutCubic)
                    .OnComplete(() =>
                    {
                        _views[x, y].color = cellType.GetColor();
                        _views[x, y].transform
                            .DOScale(defaultScale, .25f)
                            .SetEase(Ease.OutCubic);
                    });
            }
            
            Invoke(nameof(TryFindMatches), 2f);
            
            return true;
        }

        private Sequence UpdateView()
        {
            Sequence sequence = DOTween.Sequence();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var pos = _board.Grid[x, y].Pos();
                    sequence.Join(_views[x, y].transform.DOMove(pos, .5f));
                }
            }
            return sequence;
        }


        private void Setup()
        {
            var gridTypes = new CellType[width, height];
            _views = new SpriteRenderer[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    gridTypes[x, y] = GetRandomCellType();
                 
                    // View
                    var view = Instantiate(cellViewprefab, boardHandle, true);
                    view.transform.position = new Vector3(x, -y) * cellSize;
                    view.color = gridTypes[x, y].GetColor();
                    DoFade(view, x, y);

                    _views[x, y] = view;
                }
            }

            _board = new Board(width, height, gridTypes, cellSize);
        }

        private CellType GetRandomCellType()
        {
            return (CellType)Random.Range(1, (int) Enum.GetValues(typeof(CellType)).Cast<CellType>().Max());
        }

        private  void DoFade(SpriteRenderer view, int x, int y)
        {
            var realViewColor = view.color;
            var realViewScale = view.transform.localScale;
            var sequence = DOTween.Sequence();

            sequence.Append(view.transform.DOScale(realViewScale * 1.3f, .5f).SetEase(Ease.OutQuad));
            sequence.Join(view.DOColor(Color.green, .5f).SetEase(Ease.OutQuad));
            sequence.Append(view.transform.DOScale(realViewScale, .5f).SetEase(Ease.InQuad));
            sequence.Join(view.DOColor(realViewColor, .5f).SetEase(Ease.InQuad));

            sequence.SetDelay((x + y) * .1f).SetAutoKill(true);
        }

        private void UpdateCamera()
        {
            Bounds bounds = new Bounds();

            var centerGrid = _board.Grid[(width - 1) / 2, (height - 1) / 2];
            var centerOffset = ((_board.Grid.Length % 2) - 1) * cellSize / 2f;

            bounds.center = new Vector3(centerGrid.X, centerGrid.Y);
            var topLeftCellPos = _board.Grid[0, 0].Pos();
            var bottomRightCellPos = _board.Grid[width - 1, height - 1].Pos();


            bounds.size = new Vector3(Mathf.Abs((bottomRightCellPos.x - topLeftCellPos.x) + cellSize + centerOffset),
                Mathf.Abs((topLeftCellPos.y - bottomRightCellPos.y) + cellSize + centerOffset), 1);
            CameraUtils.MainCamera.SetOrthographicSize(bounds);
        }
    }
}
