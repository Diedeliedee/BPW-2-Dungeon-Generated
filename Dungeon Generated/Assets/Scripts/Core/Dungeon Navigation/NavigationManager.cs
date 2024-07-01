using System.Collections.Generic;
using UnityEngine;

public partial class NavigationManager
{
    public Dictionary<Vector2Int, Tile> tileMap;

    public void RegisterTileMap(Dictionary<Vector2Int, Tile> _map)
    {
        tileMap = _map;
    }

    public void RegisterEntities(List<Entity> _entities)
    {
        //  Registering the entities.
        for (int i = 0; i < _entities.Count; i++)
        {
                        //  Registering to a tile.
            if (!tileMap.TryGetValue(_entities[i].coordinates, out Tile _tile))
            {
                Debug.LogWarning($"Warning: Entity: {_entities[i].gameObject.name} is standing outside of bounds, collision disables.");
            }
            else
            {
                tileMap[_entities[i].coordinates].occupation = _entities[i];
            }

            //  Snap entities if they're not aligned on thegrid in editor.
            _entities[i].Snap();
        }
    }

    public bool RequestMoveTo(MovementRequest _request, out MovementCallBack _callback)
    {
        _callback = new();

        var entityInBounds = tileMap.TryGetValue(_request.originTile, out Tile _currentTile);
        var targetInBounds = tileMap.TryGetValue(_request.targetTile, out Tile _targetTile);

        //  If the target tile is out of bounds, disallow movement
        if (!targetInBounds)
        {
            //  If the entity is standing out of bounds, permit any movement.
            if (!entityInBounds)
            {
                _callback.condition = MovementCallBack.Condition.ACCESIBLE;
                _callback.targetTile = _targetTile;

                Move(_request.requestingEntity, _request.targetTile, _currentTile, _targetTile);
                return true;
            }

            _callback.condition = MovementCallBack.Condition.OUT_OF_BOUNDS;

            return false;
        }

        //  If the target tile is occupied, disallow movement.
        if (_targetTile.occupation != null)
        {
            _callback.condition     = MovementCallBack.Condition.OCCUPIED;
            _callback.targetTile    = _targetTile;

            return false;
        }

        //  Permit movement if not of the guard clauses are true.
        _callback.condition     = MovementCallBack.Condition.ACCESIBLE;
        _callback.targetTile    = _targetTile;

        Move(_request.requestingEntity, _request.targetTile, _currentTile, _targetTile);
        return true;
    }

    public bool TryGetTile(Vector2Int _coordinates, out Tile _tile)
    {
        return tileMap.TryGetValue(_coordinates, out _tile);
    }

    public void Move(Entity _entity, Vector2Int _destination, Tile _originTile, Tile _targetTile)
    {
        _entity.coordinates = _destination;

        if (_originTile != null)    _originTile.occupation = null;
        if (_targetTile != null)    _targetTile.occupation = _entity;
    }

    public static Vector2Int ProcessDesiredInput(Vector2 _direction)
    {
        var processedDirection  = Vector2Int.zero;

        if      (_direction.x > 0.5f)   processedDirection = Vector2Int.right;
        else if (_direction.x < -0.5f)  processedDirection = Vector2Int.left;
        else if (_direction.y > 0.5f)   processedDirection = Vector2Int.up;
        else if (_direction.y < -0.5f)  processedDirection = Vector2Int.down;
        return processedDirection;
    }
}