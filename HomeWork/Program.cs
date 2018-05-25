using System.Linq;
using HomeWork.Structures;

namespace HomeWork
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var trie = new Trie();
            trie.Add("Дарья");
            trie.Add("Даровали");
            var a = trie.GetByPrefix("Да").ToList();
        }
    }
}