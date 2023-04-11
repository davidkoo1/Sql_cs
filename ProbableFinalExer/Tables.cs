using System;

namespace ProbableFinalExer
{
    public class Test
    {
        public int IDTest;
        public string testName;
        public int testTime;
        public string testDiscripcion;

        public void PrintInfo()
        {
            Console.WriteLine(IDTest + " " + testName + " " + testTime + " " + testDiscripcion);
        }
    }

    public class Question
    {
        public int testID;
        public int IDQuestion;
        public int questionNumber;
        public string questionDescription;

        public void PrintfInfo()
        {
            Console.WriteLine(testID + " " + IDQuestion + " " + questionNumber + " " + questionDescription);
        }
    }

    public class OptionAnswer
    {
        public int IDAnswer;
        public int questionID;
        public string answerDescription;

        public void PrintInfo()
        {
            Console.WriteLine(IDAnswer + " " + questionID + " " + answerDescription);
        }
    }

    public class Result
    {
        public int testID;
        public int IDResult;
        public string resultDescripcion;

        public void PrintInfo()
        {
            Console.WriteLine(testID + " " + IDResult + " " + resultDescripcion);
        }
    }

    public class Temp
    {
        public int answerID;
        public int resultID;
        public decimal countScore;

        public void PrintInfo()
        {
            Console.WriteLine(answerID + " " + resultID + " " + countScore);
        }
    }

    //View
    public class Score
    {
        public int testID;
        public decimal score;

        public void PrintInfo()
        {
            Console.WriteLine(testID + " " + score);
        }
    }
}
