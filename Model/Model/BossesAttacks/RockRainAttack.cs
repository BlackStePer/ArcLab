namespace Model.BossesAttacks
{
    public class RockRainAttack : BossAttack
    {
        public RockRainAttack()
        {
            Damage = 10;
            Name = "Камнепад";
            Type = AttackType.RockRain;
        }
    }
}
