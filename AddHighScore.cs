﻿using System;
using System.Collections.Generic;

namespace SubmarineGame
{
    [Serializable()]
    public class AddHighScore : IComparable
    {
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public int CompareTo(object obj)
        {
            AddHighScore otherScore = (AddHighScore)obj;
            if (Score == otherScore.Score)
                return 0;

            if (Score < otherScore.Score)
                return 1;

            return -1;
        }
    }
}