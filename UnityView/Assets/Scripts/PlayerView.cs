using Model;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private BattleManager _battleManager;

    public BattleManager BattleManager => _battleManager;
    public Player PlayerLogic { get; private set; } = new Player();
}
