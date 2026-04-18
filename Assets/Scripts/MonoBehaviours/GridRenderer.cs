using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridRenderer : MonoBehaviour
{
    [Header("Local Dependencies")]
    [SerializeField] private Tilemap _tileMap;
    [SerializeField] private TileBase _aliveTile;
    private IGameState _gameState;
    [SerializeField] private Color _currentColor = Color.red;

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

        foreach (Vector2Int cellPosition in _gameState.GetAliveCells())
        {
            _tileMap.SetTile(new Vector3Int(cellPosition.x, cellPosition.y, 0), _aliveTile);
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
