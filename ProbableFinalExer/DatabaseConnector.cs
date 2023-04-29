using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace ProbableFinalExer
{
    internal static class DatabaseConnector
    {

        internal static readonly string ConnectionStr = @"Data Source=DESKTOP-IQ404L4;Initial Catalog=TestingApp;Integrated Security=true;";

        //view
        internal static List<Score> GetScoreFromTets()
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




        internal static List<Test> GetTests()
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
        internal static void AddTest(Test newTest)
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




        internal static List<Question> GetQuestions()
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
        internal static void AddQuestions(Question newQuestion)
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



        internal static List<OptionAnswer> GetOptionAnswer()
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



        internal static List<Result> GetResult()
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



        internal static List<Temp> GetTemp()
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



        internal static List<User> GetUsers()
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
        internal static void AddUser(User newUser)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                String query = "INSERT INTO Users VALUES (@UserId, @UserLogin, @UserPassword, @UserName, @UserSurname, @UserStanding, @UserWhoRegId, @UserStatus)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", newUser.IDUser);
                    command.Parameters.AddWithValue("@UserLogin", newUser.UserLogin);
                    command.Parameters.AddWithValue("@UserPassword", newUser.UserPassword);
                    command.Parameters.AddWithValue("@UserName", newUser.UserName);
                    command.Parameters.AddWithValue("@UserSurname", newUser.UserSurname);
                    command.Parameters.AddWithValue("@UserStanding", newUser.UserStanding);
                    command.Parameters.AddWithValue("@UserWhoRegId", newUser.UserWhoRegId);
                    command.Parameters.AddWithValue("@UserStatus", newUser.UserStatus);



                    connection.Open();
                    command.ExecuteNonQuery();


                }
            }
        }
        
        
        
        internal static List<Try> GetTry()
        {
            List<Try> ret = new List<Try>();

            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();

                SqlCommand command;
                SqlDataReader dataReader;
                string GetSqlCommand;

                GetSqlCommand = "select * FROM TryUsers";

                command = new SqlCommand(GetSqlCommand, connection);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Try t = new Try();

                    t.IDTry = dataReader.GetInt32(0);
                    t.userID = dataReader.GetInt32(1);
                    t.testID = dataReader.GetInt32(2);
                    t.Start = dataReader.GetString(3);
                    t.Finish = dataReader.GetString(4);

                    ret.Add(t);
                }

            }

            return ret;
        }
        internal static void AddTry(Try newTry)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                String query = "INSERT INTO TryUsers VALUES (@TryId, @IdUser, @IdTest, @Start, @Finish)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TryId", newTry.IDTry);
                    command.Parameters.AddWithValue("@IdUser", newTry.userID);
                    command.Parameters.AddWithValue("@IdTest", newTry.testID);
                    command.Parameters.AddWithValue("@Start", newTry.Start);
                    command.Parameters.AddWithValue("@Finish", newTry.Finish);


                    connection.Open();
                    command.ExecuteNonQuery();


                }
            }
        }
        internal static void UpdateTry(Try updateTry)//Uddate only finishtime
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                String query = "UPDATE TryUsers SET FinishTime = @FinishTime where TryId = @TryId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FinishTime", updateTry.Finish);
                    command.Parameters.AddWithValue("@TryId", updateTry.IDTry);


                    connection.Open();
                    command.ExecuteNonQuery();


                }
            }
        }

        internal static void UpdateUser(User updateUser)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                String query = "UPDATE Users SET UserLogin = @UserLogin, UserPassword = @UserPassword, UserName = @UserName, UserSurname = @UserSurname, UserStanding = @UserStanding WHERE UserId = @UserId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", updateUser.IDUser);
                    command.Parameters.AddWithValue("@UserLogin", updateUser.UserLogin);
                    command.Parameters.AddWithValue("@UserPassword", updateUser.UserPassword);
                    command.Parameters.AddWithValue("@UserName", updateUser.UserName);
                    command.Parameters.AddWithValue("@UserSurname", updateUser.UserSurname);
                    command.Parameters.AddWithValue("@UserStanding", updateUser.UserStanding);




                    connection.Open();
                    command.ExecuteNonQuery();


                }
            }
        }

        internal static void DeleteUser(User deleteUser)
        {

            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                string query = "DELETE FROM Users WHERE UserId = @UserId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", deleteUser.IDUser);

                    connection.Open();
                    command.ExecuteNonQuery();


                }
            }

        }


        internal static List<UserAnswer> GetAnswerUser()
        {
            List<UserAnswer> ret = new List<UserAnswer>();

            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();

                SqlCommand command;
                SqlDataReader dataReader;
                string GetSqlCommand;

                GetSqlCommand = "select * FROM UserAnswer";

                command = new SqlCommand(GetSqlCommand, connection);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    UserAnswer ua = new UserAnswer();

                    ua.tryID = dataReader.GetInt32(0);
                    ua.answerID = dataReader.GetInt32(1);

                    ret.Add(ua);
                }

            }

            return ret;
        }
        internal static void AddTryWithAnswer(UserAnswer newAnswerTry)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                String query = "INSERT INTO UserAnswer VALUES (@IdTry, @IdAnswer)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdTry", newAnswerTry.tryID);
                    command.Parameters.AddWithValue("@IdAnswer", newAnswerTry.answerID);


                    connection.Open();
                    command.ExecuteNonQuery();


                }
            }
        }

    }
}
