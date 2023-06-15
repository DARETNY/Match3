using System;
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
        [SerializeField] private float cellsize;
        [SerializeField] private Transform boardHandle;


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
            var widthCellCount = width / cellsize;
            var heightCellCount = height / cellsize;

            fp += new Vector2(.5f, .5f);
            
            var xStart = Mathf.Abs(Mathf.FloorToInt(fp.x / cellsize));
            var yStart = Mathf.Abs(Mathf.FloorToInt(fp.y / cellsize));
            
            var firstSelectedCell = _board.Grid[(int)xStart, (int)yStart];
            
            var xEnd = Mathf.Abs(Mathf.FloorToInt(lp.x / cellsize));
            var yEnd = Mathf.Abs(Mathf.FloorToInt(lp.y / cellsize));
            
            var lastSelectedCell = _board.Grid[(int)xEnd, (int)yEnd];
            
            Debug.Log($"{xStart} {yStart}");
            Debug.Log($"{xEnd} {yEnd}");
        }


        void Setup()
        {
            CellType[,] gridTypes = new CellType[width, height];


            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    gridTypes[x, y] = (CellType)Random.Range(1, (int) Enum.GetValues(typeof(CellType)).Cast<CellType>().Max());
                 
                    // View
                    var view = Instantiate(cellViewprefab, boardHandle, true);
                    view.transform.position = new Vector3(x, -y) * cellsize;
                    view.color = gridTypes[x, y].GetColor();
                    DoFade(view, x, y);
                }
            }

            _board = new Board(width, height, gridTypes, cellsize);
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
            var centerOffset = ((_board.Grid.Length % 2) - 1) * cellsize / 2f;

            bounds.center = new Vector3(centerGrid.X, centerGrid.Y);
            var topLeftCellPos = _board.Grid[0, 0].Pos();
            var bottomRightCellPos = _board.Grid[width - 1, height - 1].Pos();


            bounds.size = new Vector3(Mathf.Abs((bottomRightCellPos.x - topLeftCellPos.x) + cellsize + centerOffset),
                Mathf.Abs((topLeftCellPos.y - bottomRightCellPos.y) + cellsize + centerOffset), 1);
            Camera.main.SetOrtho(bounds);
        }
    }
}
