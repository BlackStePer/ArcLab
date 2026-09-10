namespace Model.BossesAttacks
{
    public class LaserPlatesAttack : BossAttack
    {
        public LaserPlatesAttack()
        {
            Damage = 30;
            Name = "Запуск пластин";
            Type = AttackType.LaserPlates;
        }
    }
}
