using Model;
using Model.Bosses;
using UnityEngine;

public class BossView : MonoBehaviour
{
    [SerializeField] private BattleManager _battleManager;

    public BattleManager BattleManager => _battleManager;
    public MonolithBoss BossLogic { get; private set; } = new MonolithBoss();
}
