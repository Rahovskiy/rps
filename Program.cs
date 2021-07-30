using System;
using System.Security.Cryptography;
using System.Text;


namespace RPSgame
{  
    
    class Program
    {
        static int Main(string[] arg)
        {
            int rep = 1;
            for (int i = 0; i < arg.Length; i++)
            {
                for (; rep <arg.Length;rep++)
                {
                    if(arg[i] == arg[rep])
                    {
                        Console.WriteLine("Repeated names");
                        return 0;
                    }
                }
                rep++;
            }
            if (arg.Length % 2 == 0 || arg.Length == 1)
            {
                Console.WriteLine("Please, enter ood number >= 3");
                return 0;
            }
            Random r = new Random();
            int compChoice = r.Next(1, arg.Length + 1);
            byte[] key = new byte[16];
           
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(key);
            
            var hmac = new HMACSHA256(key);
             byte[] bytesComp = Encoding.Default.GetBytes(arg[compChoice-1]);
             var bhash =hmac.ComputeHash(bytesComp);
            Console.WriteLine("HMAC: " + BitConverter.ToString(bhash).Replace("-", string.Empty));
          
            int yourChoice;
            while (true)
            {
                Console.WriteLine("Available moves:");
                int counter = 1;
                foreach (var a in arg)
                {
                    Console.WriteLine(counter + " " + a);
                    counter++;
                }

                Console.WriteLine("Choose yours: ");
                yourChoice = Convert.ToInt32(Console.ReadLine());
                if (yourChoice > arg.Length || yourChoice < 0)
                {
                    Console.WriteLine($"Enter number from 1 to {arg.Length}");
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine("Your move: " + arg[yourChoice - 1]);
            Console.WriteLine("Computer move:" + arg[compChoice - 1]);
            if (yourChoice + arg.Length / 2 > arg.Length)
            {
                if (yourChoice == compChoice)
                {
                    Console.WriteLine("Draw!");
                }
                else if (yourChoice - 1 - arg.Length / 2 <= compChoice - 1 && compChoice < yourChoice)
                {
                    Console.WriteLine("Computer won!");
                }
                else
                {
                    Console.WriteLine("You won!");
                }
            }
            else
            {
                if (yourChoice == compChoice)
                {
                    Console.WriteLine("Draw!");
                }
                else if (yourChoice - 1 + arg.Length / 2 >= compChoice - 1 && compChoice > yourChoice)
                
                {
                    Console.WriteLine("You won!");
                }
                else
                {
                    Console.WriteLine("Computer won!");
                }
            }
            Console.WriteLine("HMAC secretkey: " + BitConverter.ToString(key).Replace("-", string.Empty));

            return 0;
        }

    }
}