namespace Model.BossesAttacks
{
    public class ShockwaveAttack : BossAttack
    {
        public ShockwaveAttack()
        {
            Damage = 20;
            Name = "Ударная волна";
            Type = AttackType.Shockwave;
        }
    }
}
