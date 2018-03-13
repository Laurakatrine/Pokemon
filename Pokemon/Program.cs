using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pokemon
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Pokemon> roster = new List<Pokemon>();

            List<Move> CharmenderMove = new List<Move>();
            List<Move> SquritleMove = new List<Move>();
            List<Move> BulbasaurMove = new List<Move>();

            Move Ember = new Move("Ember[0]");
            Move FireBlast = new Move("Fire Blast[1]");
            Move Bubble = new Move("Bubble[0]");
            Move Bite = new Move("Bite[1]");
            Move Cut = new Move("Cut[0]");
            Move MegaDrain = new Move("Mega Drain[1]");
            Move RazorLeaf = new Move("Razor Leaf[2]");

            CharmenderMove.Add(Ember);
            CharmenderMove.Add(FireBlast);
            SquritleMove.Add(Bubble);
            SquritleMove.Add(Bite);
            BulbasaurMove.Add(Cut);
            BulbasaurMove.Add(MegaDrain);
            BulbasaurMove.Add(RazorLeaf);

            Pokemon Charmender = new Pokemon("Charmender", 3, 52, 43, 39, Elements.Fire, CharmenderMove);
            Pokemon Squirtle = new Pokemon("Squirtle", 2, 48, 65, 44, Elements.Water, SquritleMove);
            Pokemon Bulbasaur = new Pokemon("Bulbasaur", 3, 49, 49, 45, Elements.Grass, BulbasaurMove);

            roster.Add(Charmender);
            roster.Add(Squirtle);
            roster.Add(Bulbasaur);

            Console.WriteLine("Welcome to the world of Pokemon!\nThe available commands are list/fight/heal/quit");

            while (true)
            {
                Console.WriteLine("\nPlease enter a command");
                switch (Console.ReadLine())
                {
                    case "list":
                        // PRINT THE POKEMONS IN THE ROSTER HERE
                        roster.ForEach(item => Console.Write(item.Name + " "));
                        break;

                    case "fight":
                        //PRINT INSTRUCTIONS AND POSSIBLE POKEMONS (SEE SLIDES FOR EXAMPLE OF EXECUTION)

                        Console.Write("Chooce who should fight (");
                        roster.ForEach(item => Console.Write(item.Name + " "));
                        Console.WriteLine(")");


                        //READ INPUT, REMEMBER IT SHOULD BE TWO POKEMON NAMES
                        //BE SURE TO CHECK THE POKEMON NAMES THE USER WROTE ARE VALID (IN THE ROSTER) AND IF THEY ARE IN FACT 2!
                        string input = Console.ReadLine();
                        string s = input;
                        string[] fighters = s.Split(' ');
                        Pokemon player = null;
                        Pokemon enemy = null;

                        if (fighters.Length == 2)
                        {
                            foreach (Pokemon item in roster)
                            {
                                if (item.Name == fighters[0])
                                {
                                    player = item;  
                                }
                                
                                
                                if (item.Name == fighters[1])
                                {
                                    enemy = item;
                                }
                               
                            }
                        }           

                        //if everything is fine and we have 2 pokemons let's make them fight
                        if (player != null && enemy != null && player != enemy)
                        {
                            Console.WriteLine("A wild " + enemy.Name + " appears!");
                            Console.Write(player.Name + " I choose you! ");

                            //BEGIN FIGHT LOOP
                            while (player.Hp > 0 && enemy.Hp > 0)
                            {
                                //PRINT POSSIBLE MOVES
                                Console.Write("What move should we use? (");
                                player.Moves.ForEach(item => Console.Write(item.Name + " "));
                                Console.WriteLine(")");

                                int move = -1; 

                                //GET USER ANSWER, BE SURE TO CHECK IF IT'S A VALID MOVE, OTHERWISE ASK AGAIN
                                int PokemonMove = Convert.ToInt32(Console.ReadLine());
                                if (PokemonMove <= (roster.Count-1) && 0<= PokemonMove)
                                {
                                    move = PokemonMove;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid move " + player.Name + " got confused");
                                    break;
                                }
 
                                //CALCULATE AND APPLY DAMAGE
                                
                                int damage = player.Attack(enemy);
                                int applydamagetoenemy = enemy.ApplyDamage(damage);


                                //print the move and damage
                                Console.WriteLine(player.Name + " uses " + player.Moves[move].Name + ". " + enemy.Name + " loses " + damage + " HP");

                                //if the enemy is not dead yet, it attacks
                                if (enemy.Hp > 0)
                                {
                                    //CHOOSE A RANDOM MOVE BETWEEN THE ENEMY MOVES AND USE IT TO ATTACK THE PLAYER
                                    Random rand = new Random();
                                    

                                    /*the C# random is a bit different than the Unity random
                                     * you can ask for a number between [0,X) (X not included) by writing
                                     * rand.Next(X) 
                                     * where X is a number 
                                     */
                                    int enemyMove = rand.Next(roster.Count-1); 
                                    int enemyDamage = enemy.Attack(player);
                                    int applydamagetoplayer = player.ApplyDamage(enemyDamage);


                                    //print the move and damage
                                    Console.WriteLine(enemy.Name + " uses " + enemy.Moves[enemyMove].Name + ". " + player.Name + " loses " + enemyDamage + " HP");
                                
                                }
                            }
                            //The loop is over, so either we won or lost
                            if (enemy.Hp <= 0) //DET VIRKER NÅR JEG LAVER DEN HER OM, fordi den trækker hp fra den forkerte!
                            {
                                Console.WriteLine(enemy.Name + " faints, you won!");
                            }
                            else
                            {
                                Console.WriteLine(player.Name + " faints, you lost...");
                            }
                        }
                        //otherwise let's print an error message
                        else
                        {
                            Console.WriteLine("Invalid pokemons");
                        }
                        break;

                    case "heal":
                        //RESTORE ALL POKEMONS IN THE ROSTER
                        roster.ForEach(item => item.Restore());
                                           

                        Console.WriteLine("All pokemons have been healed");
                        break;

                    case "quit":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Unknown command");
                        break;
                }
            }
        }
    }
}
