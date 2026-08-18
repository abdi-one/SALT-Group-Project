using UnityEngine;

public class StoreRewindValue
{
    public Vector3 position;
    public Quaternion rotation;

    //just store the position value of player, including rotation bc why not
    public StoreRewindValue(Vector3 _position, Quaternion _rotation)
    {
        position = _position;
        rotation = _rotation;
    }
}