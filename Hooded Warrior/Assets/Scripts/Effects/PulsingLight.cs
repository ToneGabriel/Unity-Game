using UnityEngine;


public class PulsingLight : MonoBehaviour
{
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D _lightComponent;
    [SerializeField] private float _pulseTime;
    [SerializeField] private float _changeAmmount;
    private float _changeTime;
    private int _changeDirection;

    private void OnEnable()
    {
        _changeTime = Time.time;
        _changeDirection = 1;
    }

    private void Update()
    {
        if (Time.time >= _changeTime + _pulseTime)
            ChangeLightRadius();
    }

    private void ChangeLightRadius()
    {
        _lightComponent.pointLightInnerRadius += _changeDirection * _changeAmmount;
        _lightComponent.pointLightOuterRadius += _changeDirection * _changeAmmount;

        _changeDirection *= -1;
        _changeTime = Time.time;
    }

}
