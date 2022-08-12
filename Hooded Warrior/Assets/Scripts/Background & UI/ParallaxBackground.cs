using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Sprite _sprite;
    private Texture2D _texture;

    private Camera _cam;
    private Vector3 _lastCameraPosition;
    private Vector3 _deltaMovement;
    private Vector3 _positionToSet;
    private float _objectScale;
    private float _textureUnitSizeX;
    private float _offsetPositionSizeX;
    [SerializeField] private Vector2 _parallaxEffectMultiplier;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>().sprite;
        _texture = _sprite.texture;
        _objectScale = GetComponentInParent<Transform>().lossyScale.x;
        _textureUnitSizeX = _objectScale * _texture.width / _sprite.pixelsPerUnit;

        _cam = GameManager.Instance.MainCamera;
        _lastCameraPosition = _cam.transform.position;
    }

    void FixedUpdate()
    {
        // Move each background piece with a different translate multiplier relative to the camera
        _deltaMovement = _cam.transform.position - _lastCameraPosition;
        _positionToSet.Set(_deltaMovement.x * _parallaxEffectMultiplier.x, _deltaMovement.y * _parallaxEffectMultiplier.y, 0f);
        transform.position += _positionToSet;
        _lastCameraPosition = _cam.transform.position;

        // When a background piece "is over" translate it from the beginning
        if (Mathf.Abs(_cam.transform.position.x - transform.position.x) >= _textureUnitSizeX)
        {
            _offsetPositionSizeX = (_cam.transform.position.x - transform.position.x) % _textureUnitSizeX;
            _positionToSet.Set(_cam.transform.position.x + _offsetPositionSizeX, transform.position.y, 0f);
            transform.position = _positionToSet;
        }
    }
    
}
