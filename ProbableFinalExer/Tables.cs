using System;

namespace ProbableFinalExer
{
    public class Test
    {
        public int IDTest;
        public string testName;
        public int testTime;
        public string testDiscripcion;
        public int CreaterID;

        public void PrintInfo()
        {
            Console.WriteLine(IDTest + " " + testName + " " + testTime + " " + testDiscripcion + " " + CreaterID);
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


    public class User
    {
        public int IDUser;
        public string UserLogin;
        public string UserPassword;
        public string UserName;
        public string UserSurname;
        public int UserStanding;
        public int UserWhoRegId;
        public bool UserStatus;

        
        public bool IsAdmin { get { return UserStanding == 0; } }
        public bool IsTeacher { get { return UserStanding == 1; } }

        public void PrintInfo()
        {
            Console.WriteLine(IDUser + " " + UserLogin + " " + UserPassword + " " + UserName + " " + UserSurname + " " + UserStanding + " " +
                + UserWhoRegId + " " + UserStatus);
        }
    }

    public class Try
    {
        public int IDTry;
        public int userID;
        public int testID;
        public string Start;
        public string Finish;

        public void PrintInfo()
        {
            Console.WriteLine(IDTry + " " + userID + " " + testID + " " + Start + " " + Finish);
        }
    }

    public class UserAnswer
    {
        public int tryID;
        public int answerID;

        public void PrintfInfo()
        {
            Console.WriteLine(tryID + " " + answerID);
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
