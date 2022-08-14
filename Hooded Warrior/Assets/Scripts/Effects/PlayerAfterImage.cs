using System;
using UnityEngine;

public class PlayerAfterImage : MonoBehaviour, IPoolComponent
{
    private GameObject _player;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _playerSpriteRenderer;

    [SerializeField] private float _activeTime;
    [SerializeField] private float _alphaSet;
    [SerializeField] private float _alphaMultiplier;

    private float _timeActivated;
    private float _alpha;
    private Color _color;

    private void Awake()
    {
        _player = GameManager.Instance.Player.gameObject;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerSpriteRenderer = _player.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _alpha = _alphaSet;
        _color = new Color(1f, 1f, 1f, _alpha);
        _spriteRenderer.sprite = _playerSpriteRenderer.sprite;
        _timeActivated = Time.time;
    }

    private void Update()
    {
        _alpha *= _alphaMultiplier;
        _color.a = _alpha;
        _spriteRenderer.color = _color;

        if (Time.time >= _timeActivated + _activeTime)
            ObjectPoolManager.Instance.AddToPool(gameObject);
    }

    public Type GetObjectType()
    {
        return GetType();
    }

}
