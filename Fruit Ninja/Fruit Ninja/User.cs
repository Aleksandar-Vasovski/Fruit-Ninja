using System;
using System.Collections.Generic;

namespace Fruit_Ninja
{
    [Serializable]
    public class User
    {
        public string name;
        public List<Score> scores;

        public User(string name)
        {
            scores = new List<Score>();
            this.name = name;
        }

        public override string ToString()
        {
            return name;
        }

        /*<summary>
        Dodavanje na poeni na korisnikot
       </summary>*/
        public void addScore(Score score)
        {
            scores.Add(score);
        }

    }
}
