using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFind
{
    public class PlayerController : MonoBehaviour
    {
        private float _speed = 4f;
        private IList<ICell> _path;
        private Vector2Int _startPoint;
        private IPathFinder _pathFinder;
        private ICell location;
        private float tF;
        private GameObject _player;
        private Vector2Int _mapSize;
        private Vector3 _playerPos;

        [SerializeField] private CellSelector m_cellSelector;


        private void Start()
        {
            //_player = GameObject.FindGameObjectWithTag("Player");
            //var playerPos = Vector3Coords(_startPoint);
            //_player.transform.localPosition = playerPos;
            m_cellSelector.OnMouseClick += SetPointToMove;
            LoadPlayer();
        }

        private void Update()
        {
            tF += Time.deltaTime * _speed;
            _path = GetComponent<CellSelector>()._path;
            //Debug.Log(_path.Count);
            
        }

        public IEnumerator OnPlayerMove(IList<ICell> path)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            var list = ReversePath(path);
            for (int i = 1; i < list.Count; i++)
            {
                Vector3 a = Vector3Coords(list[i - 1].Point);
                Vector3 b = Vector3Coords(list[i].Point);
                for (float t = 0f; t < 1f; t = Time.deltaTime * _speed)
                {
                    //_player.transform.localPosition = Vector3.Lerp(a,b, t);
                    var child = _player.GetComponentInChildren<Transform>().GetChild(0);
                    Debug.Log(child.name);
                    //child.GetComponent<MeshRenderer>().material.color = Color.black;
                    child.transform.localPosition = Vector3.Lerp(a, b, t);
                    yield return null;
                }
            }
        }

        public void SetPointToMove(IList<ICell> path)
        {
            StartCoroutine(OnPlayerMove(path));
        }

        private void OnEnable()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _startPoint = GetComponent<CellSelector>()._startPoint;
            var _playerPos = Vector3Coords(_startPoint);
            _player.transform.localPosition = _playerPos;
        }



        private Vector3 Vector3Coords(Vector2Int _point)
        {
            Vector2Int m = new Vector2Int(10, 10);
            Vector3 coords = HexCoords.GetHexVisualCoords(_point, m);
            return coords;
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

        void LoadPlayer()
        {
            _path = GetComponent<MapController>().GetPathCount();
            _playerPos = Vector3Coords(_startPoint);
            _player = GameObject.FindGameObjectWithTag("Player");
            _player.transform.position = _playerPos;
        }

        private void OnDestroy()
        {
            m_cellSelector.OnMouseClick -= SetPointToMove;
        }
    }

}

