using UnityEngine;
using UnityEngine.Tilemaps;

public class GridRenderer : MonoBehaviour
{
    [Header("Local Dependencies")]
    [SerializeField] private Tilemap _tileMap;
    [SerializeField] private TileBase _aliveTile;

    private IGameState _gameState;

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

        //Draw cells that are currently alive
        foreach (var cell in _gameState.GetAliveCells())
        {
            //Covnert 2d coordinates to 3D Tilemap
            Vector3Int tilePosition = new Vector3Int(cell.x, cell.y, 0);

            _tileMap.SetTile(tilePosition, _aliveTile);
        }
    }
}
