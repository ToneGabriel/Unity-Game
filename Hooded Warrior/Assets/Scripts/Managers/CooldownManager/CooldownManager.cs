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
        {
            Instance = this;
            _cooldowns = new List<ICooldown>();
        }
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGamePaused)
            UpdateCooldowns();
    }
    #endregion

    #region Subscription
    public void Subscribe(ICooldown cooldown) => _cooldowns.Add(cooldown);

    public void UnSubscribe(ICooldown cooldown) => _cooldowns.Remove(cooldown);
    #endregion

    #region Others
    public void ResetCooldowns()     // reset cooldowns when loading screen is active
    {
        for (int i = 0; i < _cooldowns.Count; i++)
            _cooldowns[i].ResetCooldown();
    }

    private void UpdateCooldowns()
    {
        for (int i = 0; i < _cooldowns.Count; i++)
            _cooldowns[i].CheckCooldown();
    }
    #endregion
}