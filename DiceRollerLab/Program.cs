Orchestrator();

void Orchestrator()
{
    Console.WriteLine("Welcome to the great dice roller!");

    int numSides = GetNumSides(),
        i = 1;
    string rollAgain = "y";

    while (rollAgain == "y")
    {
        Console.WriteLine($"Roll {i++} of your {numSides}-sided dice:");
        RollDice(numSides);

        do
        {
            Console.Write("Roll again? y/[n]: ");
            rollAgain = Console.ReadLine();
        }
        while (rollAgain.ToLower() != "y" && rollAgain.ToLower() != "n" && rollAgain != "");
    }

    Console.WriteLine("\nThanks for playing.");
    Console.ReadKey();
}

int GetNumSides()
{
    int numSides = 0;
    bool validValue = false;

    while (!validValue)
    {
        Console.Write("\nHow many sides should each die have (up to 10 sides)? ");
        validValue = int.TryParse(Console.ReadLine(), out numSides) && numSides > 1 && numSides < 11;

        if (!validValue)
            Console.WriteLine("That's not a valid number of sides.  Try again.");
    }

    return numSides;
}

void RollDice(int numSides)
{
    int die1 = GetRandomSides(numSides, out int die2);
    string firstMessage = "", secondMessage = "";

    switch (numSides)
    {
        case 2:
            firstMessage = TwoSides(die1, die2);
            break;

        case 4:
            firstMessage = FourSides(die1, die2);
            break;
        
        case 6:
            firstMessage = SixSides(die1, die2, out secondMessage);
            break;

        case 10:
            firstMessage = TenSides(die1, die2);
            break;

        default:
            break;
    }

    OutputResults(die1, die2, firstMessage, secondMessage);
}

int GetRandomSides(int numSides, out int random2)
{
    Random random = new Random();
    random2 = random.Next(1, numSides + 1);

    return random.Next(1, numSides + 1);
}

string TwoSides(int die1, int die2)
{
    if (die1 == die2)
        return "A double!";
    return "";
}

string FourSides(int die1, int die2)
{
    if (die1 % 2 == 0 && die2 % 2 == 0)
        return "Evens!";

    if (die1 % 2 == 1 && die2 % 2 == 1)
        return "Odds!";

    return "";
}

string SixSides(int die1, int die2, out string secondMessage)
{
    string firstMessage = "";
    int diceTotal = die1 + die2,
        orderedDie1 = Math.Min(die1, die2),
        orderedDie2 = Math.Max(die1, die2);

    if (orderedDie1 == 1 && orderedDie2 == 1)
        firstMessage = "Snake Eyes";
    else if (orderedDie1 == 1 && orderedDie2 == 2)
        firstMessage = "Ace Deuce";
    else if (orderedDie1 == 6 && orderedDie2 == 6)
        firstMessage = "Box Cars";
    else if (diceTotal == 7 || diceTotal == 11)
        firstMessage = "Win!";
        
    if (diceTotal == 2 || diceTotal == 3 || diceTotal == 12)
        secondMessage = "Craps!";
    else
        secondMessage = "";

    return firstMessage;
}

string TenSides(int die1, int die2)
{
    if (die1 + die2 == 20)
        return "Two 10's!  What are the chances of that!?";
    return "";
}

void OutputResults(int die1, int die2, string firstMessage, string secondMessage)
{
    Console.WriteLine($"You rolled a {die1} and {die2} ({die1 + die2} total)");

    if (firstMessage != "")
        Console.WriteLine(firstMessage);
    if (secondMessage != "")
        Console.WriteLine(secondMessage);

    Console.WriteLine();
}
