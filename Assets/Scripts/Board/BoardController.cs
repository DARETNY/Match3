using System;
using System.Collections.Generic;
using Game.Cells;
using Game.Utils;
using Game.Input;
using DG.Tweening;
using UnityEngine;

namespace Game.Board
{
    public class BoardController : MonoBehaviour
    {
        private Board _board;

        [SerializeField] private int width, height;
        [SerializeField] private CellView cellViewPrefab;
        [SerializeField] private float cellSize;
        public float CellSize => cellSize;

        [SerializeField] private Transform boardHandle;


        private SpriteRenderer[,] _views;
        public static BoardController Instance { get; private set; }

        private Sequence _waveSequences;
        
        private bool _isSwiping;
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Setup();
            
            UpdateCamera();

            //TryFindMatches();

            UpdateView();
        }

        private void OnEnable()
        {
            PlayerInput.OnSwipe += Swipe;
        }

        private void OnDisable()
        {
            PlayerInput.OnSwipe -= Swipe;
        }
        
        private void Swipe(Vector2 fp, Vector2 lp)
        {
            if (_isSwiping) return;

            Debug.Log("Swiping start");
            
            _isSwiping = true;
            
            var startIndex = fp.ToGridIndex(cellSize);
            var endIndex = lp.ToGridIndex(cellSize);

            if (!startIndex.HasCell(_board) || !endIndex.HasCell(_board) || Equals(startIndex, endIndex)) return;

            // Swap
            _board.Swap(startIndex, endIndex);
            _views.Swap(startIndex, endIndex);

            // View
            var sequence = DOTween.Sequence();
            sequence = UpdateView().Pause();

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
            if (!_waveSequences.IsActive())
            {
                _waveSequences = DOTween.Sequence();
                _waveSequences.Pause();
            }
            
            List<Tuple<int, int>> matches = _board.FindMatches();

            if (matches.Count == 0)
            {
                _waveSequences.OnComplete(() =>
                {
                    Debug.Log("Swiping end");
                    _isSwiping = false;
                });
                _waveSequences.Play();
                
                return false;
            }

            // Explode matches
            List<Tuple<int, int>> movingViewPoints = new();

            var waveSequence = DOTween.Sequence().Pause();
            foreach (var match in matches)
            {
                if (movingViewPoints.Contains(match)) continue;
                
                movingViewPoints.Add(match);
                
                var x = match.Item1;
                var y = match.Item2;
                
                var cellType = GetRandomBasicCellType();
                var cellView =  _views[x, y];

                var defaultScale = cellView.transform.localScale;
                _board.Grid[x, y].CellType = cellType;
                
                var cellTween = cellView.transform
                    .DOScale(cellView.transform.localScale * 1.25f, .5f)
                    .SetEase(Ease.OutCubic)
                    .OnComplete(() =>
                    {
                        // TODO: Add explosion
                        
                        // TODO Poola ekle

                        // if (cellView != null)
                        // {
                        //     cellView.transform.position = Vector3.one * 100000f;
                        //     cellView.color = cellType.GetColor();
                        //     cellView.transform.localScale = defaultScale; // .DOScale(defaultScale, .25f) .SetEase(Ease.OutCubic);
                        // }
                        
                        if (cellView != null)
                        {
                            cellView.color = cellType.GetColor();
                            cellView.transform.DOScale(defaultScale, .25f) .SetEase(Ease.OutCubic);
                        }
                        
                    });
                
                waveSequence.Join(cellTween);
            }

            // waveSequence.AppendCallback(() =>
            // {
            //     FallViews(movingViewPoints);
            // });
            waveSequence.AppendInterval(1f);
            _waveSequences.Append(waveSequence);

            TryFindMatches();

            return true;
        }

        private void FallViews(List<Tuple<int, int>> destroyingCoords)
        {
            foreach (var (x, y) in destroyingCoords)
            {
                // TODO: send to pool
                if (_views[x, y] != null)
                    _views[x, y].gameObject.SetActive(false);
                _views[x, y] = null;
            }

            for (int y = _board.Height - 1; y >= 0; y--)
            {
                for (int x = 0; x < _board.Width; x++)
                {
                    if (_views[x, y] == null)
                    {
                        SetUpperView(x, y);
                    }
                }
            }
        }

        private void SetUpperView(int x, int y)
        {
            var belowX = x;
            var belowY = y;
            SpriteRenderer upperView = null;
            do
            {
                y--;

                if (_views[belowX, y] != null)
                {
                    upperView = _views[belowX, y];
                    _views[belowX, y] = null;
                }
                
            } while (y > 0);

            if (upperView == null)
            {
                upperView = Instantiate(cellViewPrefab).SpriteRenderer;
            }
            
            upperView.transform.position = new Vector3(belowX, -belowY) * cellSize;
            _views[belowX, belowY] = upperView;
            Debug.Log(upperView.transform.position);
        }

        private Sequence UpdateView()
        {
            Sequence sequence = DOTween.Sequence();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var pos = _board.Grid[x, y].Pos();
                    _views[x, y].GetComponent<CellView>().Coordinate.SetText($"{x}, {y}");
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
                    gridTypes[x, y] = GetRandomBasicCellType();

                    // View
                    var view = Instantiate(cellViewPrefab, boardHandle, true);
                    view.transform.position = new Vector3(x, -y) * cellSize;
                    view.SpriteRenderer.color = gridTypes[x, y].GetColor();
                    // DoFade(view, x, y);

                    _views[x, y] = view.SpriteRenderer;
                }
            }

            _board = new Board(width, height, gridTypes, cellSize);
        }

        private CellType GetRandomBasicCellType()
        {
            return (CellType) UnityEngine.Random.Range(CellSettings.MIN_BASIC_CELL_ID, CellSettings.MAX_BASIC_CELL_ID + 1);
        }

        private void DoFade(SpriteRenderer view, int x, int y)
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