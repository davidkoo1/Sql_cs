using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace ProbableFinalExer
{
    public static class DatabaseConnector
    {

        public static string ConnectionStr = @"Data Source=DESKTOP-IQ404L4;Initial Catalog=TestingApp;Integrated Security=true;";

        public static List<Score> GetScoreFromTets()
        {
            List<Score> ret = new List<Score>();

            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();

                SqlCommand command;
                SqlDataReader dataReader;
                string GetSqlCommand;
                GetSqlCommand = "SELECT * FROM TestTotalScores";

                command = new SqlCommand(GetSqlCommand, connection);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Score s = new Score();

                    s.testID = dataReader.GetInt32(0);
                    s.score = dataReader.GetDecimal(1);

                    ret.Add(s);
                }

            }
            return ret;
        }

        public static List<Test> GetTests()
        {
            List<Test> ret = new List<Test>();

            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();

                SqlCommand command;
                SqlDataReader dataReader;
                string GetSqlCommand;
                GetSqlCommand = "SELECT * FROM Test";

                command = new SqlCommand(GetSqlCommand, connection);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Test t = new Test();

                    t.IDTest = dataReader.GetInt32(0);
                    t.testName = dataReader.GetString(1);
                    t.testTime = dataReader.GetInt32(2);
                    t.testDiscripcion = dataReader.GetString(3);
                    t.CreaterID = dataReader.GetInt32(4);

                    ret.Add(t);
                }

            }
            return ret;
        }


        public static List<Question> GetQuestions()
        {
            List<Question> ret = new List<Question>();

            using(SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();

                SqlCommand command;
                SqlDataReader dataReader;
                string GetSqlCommand;

                GetSqlCommand = "SELECT * FROM Question";

                command = new SqlCommand(GetSqlCommand, connection);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Question q = new Question();

                    q.testID = dataReader.GetInt32(0);
                    q.IDQuestion = dataReader.GetInt32(1);
                    q.questionNumber = dataReader.GetInt32(2);
                    q.questionDescription = dataReader.GetString(3);

                    ret.Add(q);
                }

            }

            return ret;
        }


        public static List<OptionAnswer> GetOptionAnswer()
        {
            List<OptionAnswer> ret = new List<OptionAnswer>();

            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();

                SqlCommand command;
                SqlDataReader dataReader;
                string GetSqlCommand;

                GetSqlCommand = "SELECT * FROM OptionAnswer";

                command = new SqlCommand(GetSqlCommand, connection);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    OptionAnswer oA = new OptionAnswer();

                    oA.IDAnswer = dataReader.GetInt32(0);
                    oA.questionID = dataReader.GetInt32(1);
                    oA.answerDescription = dataReader.GetString(2);

                    ret.Add(oA);
                }

            }

            return ret;
        }


        public static List<Result> GetResult()
        {
            List<Result> ret = new List<Result>();

            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();

                SqlCommand command;
                SqlDataReader dataReader;
                string GetSqlCommand;

                GetSqlCommand = "SELECT * FROM Result";

                command = new SqlCommand(GetSqlCommand, connection);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Result r = new Result();

                    r.testID = dataReader.GetInt32(0);
                    r.IDResult = dataReader.GetInt32(1);
                    r.resultDescripcion = dataReader.GetString(2);

                    ret.Add(r);
                }

            }

            return ret;
        }

        public static List<Temp> GetTemp()
        {
            List<Temp> ret = new List<Temp>();

            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();

                SqlCommand command;
                SqlDataReader dataReader;
                string GetSqlCommand;

                GetSqlCommand = "SELECT * FROM Temp";

                command = new SqlCommand(GetSqlCommand, connection);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Temp t = new Temp();

                    t.answerID = dataReader.GetInt32(0);
                    t.resultID = dataReader.GetInt32(1);
                    t.countScore = dataReader.GetDecimal(2);

                    ret.Add(t);
                }

            }

            return ret;
        }

        public static List<User> GetUsers()
        {
            List<User> ret = new List<User>();

            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();

                SqlCommand command;
                SqlDataReader dataReader;
                string GetSqlCommand;

                GetSqlCommand = "select * FROM Users";

                command = new SqlCommand(GetSqlCommand, connection);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    User u = new User();

                    u.IDUser = dataReader.GetInt32(0);
                    u.UserLogin = dataReader.GetString(1);
                    u.UserPassword = dataReader.GetString(2);
                    u.UserName = dataReader.GetString(3);
                    u.UserSurname = dataReader.GetString(4);
                    u.UserStanding = dataReader.GetInt32(5);
                    u.UserWhoRegId = dataReader.GetInt32(6);
                    u.UserStatus = dataReader.GetBoolean(7);

                    ret.Add(u);
                }

            }

            return ret;
        }
        public static void AddTest(Test newTest)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                String query = "INSERT INTO Test VALUES (@TestId, @TestName, @TestTime, @TestDiscripcion)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TestId", newTest.IDTest);
                    command.Parameters.AddWithValue("@TestName", newTest.testName);
                    command.Parameters.AddWithValue("@TestTime", newTest.testTime);
                    command.Parameters.AddWithValue("@TestDiscripcion", newTest.testDiscripcion);


                    connection.Open();
                    command.ExecuteNonQuery();


                }
            }
        }

        public static void AddQuestions(Question newQuestion)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                String query = "INSERT INTO Test VALUES (@testID, @IDQuestion, @questionDescription)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testID", newQuestion.testID);
                    command.Parameters.AddWithValue("@IDQuestion", newQuestion.IDQuestion);
                    command.Parameters.AddWithValue("@questionDescription", newQuestion.questionDescription);
                    

                    connection.Open();
                    command.ExecuteNonQuery();


                }
            }
        }

    }
}
