using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private Transform _bossSpawnPosition;
    [SerializeField] private Enemy _boss;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameManager.Instance.Player.gameObject && !_boss.IsDead && !_boss.gameObject.activeSelf)
        {
            _boss.gameObject.SetActive(true);
            _boss.transform.position = _bossSpawnPosition.position;
        }
    }
}
