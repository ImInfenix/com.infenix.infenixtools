using UnityEngine;

namespace InfenixTools.DataStructures
{
    public class GenericGrid<T>
    {
        protected readonly int width;
        protected readonly int height;
        protected readonly float cellSize;
        protected readonly T[,] gridContent;

        public GenericGrid(int width, int height, float cellSize, T initialValue)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;

            gridContent = new T[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    gridContent[i, j] = initialValue;
                }
            }
        }

        public int Width => width;

        public int Height => height;

        public float CellSize => cellSize;

        public virtual void Set(int x, int y, T value)
        {
            gridContent[x, y] = value;
        }

        public virtual void Set(Vector2Int position, T value)
        {
            gridContent[position.x, position.y] = value;
        }

        public virtual T Get(int x, int y)
        {
            return gridContent[x, y];
        }

        public virtual T Get(Vector2Int position)
        {
            return Get(position.x, position.y);
        }

        public bool IsPositionInGrid(int x, int y)
        {
            return IsPositionInGrid(new Vector2Int(x, y));
        }

        public bool IsPositionInGrid(Vector2Int position)
        {
            return position.x >= 0 && position.x < width && position.y >= 0 && position.y < height;
        }
    }
}
