using Model;
using UnityEngine;
using Model.BossesAttacks;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private PlayerView _player;
    [SerializeField] private BossView _boss;
    [SerializeField] private GameUIManager _uiManager;
    public GameLogic BattleLogic { get; private set; }

    [Header("Физический контроллер босса")]
    [SerializeField] private BossController _bossController;
    [SerializeField] private float _attackCooldown = 4.0f; 

    private System.Collections.IEnumerator BossAttackRoutine()
    {

        yield return new WaitForSeconds(2.0f);

        while (BattleLogic != null && !BattleLogic.IsBattleOver())
        {
            BossAttack currentAttack = BattleLogic.PerformBossAttack();

            if (_bossController != null)
            {
                _bossController.LaunchVisualAttack(currentAttack.Type);
            }
            yield return new WaitForSeconds(_attackCooldown);
        }
    }

    private void Start()
    {
        BattleLogic = new GameLogic(_player.PlayerLogic, _boss.BossLogic);

        if (_uiManager != null)
        {
            _uiManager.SetupHUD(_player.PlayerLogic.MaxHP, _boss.BossLogic.MaxHP);
        }

        _player.PlayerLogic.DamageTaken += OnPlayerDamageTaken;
        _player.PlayerLogic.PlayerDied += OnPlayerDied;

        _boss.BossLogic.DamageTaken += OnBossDamageTaken;
        _boss.BossLogic.BossDied += OnBossDied;

        StartCoroutine(BossAttackRoutine());
    }

    private void OnPlayerDamageTaken() =>_uiManager.UpdatePlayerHP(_player.PlayerLogic.HP);


    private void OnPlayerDied() =>_uiManager.UpdatePlayerHP(0);


    private void OnBossDamageTaken() => _uiManager.UpdateBossHP(_boss.BossLogic.HP);

    private void OnBossDied() => _uiManager.UpdateBossHP(0); 

    public void OnProjectileCollisionEnter(Projectile projectile, Collision collision)
    {

        BossView bossView = collision.gameObject.GetComponentInParent<BossView>();

        if (projectile.Sender == _player.gameObject && bossView != null)
        {
            BattleLogic.PerformPlayerAttack();
            return;
        }

        PlayerView playerView = collision.gameObject.GetComponentInParent<PlayerView>();
        if (projectile.Sender == _boss.gameObject && playerView != null)
        {
            BattleLogic.PerformBossAttack();
            Debug.Log(_player.PlayerLogic.HP);
        }
    }


    private void OnDestroy()
    {
        if (_player != null && _player.PlayerLogic != null)
        {
            _player.PlayerLogic.DamageTaken -= OnPlayerDamageTaken;
            _player.PlayerLogic.PlayerDied -= OnPlayerDied;
        }

        if (_boss != null && _boss.BossLogic != null)
        {
            _boss.BossLogic.DamageTaken -= OnBossDamageTaken;
            _boss.BossLogic.BossDied -= OnBossDied;
        }
    }
}
