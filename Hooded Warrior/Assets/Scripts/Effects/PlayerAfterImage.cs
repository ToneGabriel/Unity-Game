using UnityEngine;

public class PlayerAfterImage : MonoBehaviour, IPoolComponent
{
    public static string PoolTag;

    private GameObject _player;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _playerSpriteRenderer;

    private float _activeTime = 0.5f;
    private float _timeActivated;

    private float _alpha;
    private float _alphaSet = 1f;
    private float _alphaMultiplier = 0.8f;
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
            ObjectPoolManager.Instance.AddToPool(PoolTag, gameObject);
    }

    public string GetTag()
    {
        return PoolTag;
    }

    public void SetTag(string tag)
    {
        PoolTag = tag;
    }

}
