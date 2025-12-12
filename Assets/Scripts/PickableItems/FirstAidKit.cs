

public class FirstAidKit : Item, IUsable
{
    private float _value = 40f;

    public void Use(IDamageable target)
    {
        Health healthComponent = target.GetHealthComponent();

        if (healthComponent != null)
        {
            healthComponent.Heal(_value);
        }
    }
}
