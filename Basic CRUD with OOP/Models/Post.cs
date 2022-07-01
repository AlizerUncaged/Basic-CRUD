using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Basic_CRUD_with_OOP.Utilities;
using Pastel;

namespace Basic_CRUD_with_OOP.Models
{
    public class Post : IModel
    {
        public string Title { get; set; } // Auto implemented proerty.
        public string Content { get; set; }

        public string Author { get; set; }

        public long PostId { get; set; }

        public DateTime DatePosted { get; set; }

        public List<Comment> Comments { get; set; }

        public void AddComments(Comment c) => Comments.Add(c);
        public void AddComments(IEnumerable<Comment> c) => Comments.AddRange(c);

        /// <summary>
        /// Gets the unique comments by username.
        /// </summary>
        public int UniqueComments => Comments.GroupBy(x => x.Username).Count(); // Automatically calculated property.

        public Post() : this(null, null, null)
        {

        }

        public Post(string content, string title) : this(content, "Anonymous", title)
        {

        }

        public Post(string content, string author, string title)
        {
            Comments = new List<Comment>();
            Title = title;
            PostId = Utilities.Random.Global.Next();
            Content = content;
            Author = author;
            DatePosted = DateTime.Now;
        }

        /// <summary>
        /// Generate a post from user inputs.
        /// </summary>
        /// <returns></returns>
        public static Post AskPost(string user)
        {
            Console.Write("[?] Post title: ");
            string title = Console.ReadLine().Trim();
            Console.WriteLine($"[?] Write the content below, press {"Ctrl + S".Pastel(Color.Orange)} to save.");
            string lines = string.Empty;

            Console.Write(">> ");
            var key = Console.ReadKey(true);
            while (true)
            {
                lines += key.KeyChar;

                if (key.Key == ConsoleKey.S && key.Modifiers.HasFlag(ConsoleModifiers.Control))
                    break;
                else
                    Console.Write(key.KeyChar);

                // Fixes.
                switch (key.KeyChar)
                {
                    case '\b':
                        Console.Write(" \b");
                        break;
                    case '\r':
                        Console.Write("\n>> ");
                        lines += Environment.NewLine;
                        break;
                }

                key = Console.ReadKey(true);
            }
            // Clean the content string.
            lines = new string(lines.Where(x => !char.IsControl(x)).ToArray());

            return new Post(lines, user, title);
        }

        /// <returns>The console writable form of the post.</returns>
        public override string ToString() =>
            $">> {Title.Pastel(Color.LightPink)} by [{Author.Pastel(Color.LightCyan)}]{Environment.NewLine}>> Posted on {DatePosted.ToString("dddd, dd MMMM yyyy")}:{Environment.NewLine}{Environment.NewLine}" +
            $"\t{string.Join($"{Environment.NewLine}\t", Content.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).SelectMany(x => x.SplitOnLength(80)))}{Environment.NewLine}{Environment.NewLine}" +
            $"{(Comments.Any() ? $">> Comments:{Environment.NewLine}{Environment.NewLine}{string.Join($"{Environment.NewLine}", Comments.Select(x => $"{x}"))}" : ">> No comments")}";

        public string ToTruncatedString() =>
            $"[{Author.Pastel(Color.LightCyan)}]: \"{Title.Truncate(16).Pastel(Color.LightPink)}\" posted on {DatePosted.ToString("dddd, dd MMMM yyyy").Pastel(Color.LightSkyBlue)}";

        public string XmlName => "Posts.xml";
    }
}
