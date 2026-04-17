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

        // //Draw cells that are currently alive
        // foreach (var cell in _gameState.GetAliveCells())
        // {
        //     //Covnert 2d coordinates to 3D Tilemap
        //     Vector3Int tilePosition = new Vector3Int(cell.x, cell.y, 0);
        //     _tileMap.SetTile(tilePosition, _aliveTile);

        //     _tileMap.SetTileFlags(tilePosition, TileFlags.None);
        //     _tileMap.SetColor(tilePosition, _currentColor);
        // }

        // var aliveCells = _gameState.GetAliveCells().ToList();
        // foreach (var cell in aliveCells)
        // {
        //     _tileMap.SetTile(new Vector3Int(cell.x, cell.y, 0), _aliveTile);
        // }

        // // Pass 2: now apply color AFTER all neighbor refreshes are done
        // foreach (var cell in aliveCells)
        // {
        //     Vector3Int pos = new Vector3Int(cell.x, cell.y, 0);
        //     _tileMap.SetTileFlags(pos, TileFlags.None);
        //     _tileMap.SetColor(pos, _currentColor);
        //     _tileMap.RefreshTile(pos);
        // }

        foreach (Vector2Int cellPosition in _gameState.GetAliveCells())
        {
            _tileMap.SetTile(new Vector3Int(cellPosition.x, cellPosition.y, 0), _aliveTile);
        }

        // 3. THE FIX: Force Unity 6 to re-apply the global tint to the new tiles
        _tileMap.color = _currentColor;
    }

    public void SetTileColor(Color newColor)
    {
        _currentColor = newColor;
        _tileMap.color = newColor;
    }
}
