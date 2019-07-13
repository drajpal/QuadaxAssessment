using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public const int numberOfAttempts = 10;
    public class FinalResult
    {
        public string Answer { get; set; }
        public bool Flag { get; set; }
        public int Index { get; set; }
    }

    static void Main()
    {
        Program guessingGame = new Program();
        guessingGame.Run();
    }

    void Run()
    {
        // initialize the number of attempts
        //int numberOfAttempts = 10;

        Console.WriteLine("\nHello and welcome to Guessing game.");
        Console.WriteLine("\n\nPlease Guess some 4 Digit Random Number");
        Console.WriteLine("\nFor each digit, the number is chosen from 1 to 6  \n and numbers can repeat.");
        Console.WriteLine("a minus (-) sign is printed for every digit that is correct but in the wrong position");
        Console.WriteLine("a plus (+) sign is printed for every digit that is both correct and in the correct position");
        Console.WriteLine(string.Format("\nYou have {0} attempts to win the game.", numberOfAttempts));

        // Generate Random Number here
        string getRandomNumber = GenerateRandomNumber();

        for (int i = 1; i <= numberOfAttempts; i++)
        {
            // Get User Input
            string userEnteredInput = GetUserInput(i);

            // Get the result here
            List<FinalResult> result = GetResult(getRandomNumber, userEnteredInput);

            // Guess the count of number of digits that are correct
            int flagCount = result.Where(f => f.Flag == true).Count();

            // Get the final string
            string finalString = string.Join("", result
                .Select(c => (c.Answer).ToString()));

            // check the flag count and display appropriate message
            if (flagCount == 4)
            {
                Console.WriteLine("Random Number:{0} , Your Input:{1}", getRandomNumber, userEnteredInput);
                Console.WriteLine("Your guess is correct! Game Won..)");
                break;
            }
            else if (i == numberOfAttempts)
            {
                Console.WriteLine("You Reached Maximum attempts, You lost:(");
                Console.WriteLine("Random Number is {0}", getRandomNumber);
            }
            else
            {
                Console.WriteLine(string.Format(finalString));
            }
        }

        Console.ReadLine();
    }

    public List<FinalResult> GetResult(string randomNumber, string userInput)
    {
        char[] splitRandomNumber = randomNumber.ToCharArray();
        char[] splitUserInput = userInput.ToCharArray();

        List<FinalResult> results = new List<FinalResult>();

        for (int index = 0; index < randomNumber.Length; index++)
        {
            FinalResult result = new FinalResult();
            var isPlusMinus = false;
            if (splitRandomNumber[index] == splitUserInput[index])
            {
                result.Index = index;
                result.Flag = splitRandomNumber[index] == splitUserInput[index];
                result.Answer = "+";
                results.Add(result);
                isPlusMinus = true;
            }
            else
            {
                for (int j = 0; j < 4; j++)
                {
                    if (splitRandomNumber[index] == splitUserInput[j] && index != j)
                    {
                        result.Index = index;
                        result.Flag = false;
                        result.Answer = "-";
                        results.Add(result);
                        isPlusMinus = true;
                        break;
                    }
                }
            }
            // if nothing is matched printing empty string.
            if (!isPlusMinus)
            {
                result.Index = index;
                result.Answer = " ";
                results.Add(result);
            }
        }

        return results;
    }

    public string GetUserInput(int attempt)
    {
        int inputNumber;

        Console.WriteLine(string.Format("\nGuess the number. Attempt:{0}", attempt));
        Console.WriteLine("Enter a 4 digit number");

        if (int.TryParse(Console.ReadLine(), out inputNumber)
            && inputNumber.ToString().Length == 4)
        {
            return inputNumber.ToString();
        }
        else
        {
            Console.WriteLine("You have entered a invalid input.");
            return "0000";
        }
    }

    public string GenerateRandomNumber()
    {
        Random random = new Random();
        string number = string.Empty;
        int length = 4;
        for (int i = 0; i < length; i++)
        {
            number += random.Next(1, 7);
        }

        return number;
    }
}