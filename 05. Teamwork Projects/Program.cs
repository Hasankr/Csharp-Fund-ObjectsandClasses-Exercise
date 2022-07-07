using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Teamwork_Projects
{
    class Team
    {
        public Team(string nameTeam, string creatorTeam)
        {
            Name = nameTeam;
            Creator = creatorTeam;
            this.Members = new List<string>();
        }
        public string Name { get; set; }
        public string Creator { get; set; }

        public List<string> Members { get; set; }

        public void AddMembars(string membars)
        {
            this.Members.Add(membars);
        }
    }

    internal class Program
    {
        static void Main(string[] args)

        {
            List<Team> teams = new List<Team>();
            int n = int.Parse(Console.ReadLine());
            RegisterTeams(teams, n);

            string comand = Console.ReadLine();
            RegisterUser(comand, teams);
            PrintOutput(teams);



        }



        static void RegisterTeams(List<Team> teams, int n)
        {

            for (int i = 0; i < n; i++)
            {
                string[] infoTeams = Console.ReadLine().Split('-', StringSplitOptions.RemoveEmptyEntries).ToArray();
                string nameTeam = infoTeams[1];
                string creatorTeam = infoTeams[0];
                //"Team {teamName} was already created!"
                if (teams.Any(x => x.Name == nameTeam))
                {
                    Console.WriteLine($"Team {nameTeam} was already created!");
                    continue;
                }
                //-	"{user} cannot create another team!"
                if (teams.Any(x => x.Creator == creatorTeam))
                {
                    Console.WriteLine($"{creatorTeam} cannot create another team!");
                    continue;
                }
                Team newTeam = new Team(nameTeam, creatorTeam);
                teams.Add(newTeam);
                //Team {teamName} has been created by {user}!".
                Console.WriteLine($"Team {nameTeam} has been created by {creatorTeam}!");
            }
        }

        static void RegisterUser(string comand, List<Team> teams)
        {
            while (comand != "end of assignment")
            {
                string[] infoUser = comand.Split("->", StringSplitOptions.RemoveEmptyEntries).ToArray();
                string user = infoUser[0];
                string teamJoin = infoUser[1];
                Team team = teams.FirstOrDefault(x => x.Name == teamJoin);
                //-	"Team {teamName} does not exist!"
                if (team == null)
                {
                    Console.WriteLine($"Team {teamJoin} does not exist!");
                    comand = Console.ReadLine();
                    continue;
                }
                //-	"Member {user} cannot join team {team Name}!"
                if (teams.Any(x => x.Creator == user))
                {
                    Console.WriteLine($"Member {user} cannot join team {teamJoin}!");
                    comand = Console.ReadLine();
                    continue;
                }
                if (teams.Any(x => x.Members.Contains(user)))
                {
                    Console.WriteLine($"Member {user} cannot join team {teamJoin}!");
                    comand = Console.ReadLine();
                    continue;
                }
                team.AddMembars(user);

                comand = Console.ReadLine();
            }
            return;
        }

        static void PrintOutput(List<Team> teams)
        {
            List<Team> validTeams = teams.Where(x => x.Members.Count > 0).
                OrderByDescending(x => x.Members.Count).ThenBy(x => x.Name).ToList();
            List<Team> invalidTeams = teams.Where(x => x.Members.Count == 0).OrderBy(x => x.Name).ToList();

            foreach (var valid in validTeams)
            {
                //  "{teamName}
                //- { creator}
                // -- { member}…"
                Console.WriteLine(valid.Name);
                Console.WriteLine($"- {valid.Creator}");
                foreach (var member in valid.Members.OrderBy(x => x))
                {
                    Console.WriteLine($"-- {member}");
                }

            }
            Console.WriteLine("Teams to disband:");
            foreach (var item in invalidTeams)
            {
                Console.WriteLine($"{item.Name}");
            }
        }
    }
}
