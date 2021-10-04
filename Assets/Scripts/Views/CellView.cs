using UnityEngine;

namespace PathFind
{
    public class CellView : MonoBehaviour
    {
        private Vector2Int _point;
        private bool _mouseOver;

        public void SetPoint(Vector2Int point)
        {
            _point = point;
        }

        public Vector2Int GetPoint() => _point;

        public void SetMouseOver(bool mouseOver)
        {
            _mouseOver = mouseOver;
        }

        //public bool GetMouseOver() => _mouseOver;

        //private void OnMouseEnter()
        //{
        //    GameObject gameObject = GetComponent<CellView>().gameObject;
        //    Transform go = gameObject.transform.GetChild(0);
        //    go.GetComponent<Renderer>().material.color = Color.green;
        //}

        //private void OnMouseExit()
        //{
        //    GameObject gameObject = GetComponent<CellView>().gameObject;
        //    Transform go = gameObject.transform.GetChild(0);
        //    go.GetComponent<Renderer>().material.color = Color.white;
        //}
    }
}
