using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private Transform _bossSpawnPosition;
    [SerializeField] private Enemy _boss;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ColliderIsPlayer(other) && !_boss.StatusComponents.IsDead && !_boss.gameObject.activeSelf)
        {
            _boss.gameObject.SetActive(true);
            _boss.transform.position = _bossSpawnPosition.position;
        }
    }

    private bool ColliderIsPlayer(Collider2D other)
    {
        return other.gameObject == GameManager.Instance.Player.gameObject;
    }
}
