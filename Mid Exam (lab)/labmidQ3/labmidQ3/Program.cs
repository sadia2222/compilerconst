using System;
using System.Linq;

class PasswordGenerator
{
    private static readonly Random random = new Random();
    private const string UpperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string SpecialCharacters = "!@#$%^&*()_+{}:\"<>?";

    public static string GeneratePassword(string firstName, string lastName, string registrationNumber)
    {
        
        string oddLettersFirstName = GetOddLetters(firstName);
       
        string evenLettersLastName = GetEvenLetters(lastName);

        
        string numbers = GetNumbers(registrationNumber);

        
        string initials = GetInitials(firstName, lastName);

        
        string upperCaseLetter = GetRandomUpperCaseLetter();

        
        string specialCharacters = GetRandomSpecialCharacters();

        
        string password = oddLettersFirstName + evenLettersLastName + numbers + initials + upperCaseLetter + specialCharacters;

        
        password = ShuffleString(password);

       
        if (password.Length > 16)
            password = password.Substring(0, 16);

        return password;
    }

    private static string GetOddLetters(string text)
    {
        return new string(text.Where((c, i) => i % 2 == 0).ToArray());
    }

    private static string GetEvenLetters(string text)
    {
        return new string(text.Where((c, i) => i % 2 != 0).ToArray());
    }

    private static string GetNumbers(string registrationNumber)
    {
        string numbers = registrationNumber;
        while (numbers.Length < 4)
        {
            numbers += random.Next(10).ToString();
        }
        return numbers;
    }

    private static string GetInitials(string firstName, string lastName)
    {
        return $"{firstName[0]}{lastName[0]}";
    }

    private static string GetRandomUpperCaseLetter()
    {
        return UpperCaseLetters[random.Next(UpperCaseLetters.Length)].ToString();
    }

    private static string GetRandomSpecialCharacters()
    {
        string specialCharacters = "";
        for (int i = 0; i < 2; i++)
        {
            specialCharacters += SpecialCharacters[random.Next(SpecialCharacters.Length)];
        }
        return specialCharacters;
    }

    private static string ShuffleString(string str)
    {
        char[] array = str.ToCharArray();
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            var value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
        return new string(array);
    }

    static void Main(string[] args)
    {
        Console.WriteLine("enter your first name:");
        string firstName = Console.ReadLine();

        Console.WriteLine(" enter your last name:");
        string lastName = Console.ReadLine();

        Console.WriteLine(" enter your registration number:");
        string registrationNumber = Console.ReadLine();

        string password = GeneratePassword(firstName, lastName, registrationNumber);
        Console.WriteLine("Generated Password: " + password);
    }
}
