using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basic_CRUD_with_OOP.Utilities;

namespace Basic_CRUD_with_OOP.Models
{
    public class Comment
    {
        /// <summary>
        /// The person who commented.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The comment string.
        /// </summary>
        public string Content { get; set; }

        public DateTime DateModified { get; set; }


        public Comment() : this(null, null)
        {
        }

        public Comment(string content) : this("Anonymous", content)
        {
        }

        public Comment(string username, string content)
        {
            Username = username;
            Content = content;
            DateModified = DateTime.Now;
        }

        public static Comment AskComment(string username)
        {
            Console.Write("[?] Comment: ");
            return new Comment(username, Console.ReadLine());
        }

        public override string ToString() => $"\t[{Username}] on {DateModified.ToString("dddd, dd MMMM yyyy")}:{Environment.NewLine}\t+ {string.Join($"{Environment.NewLine}\t+ ", Content.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).SelectMany(x => x.SplitOnLength(80).Select(u => u.Trim())))}";
    }
}
