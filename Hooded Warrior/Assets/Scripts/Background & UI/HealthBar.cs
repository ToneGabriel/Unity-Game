using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _health;
    [SerializeField] private bool _isStatic;

    private float _maxHealth;
    private Vector3 _forwardLook;

    private void Awake()
    {
        _forwardLook = new Vector3(0f, 0f, 1f);
    }

    private void Update()
    {
        if (!_isStatic)
            transform.rotation = Quaternion.LookRotation(_forwardLook);
    }

    public void SetMaxHealth(float health)
    {
        _maxHealth = health;
    }

    public void SetHealthBar(float currentHealth)
    {
        _health.fillAmount = currentHealth / _maxHealth;
    }

}
