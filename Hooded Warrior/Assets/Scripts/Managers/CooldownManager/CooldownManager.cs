using System.Collections.Generic;
using UnityEngine;

public class CooldownManager : MonoBehaviour
{
    public static CooldownManager Instance;
    private List<ICooldown> _cooldowns;

    #region Unity functions
    private void Awake()                                            // Singleton instance
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start() => _cooldowns = new List<ICooldown>();

    private void Update()
    {
        for (int i = 0; i < _cooldowns.Count; i++)
            _cooldowns[i].CheckCooldown();
    }
    #endregion

    #region Subscription
    public void Subscribe(ICooldown cooldown) => _cooldowns.Add(cooldown);

    public void UnSubscribe(ICooldown cooldown) => _cooldowns.Remove(cooldown);
    #endregion
}