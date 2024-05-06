using System;

public enum ObjectiveType
{
    Gather,
    Collect,
    Kill,
};

public class Objective
{
    public ObjectiveType Type;

    public BaseDataEntry Source;
    
    public BaseDataEntry Target;

    public int Quantity;

    public bool IsCompleted;

    private System.Action _completionCallback;

    public Objective(bool autogenerate, System.Action completionCallback = null)
    {
        _completionCallback = completionCallback;

        if (!autogenerate)
        {
            return;
        }

        Type = GenerateType();

        switch (Type) {
            case ObjectiveType.Gather:
                InitGather();
            break;
            case ObjectiveType.Collect:
                InitCollect();
            break;
            case ObjectiveType.Kill:
                InitKill();
            break;
        }
    }

    void InitGather()
    {
        System.Random random = new System.Random();

        int targetIndex = random.Next(Database.Gatherables.Count);
        Target = Database.Gatherables[targetIndex];
        int quantityIndex = random.Next(Database.GatherableQuantities.Count);
        Quantity = Database.GatherableQuantities[quantityIndex];

        string randomTagFromTarget = Target.PickRandomTag();
        Source = Database.PickRandomLocationByTag(randomTagFromTarget);
    }

    void InitCollect()
    {
        System.Random random = new System.Random();

        int targetIndex = random.Next(Database.Items.Count);
        Target = Database.Items[targetIndex];
        Quantity = 1;

        string randomTagFromTarget = Target.PickRandomTag();
        Source = Database.PickRandomLocationByTag(randomTagFromTarget);
    }

    void InitKill()
    {
        System.Random random = new System.Random();

        int targetIndex = random.Next(Database.MobTypes.Count);
        int targetQuantityIndex = random.Next(Database.KillQuantities.Count);
        Target = Database.MobTypes[targetIndex];
        Quantity = Database.KillQuantities[targetQuantityIndex];

        string randomTagFromTarget = Target.PickRandomTag();
        Source = Database.PickRandomLocationByTag(randomTagFromTarget);
    }

    ObjectiveType GenerateType()
    {
        ObjectiveType[] types = (ObjectiveType[])Enum.GetValues(typeof(ObjectiveType));
        System.Random random = new System.Random();
        int randomIndex = random.Next(types.Length);
        return types[randomIndex];
    }

    public string GetTypeAsAdjective()
    {
        switch (Type)
        {
            case ObjectiveType.Gather:
                return "gather";
            case ObjectiveType.Collect:
                return "collect";
            case ObjectiveType.Kill:
                return "kill";
        }

        return "";
    }

    public Action? GetContextualAction(Location location)
    {
        if (IsCompleted || Source != location) return null;

        // In a more sophisticated version of the game, action callbacks would be 
        // more involved than this. We're using simple actions that immediately complete
        // the objective to simulate the gameplay at a very high level, since the focus
        // is mainly on the PCG techniques and not the gameplay logic.
        Action action = new Action
        {
            Label = ToString(),
            Callback = () => {
                IsCompleted = true;

                if (_completionCallback != null)
                {
                    _completionCallback.Invoke();
                }

                PlayerState.instance.EventTitles.Run("ObjectiveCompleted");
                PlayerState.instance.Actions.OnActionContextChanged();
            }
        };

        return action;
    }

    public override string ToString()
    {
        switch (Type)
        {
            case ObjectiveType.Gather:
                return $"Gather {Quantity} {Target.Name}";
            case ObjectiveType.Collect:
                return $"Collect {Target.Name}";
            case ObjectiveType.Kill:
                return $"Kill {Quantity} {Target.Name}";
        }

        return "";
    }
}
