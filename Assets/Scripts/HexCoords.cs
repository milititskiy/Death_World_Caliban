using UnityEngine;


namespace PathFind
{
    public static class HexCoords
    {
        public const float CellHeight = 0.866025403784435f;
        public static MapController mapController = new MapController();

        public static Vector3 GetHexVisualCoords(Vector2Int point, Vector2Int mapSize)
        {
            var shift = point.y % 2 == 0 ? 0 : 0.5f;
            var x = point.x + shift - ((float)mapSize.x / 2) + 0.25f;
            var y = point.y * CellHeight - (mapSize.y * CellHeight / 2f);
            return new Vector3( x, 0, y);
        }


        public static Vector3 GetHexVisualCoords(Vector2Int point)
        {
            var _mapSize = mapController.GetMapSize();
            Vector3 coords = GetHexVisualCoords(point, _mapSize);
            return coords;
        }
    }
}