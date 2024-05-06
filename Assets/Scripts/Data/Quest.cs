// Quest source (NPC)
// Quest target(s) (item(s), mob(s), NPC(s))
// Quest action (gather, kill, deliver)
// Quest reward (gold, XP)
// (maybe) chaining (quest A depends on quest B)

// Gather 10 bear heads from the forest for Ted
// ObjectiveType: Gather
// Source: Forest (Location)
// Target: Bear Ass (Item)
// Quantity: 10

// Collect Iron Sword from Weaponsmith for Freya
// ObjectiveType: Collect
// Source: Weaponsmith (Location)
// Target: Iron Sword (Item)
// Quantity: 1

// Kill 15 rats at the inn
// ObjectiveType: Kill
// Source: Inn (Location)
// Target: Rat (MobType)
// Quantity: 15

// Innkeeper has asked you to clear out the rats because they keep eating the cheese wheels.
// Kill quest description templates:
// "{NPC} has asked you to clear out the {MobType} because {reason}"
// "You've been asked to kill {MobType} by {NPC} since {reason}"

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public enum QuestStatus
{
    Available,
    Accepted,
    Completed,
};

public class Quest
{
    public string Title;

    public string Description;

    public NPC QuestGiver;
    public Location QuestGiverLocation;

    public readonly List<Objective> Objectives = new List<Objective>();

    public readonly List<IReward> Rewards = new List<IReward>();

    public readonly List<int> RewardQuantities = new List<int>();

    public QuestStatus Status = QuestStatus.Available;

    private bool _isSequential; // if this is true, objectives must be completed in the order they're specified
    private System.Action _completionCallback;

    public Quest(bool autogenerate, bool isSequential = false, System.Action completionCallback = null)
    {
        _isSequential = isSequential;
        _completionCallback = completionCallback;

        if (!autogenerate)
        {
            return;
        }

        // TODO: Clean these methods up. Ideally, they should return values instead of assigning them directly,
        // and the ones that depend on others should accept arguments instead of reading properties directly.
        GenerateObjectives();
        GenerateQuestGiver();
        GenerateTitle();
        GenerateDescription();
        GenerateRewards();
    }

    void GenerateQuestGiver()
    {
        // Pick a quest giver who isn't associated with the target item based on the first tag in its list of tags.
        // This avoids a situation where we're sent to collect an item that the quest giver should already have (e.g. collecting a sword for a weaponsmith).
        QuestGiver = Database.PickRandomNPCNotHavingTag(Objectives[0].Target.Tags[0]);

        // The quest giver's location is assumed to be the first location that matches the first tag in the quest giver's list.
        QuestGiverLocation = Database.PickFirstLocationByTag(QuestGiver.Tags[0]);
    }

    void GenerateObjectives()
    {
        System.Random random = new System.Random();
        int objectiveCount = random.Next(1, 1);

        for (int i = 0; i < objectiveCount; ++i)
        {
            Objective objective = new Objective(true);
            Objectives.Add(objective);
        };
    }

    void GenerateTitle()
    {
        if (Objectives.Count == 0)
        {
            return;
        }

        Objective objective = Objectives[0];
        switch (objective.Type)
        {
            case ObjectiveType.Gather:
                Title = $"Gather {objective.Target.Name} for {QuestGiver.Name}";
            break;
            case ObjectiveType.Collect:
                Title = $"Collect {objective.Target.Name} for {QuestGiver.Name}";
            break;
            case ObjectiveType.Kill:
                Title = $"Kill {objective.Target.Name} for {QuestGiver.Name}";
            break;
        }
    }

    void GenerateDescription()
    {
        // For now, assume only 1 objective
        var target = Objectives[0].Target.Name;
        var adjective = Objectives[0].GetTypeAsAdjective();

        var reasonTags = new List<string>
        {
            adjective
        };

        if (adjective == "gather")
        {
            // To narrow down the reason picked so that it makes sense for the item being gathered
            reasonTags.Add(Objectives[0].Target.Tags[0]);
        }
        
        List<string> baseDescriptions = new List<string>()
        {
            $"You've been asked by {QuestGiver.Name} to {adjective} {target}",
            $"{QuestGiver.Name} wants you to {adjective} {target}",
            $"You have been tasked with {adjective}ing {target} by {QuestGiver.Name}"
        };

        System.Random random = new System.Random();
        int baseIndex = random.Next(baseDescriptions.Count);

        Description = baseDescriptions[baseIndex] + " " + Database.PickRandomObjectiveReasonByTags(reasonTags).Name;
    }

    void GenerateRewards()
    {
        System.Random random = new System.Random();
        int rewardCount = random.Next(1, 2);
        
        if (rewardCount == 2) {
            Rewards.Add(Database.Rewards[0]); // Gold
            Rewards.Add(Database.Rewards[1]); // XP
            int xpQuantityIndex = random.Next(Database.RewardQuantities.Count);
            RewardQuantities.Add(Database.RewardQuantities[xpQuantityIndex]);
            int goldQuantityIndex = random.Next(Database.RewardQuantities.Count);
            RewardQuantities.Add(Database.RewardQuantities[goldQuantityIndex]);
        } else {
            int rewardIndex = random.Next(Database.Rewards.Count);
            Rewards.Add(Database.Rewards[rewardIndex]);
            int quantityIndex = random.Next(Database.RewardQuantities.Count);
            RewardQuantities.Add(Database.RewardQuantities[quantityIndex]);
        }
    }

    public string GetRewardList()
    {
        string list = "";
        for (int i = 0; i < Rewards.Count; ++i)
        {
            list += $"- {RewardQuantities[i]} {Rewards[i].GetName()}\n";
        }

        return list;
    }

    public List<Action> GetContextualActions(Location location)
    {
        List<Action> actions = new List<Action>();

        foreach (var objective in Objectives)
        {
            Action? objectiveAction = objective.GetContextualAction(location);
            if (objectiveAction != null)
            {
                actions.Add((Action)objectiveAction);
            }

            if (_isSequential && !objective.IsCompleted) break;
        }

        if (!Objectives.Any(objective => !objective.IsCompleted) && QuestGiverLocation == location)
        {
            // All objectives completed, so this quest can be turned in at the quest giver's location
            actions.Add(new Action { Label = "Turn in active quest", Callback = Complete });
        }

        return actions;
    }

    public void Complete()
    {
        if (_completionCallback != null)
        {
            _completionCallback.Invoke();
        }

        ExecuteEvents.Execute<IQuestStateEvents>(GameObject.Find("QuestState"), null, (x, y) => x.OnCompleteQuest(this));
    }
}
