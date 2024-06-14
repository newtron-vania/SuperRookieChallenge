using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager
{
    private int _width = 32;
    private int _height = 32;
    private float _cellSize = 0.1f;
    private Vector2 _startPos = Vector2.zero;

    private bool[,] _GridMap;

    public void Init()
    {
        Vector2 startPos = _startPos - new Vector2(_width / 2f, _height / 2f);
        int widthSize = (int)(_width / _cellSize);
        int heightSize = (int)(_height / _cellSize);
        _GridMap = new bool[widthSize, heightSize];
        for (int x = 0; x < widthSize; x++)
        {
            for (int y = 0; y < heightSize; y++)
            {
                _GridMap[x,y] = hasObstacle(startPos + new Vector2(x * _cellSize, y * _cellSize));
            }
        }
    }

    public void OnUpdate()
    {
        
    }

    bool hasObstacle(Vector2 worldPoint)
    {
        return Physics.CheckSphere(worldPoint, _cellSize / 2);;
    }
}
