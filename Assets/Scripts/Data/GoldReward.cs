public class GoldReward : IReward
{
    public string GetName()
    {
        return "Gold";
    }

    public void Issue(int amount)
    {
        PlayerState.instance.AwardGold(amount);
    }
}
