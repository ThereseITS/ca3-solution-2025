using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ca3_solution_2025
{
    internal class Player
    {
        string _name;
        int _points = 0;
        int _nWon = 0;
        int _nDrawn = 0;
        int _nLost = 0;

        public string Name { get { return _name; } }
        public int Points { get { return _points; } }
        public int NWon { get { return _nWon; } }
        public int NDrawn { get { return _nDrawn; } }
        public int NLost{ get { return _nLost; } }
        public Player(string name)
        {
            this._name = name;
        }
        public int AddResults(string result)
        {
           
            switch(result.ToLower())
            {
                case "won": _points += 2;_nWon++;  break;
                case "drawn": _points += 1;_nDrawn++; break;
                case "lost": _nLost++; break;
                default: break;
            }
            return _points;
        }
        public int GamesPlayed()
        {
            return _nDrawn + _nWon + _nLost;
        }
        public override string ToString()
        {
            return $"{_name,-10} {this.GamesPlayed(),-10} {_nWon,-10} {_nDrawn,-10} {_nLost,-10 } {_points,-10}";
        }
    }
}
