using System;

namespace ProbableFinalExer
{
    internal class Test
    {
        internal int IDTest;
        internal string testName;
        internal int testTime;
        internal string testDiscripcion;
        internal int CreaterID;

        //internal string IsCreater { }
        internal void PrintInfo()
        {
            Console.WriteLine(IDTest + " " + testName + " " + testTime + " " + testDiscripcion + " " + CreaterID);
        }
    }

    internal class Question
    {
        internal int testID;
        internal int IDQuestion;
        internal int questionNumber;
        internal string questionDescription;

        internal void PrintfInfo()
        {
            Console.WriteLine(testID + " " + IDQuestion + " " + questionNumber + " " + questionDescription);
        }
    }

    internal class OptionAnswer
    {
        internal int IDAnswer;
        internal int questionID;
        internal string answerDescription;

        internal void PrintInfo()
        {
            Console.WriteLine(IDAnswer + " " + questionID + " " + answerDescription);
        }
    }

    internal class Result
    {
        internal int testID;
        internal int IDResult;
        internal string resultDescripcion;

        internal void PrintInfo()
        {
            Console.WriteLine(testID + " " + IDResult + " " + resultDescripcion);
        }
    }

    internal class Temp
    {
        internal int answerID;
        internal int resultID;
        internal decimal countScore;

        internal void PrintInfo()
        {
            Console.WriteLine(answerID + " " + resultID + " " + countScore);
        }
    }


    internal class User
    {
        internal int IDUser;
        internal string UserLogin;
        internal string UserPassword;
        internal string UserName;
        internal string UserSurname;
        internal int UserStanding;
        internal int UserWhoRegId;
        internal bool UserStatus;

        
        internal bool IsAdmin { get { return UserStanding == 0; } }
        internal bool IsTeacher { get { return UserStanding == 1; } }

        internal string IsStanding { get { if (UserStanding == 0) return "ADMIN"; else if (UserStanding == 1) return "Teacher"; else return "Student"; } }
        internal string IsStatus { get { if (UserStatus) return "Online"; else return "Offline"; } }
        internal void PrintInfo()
        {
            Console.WriteLine(IDUser + " " + UserLogin + " " + UserPassword + " " + UserName + " " + UserSurname + " " + IsStanding + " " +
                + UserWhoRegId + " " + IsStatus);
        }
    }

    internal class Try
    {
        internal int IDTry;
        internal int userID;
        internal int testID;
        internal string Start;
        internal string Finish;

        internal void PrintInfo()
        {
            Console.WriteLine(IDTry + " " + userID + " " + testID + " " + Start + " " + Finish);
        }
    }

    internal class UserAnswer
    {
        internal int tryID;
        internal int answerID;

        internal void PrintfInfo()
        {
            Console.WriteLine(tryID + " " + answerID);
        }
    }
    //View
    internal class Score
    {
        internal int testID;
        internal decimal score;

        internal void PrintInfo()
        {
            Console.WriteLine(testID + " " + score);
        }
    }

    
}
