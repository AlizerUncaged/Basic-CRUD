using System;
using Pastel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;
using Basic_CRUD_with_OOP.Utilities;
using System.IO;

namespace Basic_CRUD_with_OOP
{
    internal class Program
    {

        private List<Models.Account> Accounts = new List<Models.Account>();
        private IEnumerable<Models.Post> Posts => Accounts.SelectMany(x => x.Posts);

        public void ViewPost(Models.Post post)
        {
            bool nonExit = true;
            while (nonExit)
            {
                Console.Clear();
                Console.WriteLine($"{post}");
                Console.WriteLine($">> Press {"ESC".Pastel(Color.Orange)} to exit or {"C".Pastel(Color.Orange)} to write a comment.");

                if (CurrentAccount.Posts.Contains(post))
                    Console.WriteLine($">> This is your post, you can press {"DEL".Pastel(Color.Orange)} to delete it.{Environment.NewLine}");

                var c = Console.ReadKey(true).Key;
                switch (c)
                {
                    case ConsoleKey.Escape:
                        nonExit = false;
                        break;
                    case ConsoleKey.Delete:
                        CurrentAccount.Posts.Remove(post);
                        Save(Accounts);
                        nonExit = false;
                        break;
                }
            }

        }

        public Models.Post SearchPosts()
        {
            int selected = 0;
            while (true)
            {
                Console.WriteLine(Utilities.StringExtensions.Banner);
                Console.WriteLine($">> {"Welcome back!".Pastel(Color.GreenYellow)} SNC#, a social networking concept made in C#");
                Console.WriteLine($">> Use {"arrow".Pastel(Color.Orange)} keys or {"W".Pastel(Color.Orange)} and {"D".Pastel(Color.Orange)} keys to move and {"ENTER".Pastel(Color.Yellow)} to view.");
                Console.WriteLine($">> Press {"A".Pastel(Color.Orange)} to write a post.{Environment.NewLine}");
                Console.WriteLine($">> New posts: {Environment.NewLine}");

                var orderedPosts = Posts.OrderByDescending(x => x.DatePosted).ToList();
                foreach (var p in orderedPosts)
                    Console.Write((orderedPosts.IndexOf(p) == selected ? ">> ".Pastel(Color.Cyan) : "   ")
                        + $"{p.ToTruncatedString()}{Environment.NewLine}");

                var c = Console.ReadKey(true).Key;
                switch (c)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        selected--;
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        selected++;
                        break;
                    case ConsoleKey.A:
                        CurrentAccount.Posts.Add(Models.Post.AskPost(CurrentAccount.Username));
                        Save(Accounts);
                        Console.Clear();
                        break;
                    case ConsoleKey.Enter:
                        ViewPost(orderedPosts[selected]);
                        Console.Clear();
                        break;
                }

                selected = selected > orderedPosts.Count - 1 ? selected = orderedPosts.Count - 1 : selected <= 0 ? selected = 0 : selected;
                // Reset the console cursor to top.
                Console.SetCursorPosition(0, 0);
            }
        }

        private Models.Account CurrentAccount;

        public void Start()
        {
            Console.Title = "Social Networking in C# Console App [SNC#]";

            // Load posts from .xml
            Accounts = Read<Models.Account>(new Models.Account().XmlName).ToList();


            Console.Write("[?] Log in as: ");
            var username = Console.ReadLine().Trim();
            var foundAccount = Accounts.Where(x => string.Equals(x.Username, username, StringComparison.OrdinalIgnoreCase));

            if (foundAccount.Any())
                CurrentAccount = Accounts.FirstOrDefault();
            else
            {
                Console.WriteLine($"[!] {"You're not registered yet!".Pastel(Color.LightYellow)}");
                CurrentAccount = Models.Account.AskAccount();
                Accounts.Add(CurrentAccount);
            }

            Console.Clear();
            while (true)
            {
                var selectedPost = SearchPosts();
            }
        }

        /// <summary>
        /// Saves models on file as xml.
        /// </summary>
        public IEnumerable<T> Read<T>(string filename) where T : IModel
        {
            IEnumerable<T> p;

            if (!File.Exists(filename)) return new List<T>();

            var serializer = new XmlSerializer(typeof(T[]));
            using (var reader = XmlReader.Create(filename))
                p = (IEnumerable<T>)serializer.Deserialize(reader);

            return p;
        }


        public void Save<T>(IEnumerable<T> obj) where T : IModel
        {
            if (obj.Count() <= 0) return;
            var serializer = new XmlSerializer(obj.GetType());
            using (var writer = XmlTextWriter.Create(obj.First().XmlName))
                serializer.Serialize(writer, obj);
        }

        static void Main(string[] args) => new Program().Start();
    }
}
