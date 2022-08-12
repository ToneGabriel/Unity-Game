
public interface IDamageble
{
    void Damage(AttackDetails attackDetails);

    bool CanTakeDamage();

    void AdditionalDamageActions(AttackDetails attackDetails);

    void CheckStatus();
}