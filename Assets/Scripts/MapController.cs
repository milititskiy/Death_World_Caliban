using PathFind;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PathFind
{
    public class MapController : MonoBehaviour
    {
        public Action<ICell> OnStartCellSelect = delegate { };
        public Action<ICell> OnEndCellSelect = delegate { };
        public Action<IList<ICell>> OnPathFind = delegate { };
        [SerializeField] private CellSelector m_cellSelector;
        [SerializeField] private CellAssets m_prefabs;
        private int m_mapSizeX;
        private int m_mapSizeY;
        //added the ground size object
        public GameObject mapSizeObject;
        public List<Vector2Int> listOfMapPoints;
        private Vector2Int mapSize;
        private IMap _map;
        public  Dictionary<Vector2Int, CellView> _cellsView;
        private IPathFinder _pathFinder;
        private ICell _cellStart;
        private ICell _cellEnd;

        private void Awake()
        {
            listOfMapPoints = new List<Vector2Int>();
            mapSize = GetMapSizeFromGameObject(mapSizeObject);
            _map = new Map(mapSize.x,mapSize.y);
            GetCells(_map.GetCells());
        }

        private void Start()
        {
            m_cellSelector.OnStartPoint += OnSetPointStart;
            m_cellSelector.OnEndPoint += OnSetPointEnd;
        }

        private void GetCells(IDictionary<Vector2Int,ICell> cells)
        {
            _pathFinder = new PathFinder();
            _cellsView = new Dictionary<Vector2Int,CellView>();
            foreach (var cellPair in cells)
            {
                var point = cellPair.Key;
                var cell = cellPair.Value;

                var prefabItem = m_prefabs.GetRandomPrefab(!cell.IsWall);
                var position = HexCoords.GetHexVisualCoords(point, mapSize);
                //var prefab = Resources.Load("Hex") as GameObject;
                var go = Instantiate(prefabItem.Prefab, position, Quaternion.identity);
                //var go = Instantiate(prefabItem.Prefab, position,Quaternion.Euler(90,0,0));
                go.transform.SetParent(transform);
                var cellView = go.GetComponent<CellView>();
                cellView.SetPoint(point);
                _cellsView[point] = cellView;
                //deactivating cells for experiment
                var renderer =  cellView.GetComponent<MeshRenderer>();
                renderer.enabled = false;
                listOfMapPoints.Add(point);
            }
        }


        public Vector2Int GetMapSize() => new Vector2Int(m_mapSizeX, m_mapSizeY);

        public Vector2Int GetMapSizeFromGameObject(GameObject gameObject)
        {
            m_mapSizeX = Mathf.FloorToInt(gameObject.GetComponent<MeshRenderer>().bounds.size.x);
            m_mapSizeY = Mathf.FloorToInt(gameObject.GetComponent<MeshRenderer>().bounds.size.y);
            return new Vector2Int(m_mapSizeX,m_mapSizeY);
        }

        void OnSetPointStart(Vector2Int point)
        {
            _cellStart = _map.GetCell(point);
            OnStartCellSelect?.Invoke(_cellStart);
            Calculate();
        }

        void OnSetPointEnd(Vector2Int point)
        {
            _cellEnd = _map.GetCell(point);
            OnEndCellSelect?.Invoke(_cellEnd);
            Calculate();
        }

        void Calculate()
        {
            var path = _pathFinder.FindPathOnMap(_cellStart, _cellEnd, _map);
            OnPathFind?.Invoke(path);
        }

        private void OnDestroy()
        {
            m_cellSelector.OnStartPoint -= OnSetPointStart;
            m_cellSelector.OnEndPoint -= OnSetPointEnd;
        }

        public IList<ICell> GetPathCount()
        {
            IList<ICell> path = _pathFinder.FindPathOnMap(_cellStart, _cellEnd, _map);
            return path;
        }

        public IList<ICell> ReversePath(IList<ICell> path)
        {
            List<ICell> listOfCells = new List<ICell>();
            foreach (ICell cell in path)
            {
                listOfCells.Add(cell);

            }
            listOfCells.Reverse();
            return listOfCells as IList<ICell>;
        }
    }
}