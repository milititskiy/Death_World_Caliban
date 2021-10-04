using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PathFind
{
    public class LoadPlayer : MonoBehaviour
    {
        public Vector2Int playerPoint;
        public Vector3 playerPos;

        public GameObject loadPrimitivePlayer()
        {
            var mapSizeObject = GetComponent<MapController>().mapSizeObject;
            var mapSize = GetComponent<MapController>().GetMapSizeFromGameObject(mapSizeObject);
            var point = GetComponent<MapController>().listOfMapPoints[0];
            Vector3 position = HexCoords.GetHexVisualCoords(point, mapSize);
            playerPoint = point;
            GameObject gop = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gop.GetComponent<MeshRenderer>().material.color = Color.green;
            gop.name = "Player";
            gop.tag = "Player";
            gop.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            gop.transform.position = new Vector3(position.x, 0.55f, position.z);
            return gop;
        }
    }

}
