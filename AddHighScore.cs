using System;

namespace SubmarineGame
{
    [Serializable]
    public class AddHighScore : IComparable
    {
        //Class to handle serialization of highscores
        public string PlayerName { get; set; }
        public int Score { get; set; }

        //Compares highscore elements
        public int CompareTo(object obj)
        {
            var otherScore = (AddHighScore) obj;
            if (Score == otherScore.Score)
                return 0;

            if (Score < otherScore.Score)
                return 1;

            return -1;
        }
    }
}