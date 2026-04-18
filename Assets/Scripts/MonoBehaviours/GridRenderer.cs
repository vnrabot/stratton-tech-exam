using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridRenderer : MonoBehaviour
{
    [Header("Local Dependencies")]
    [SerializeField] private Tilemap _tileMap;
    [SerializeField] private TileBase _aliveTile;
    [SerializeField] private Color _currentColor = Color.red;
    private IGameState _gameState;
    private HashSet<Vector2Int> _previousAliveCells = new HashSet<Vector2Int>();

    //Called by master controller to inject the data.
    public void Setup(IGameState gameState)
    {
        _gameState = gameState;
    }

    public void UpdateVisuals()
    {
        if (_gameState == null) return;

        //Clear canvas for new frame
        _tileMap.ClearAllTiles();

        foreach (Vector2Int ghost in _previousAliveCells)
        {
            Vector3Int pos = new Vector3Int(ghost.x, ghost.y, 0);
            _tileMap.SetTile(pos, _aliveTile);
            _tileMap.SetTileFlags(pos, TileFlags.None);

            Color ghostColor = new Color(_currentColor.r, _currentColor.g, _currentColor.b, 0.3f);
            _tileMap.SetColor(pos, ghostColor);
        }

        _previousAliveCells.Clear();

        foreach (Vector2Int cellPosition in _gameState.GetAliveCells())
        {
            Vector3Int pos = new Vector3Int(cellPosition.x, cellPosition.y, 0);
            _tileMap.SetTile(pos, _aliveTile);

            _tileMap.SetTileFlags(pos, TileFlags.None);
            _tileMap.SetColor(pos, _currentColor);

            _previousAliveCells.Add(cellPosition);
        }

        _tileMap.color = _currentColor;
    }

    public void SetTileColor(Color newColor)
    {
        _currentColor = newColor;
        _tileMap.color = newColor;
    }

    public void SetCellScale(float scale)
    {
        _tileMap.transform.localScale = new Vector3(scale, scale, 1f);
    }
}
