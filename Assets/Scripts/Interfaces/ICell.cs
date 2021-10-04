using UnityEngine;

namespace PathFind
{
    public interface ICell
    {
        ICell Parent { get; }
        Vector2Int Point { get; }
        bool IsWall { get; }
        bool IsEntrance { get; }
        bool IsExit { get; }
        int Weight { get; }
        int Heuristic { get; }
        int Distance { get; }
        int Summ { get; }
        

        void SetParent(ICell parent);
        void SetIsWall(bool isWall);
        //added entrance and exit cell
        void SetIsEntrance(bool isEntrance);
        void SetIsExit(bool isExit);
        void SetWeight(int weight);
        void SetHeuristic(int heuristic);
        void SetDistance(int distance);
    }
}