﻿using UnityEngine;

namespace PathFind
{
    public class Cell : ICell
    {
        public ICell Parent { get; private set; }
        public Vector2Int Point { get; }
        public bool IsWall { get; private set; }
        public bool IsEntrance { get; private set; }
        public bool IsExit { get; private set; }
        public int Weight { get; private set; }
        public int Heuristic { get; private set; }
        public int Distance { get; private set; }
        public int Summ => (Distance + Heuristic) * Weight;
        //Adding parameters for moving
        public Cell parent = null;
        public bool target = false;

        

        

        public Cell(Vector2Int point)
        {
            Point = point;
            Weight = 1;
        }

        public void SetParent(ICell parent)
        {
            Parent = parent;
        }

        public void SetIsWall(bool isWall)
        {
            IsWall = isWall;
        }

        public void SetIsEntrance(bool isEntrance)
        {
            IsEntrance = isEntrance;
        }

        public void SetIsExit(bool isExit)
        {
            IsExit = isExit;
        }

        public void SetWeight(int weight)
        {
            Weight = 1 + weight;
        }

        public void SetHeuristic(int heuristic)
        {
            Heuristic = heuristic;
        }

        public void SetDistance(int distance)
        {
            Distance = distance;
        }
    }
}
