public class XPReward : IReward
{
    public string GetName()
    {
        return "XP";
    }

    public void Issue(int amount)
    {
        PlayerState.instance.AwardXP(amount);
    }
}
