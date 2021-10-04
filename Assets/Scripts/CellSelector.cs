using PathFind;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PathFind
{
    public class CellSelector : MonoBehaviour
    {
        [SerializeField] private Camera m_camera;


        public Action<Vector2Int> OnStartPoint = delegate { };
        public Action<Vector2Int> OnEndPoint = delegate { };

        public delegate void MouseClick(IList<ICell> cells);
        public MouseClick OnMouseClick;

        private GameObject _player;
        private Vector2Int _endPoint;
        public Vector2Int _startPoint;
        private Vector2Int _mapSize;
        private Vector3 _playerPos;
        public IList<ICell> _path;
        public float speed = 0.25f;
        float s;

        private IPathFinder _pathFinder;

        List<Vector2Int> moveList = new List<Vector2Int>();
        List<CellView> activatedCells = new List<CellView>();
        private bool _isNotMoving;

        enum MouseButton
        {
            None = 0,
            Left = 1,
            Right = 2
        }

        private void Awake()
        {
            _isNotMoving = true;
            _mapSize = GetComponent<MapController>().GetMapSize();
        }

        private void Start()
        {
            LoadPlayer();
        }

        private void Update()
        {
            s = Time.deltaTime * speed;

            OnMouseOver();
            ButtonClick();
            //Debug.Log(_isNotMoving);
        }







        private void ButtonClick()
        {
            var mouseButton = Input.GetMouseButtonDown(0) ? MouseButton.Left : Input.GetMouseButtonDown(1) ? MouseButton.Right : MouseButton.None;
            if (mouseButton != MouseButton.None)
            {
                var ray = m_camera.ScreenPointToRay(Input.mousePosition);
                var cell = Raycast(ray);

                if (cell != null)
                {
                    if (mouseButton == MouseButton.Left)
                    {
                        OnStartPoint?.Invoke(_endPoint);
                        StartCoroutine(MovePlayerToPoint(_path));
                        //GetNeighBours(_path,1);

                    }
                }
                if(mouseButton == MouseButton.Right)
                {
                    GetNeighBours(_startPoint, 1);
                }
            }
        }

        private CellView Raycast(Ray ray)
        {
            var result = default(CellView);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                result = hit.transform.GetComponent<CellView>();
            }
            return result;
        }

        private void OnMouseOver()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var cell = Raycast(ray);

            if (cell != null)
            {
                _path = GetComponent<MapController>().GetPathCount();
                _endPoint = cell.GetPoint();
                OnEndPoint?.Invoke(_endPoint);
            }

        }

        void LoadPlayer()
        {
            OnStartPoint.Invoke(_startPoint);
            _path = GetComponent<MapController>().GetPathCount();
            _playerPos = Vector3Coords(_startPoint);
            _player = GameObject.FindGameObjectWithTag("Player");
            _player.transform.localPosition = _playerPos;
        }

          IEnumerator  MovePlayerToPoint(IList<ICell> _path)
        {
            var list = ReversePath(_path);
            for (int i = 1; i < list.Count; i++)
            {
                var a = Vector3Coords(list[i - 1].Point);
                var b = Vector3Coords(list[i].Point);
                for (float t = 0f; t < 1f; t += s)
                {
                    _player.transform.localPosition = Vector3.Lerp(a, b, t);
                    yield return null;
                }
                if (_endPoint == list[i].Point) {
                    _startPoint = _endPoint;
                    _isNotMoving = true;
                    // DeactivateCells(activatedCells);
                }

            }
        }
         void OnTriggerExit(Collider other)
        {
            Destroy(other.gameObject);
        }

        void GetNeighBours(IList<ICell> icells, int n)
        {
            var reversed = ReversePath(icells);
            var minOpened = reversed[reversed.Count - 1];

            if (minOpened.Point.y % 2 != 0)
            {
                var n1 = new Vector2Int(minOpened.Point.x, minOpened.Point.y - n);
                var n2 = new Vector2Int(minOpened.Point.x + n, minOpened.Point.y - n);
                var n3 = new Vector2Int(minOpened.Point.x - n, minOpened.Point.y);
                var n4 = new Vector2Int(minOpened.Point.x + n, minOpened.Point.y);
                var n5 = new Vector2Int(minOpened.Point.x, minOpened.Point.y + n);
                var n6 = new Vector2Int(minOpened.Point.x + n, minOpened.Point.y + n);
                moveList.Add(n1);
                moveList.Add(n2);
                moveList.Add(n3);
                moveList.Add(n4);
                moveList.Add(n5);
                moveList.Add(n6);
            }
            else
            {
                var n1 = new Vector2Int(minOpened.Point.x - n, minOpened.Point.y - n);
                var n2 = new Vector2Int(minOpened.Point.x, minOpened.Point.y - n);
                var n3 = new Vector2Int(minOpened.Point.x - n, minOpened.Point.y);
                var n4 = new Vector2Int(minOpened.Point.x + n, minOpened.Point.y);
                var n5 = new Vector2Int(minOpened.Point.x - n, minOpened.Point.y + n);
                var n6 = new Vector2Int(minOpened.Point.x, minOpened.Point.y + n);
                moveList.Add(n1);
                moveList.Add(n2);
                moveList.Add(n3);
                moveList.Add(n4);
                moveList.Add(n5);
                moveList.Add(n6);
            }

        }

        void GetNeighBours(Vector2Int playerStarPoint, int n)
        {
            var minOpened = playerStarPoint;

            if (minOpened.y % 2 != 0)
            {
                var n1 = new Vector2Int(minOpened.x, minOpened.y - n);
                var n2 = new Vector2Int(minOpened.x + n, minOpened.y - n);
                var n3 = new Vector2Int(minOpened.x - n, minOpened.y);
                var n4 = new Vector2Int(minOpened.x + n, minOpened.y);
                var n5 = new Vector2Int(minOpened.x, minOpened.y + n);
                var n6 = new Vector2Int(minOpened.x + n, minOpened.y + n);
                moveList.Add(n1);
                moveList.Add(n2);
                moveList.Add(n3);
                moveList.Add(n4);
                moveList.Add(n5);
                moveList.Add(n6);
            }
            else
            {
                var n1 = new Vector2Int(minOpened.x - n, minOpened.y - n);
                var n2 = new Vector2Int(minOpened.x, minOpened.y - n);
                var n3 = new Vector2Int(minOpened.x - n, minOpened.y);
                var n4 = new Vector2Int(minOpened.x + n, minOpened.y);
                var n5 = new Vector2Int(minOpened.x - n, minOpened.y + n);
                var n6 = new Vector2Int(minOpened.x, minOpened.y + n);
                moveList.Add(n1);
                moveList.Add(n2);
                moveList.Add(n3);
                moveList.Add(n4);
                moveList.Add(n5);
                moveList.Add(n6);
            }

            var _cellsView = GetComponent<MapController>()._cellsView;
            foreach (var cellPair in _cellsView)
            {
                var point = cellPair.Key;
                var cell = cellPair.Value;
                foreach (var item in moveList)
                {
                    if(item == point)
                    {
                        var cellRenderer = cell.GetComponent<MeshRenderer>();
                        cellRenderer.enabled = true;
                        //activatedCells.Add(cell);
                    }
                }
            }

        }

        void DeactivateCells(List<CellView> cells)
        {
            //foreach (var cell in cells)
            //{
            //    cell.gameObject.SetActive(false);
            //}
            if (_isNotMoving)
            {
                Debug.Log(_isNotMoving);
            }
        }


        private void OnDrawGizmos()
        {
            for (int i = 0; i < moveList.Count; i++)
            {
                var vec3 = Vector3Coords(moveList[i]);
                Gizmos.DrawSphere(vec3, 0.2f);
            }
        }

        private IList<ICell> ReversePath(IList<ICell> path)
        {
            List<ICell> listOfCells = new List<ICell>();
            foreach (ICell cell in path)
            {
                listOfCells.Add(cell);
            }
            listOfCells.Reverse();
            return listOfCells as IList<ICell>;
        }


        private Vector3 Vector3Coords(Vector2Int _point)
        {
            Vector3 coords = HexCoords.GetHexVisualCoords(_point, _mapSize);
            return coords;
        }

        //private Vector2Int GetMapSize()
        //{
        //    var mapSizeObject = GetComponent<MapController>().mapSizeObject;
        //    var mapSize = GetComponent<MapController>().GetMapSizeFromGameObject(mapSizeObject);
        //    return mapSize;
        //}

        //private  List<Vector2Int> ConvertPathToVecto2IntAndReverse(IList<ICell> path){
        //    List<Vector2Int> pointsList = new List<Vector2Int>();
        //    foreach (var item in path)
        //    {
        //        pointsList.Add(item.Point);
        //    }
        //    pointsList.Reverse();
        //    return pointsList;
        //}

    }
}
