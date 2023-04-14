using System;
using System.Collections.Generic;
using System.Linq;

namespace ProbableFinalExer
{
    class main
    {
        static int GetNewTestId(List<Test> tests)
        {
            int ret = tests.Max(x => x.IDTest);
            return ret + 1;
        }

        int GetNewQuestionId(List<Question> questions)
        {
            return questions.Max(x => x.IDQuestion) + 1;
        }

        static int GetNewTryId(List<Try> trys)
        {
            return trys.Max(x => x.IDTry) + 1;
        }


        static int Main(string[] args)
        {
            
            //Tables
            List<Test> tests = DatabaseConnector.GetTests();
            List<Question> questions = DatabaseConnector.GetQuestions();
            List<OptionAnswer> answers = DatabaseConnector.GetOptionAnswer();
            List<Result> results = DatabaseConnector.GetResult();
            List<Temp> temps = DatabaseConnector.GetTemp();
            List<User> users = DatabaseConnector.GetUsers();
            List<Try> trys = DatabaseConnector.GetTry();
            List<UserAnswer> userAnswers = DatabaseConnector.GetAnswerUser();
            //View
            List<Score> score = DatabaseConnector.GetScoreFromTets();
/*
            foreach (var item in users)
                item.PrintInfo();
*/


            User currentUser = new User();
            bool input = false;
            while (!input)
            {
                Console.Write("Login: ");
                string inputLogin = "Alehandro";
                Console.Write("Password: ");
                string intputPassword = "Nagibator3000";


                foreach (var user in users)
                {
                    if (user.UserLogin == inputLogin && user.UserPassword == intputPassword)
                    {
                        currentUser = user;
                        currentUser.UserStatus = true;
                        input = true;
                        break;
                    }
                }
            }


            if (currentUser.IsAdmin && input)
            {
                //рег учеников/учителей/созд тестов(с опр условиями)/тегов
            }
            else if (currentUser.IsTeacher && input)
            {
                //админ+realTime просмотр прохождения теста и просмотр результатов
            }
            else if (input)
            {
                bool exit = false;
                while (!exit)
                {
                    foreach (var test in tests)
                        test.PrintInfo();
                    Console.WriteLine("Выберите тест");
                    int selectTestUser = int.Parse(Console.ReadLine());

                    int coutTrysUser = trys.Where(x => x.testID == selectTestUser && x.userID == currentUser.IDUser).Count();
                    int countRes = results.Where(x => x.testID == selectTestUser).Count();
                    int min = results.Where(x => x.testID == selectTestUser).Min(x => x.IDResult);

                    decimal[] res = new decimal[countRes];
                    decimal sum = 0;
                    List<Result> selectRes = new List<Result>();
                    if (coutTrysUser > 0)
                    {
                        var testUsersTry = trys.Where(x => x.testID == selectTestUser && x.userID == currentUser.IDUser).ToList();
                        
                        bool show = false;
                        while (!show)
                        {
                            for (int i = 0; i < coutTrysUser; i++)
                            {
                                if (testUsersTry[i].testID == selectTestUser && currentUser.IDUser == testUsersTry[i].userID)
                                {
                                    Console.WriteLine("Результаты " + (i + 1) + " попытки");
                                }
                            }
                            Console.WriteLine("Skip - 0");
                            int idResPreviousTrys = int.Parse(Console.ReadLine());
                            if (idResPreviousTrys <= 0) show = true;
                            if (!show)
                            {
                                foreach (var item in userAnswers)
                                {
                                    if (item.tryID == testUsersTry[idResPreviousTrys - 1].IDTry)
                                        foreach (var temp in temps)
                                        {
                                            if (temp.answerID == item.answerID)
                                            {
                                                //Console.WriteLine("Yes");
                                                
                                                res[temp.resultID - min] += temp.countScore;
                                                sum += temp.countScore;

                                            }
                                        }
                                }

                                selectRes = results.Where(x => x.testID == selectTestUser).ToList();

                                Console.WriteLine("\tРезультаты:");
                                for (int i = 0; i < res.Length; i++)
                                {
                                    Console.WriteLine("На " + Math.Round((double)(res[i] / sum) / 0.01) + "% похож на " + selectRes[i].resultDescripcion);
                                    res[i] = 0;
                                }
                                sum = 0;
                            }
                            

                        }
                    }

                    

                    var selectTest = tests.Where(x => x.IDTest == selectTestUser).FirstOrDefault();

                    Console.WriteLine("Вы выбрали тест - " + "\"" + selectTest.testName + "\"");

                    var selectQuestion = questions.Where(x => x.testID == selectTestUser).OrderBy(question => question.questionNumber).ToList();

                    Try newTry = new Try();
                    newTry.IDTry = GetNewTryId(trys);
                    newTry.userID = currentUser.IDUser;
                    newTry.testID = selectTestUser;
                    newTry.Start = DateTime.Now.ToString();
                    newTry.Finish = "NO";

                    DatabaseConnector.AddTry(newTry);
                    trys.Add(newTry);


                    int countQuestion = selectQuestion.Count();

                    //int countRes = results.Where(x => x.testID == selectTestUser).Count();

                    //по времени 
                    bool flag = true;
                    for (int i = 0; i < countQuestion || flag; i++)
                    {
                        Console.WriteLine((i + 1) + " " + selectQuestion[i].questionDescription);
                        //Console.WriteLine((i + 1) + " " + selectQuestion[qNow].questionDescription);
                        var selectAnswer = answers.Where(x => x.questionID == selectQuestion[i].IDQuestion).ToList();
                        //var selectAnswer = answers.Where(x => x.questionID == selectQuestion[qNow].IDQuestion).ToList();
                        int countAnswer = selectAnswer.Count();


                        for (int j = 0; j < countAnswer; j++)
                            Console.WriteLine((j + 1) + " " + selectAnswer[j].answerDescription);

                        int answUser = int.Parse(Console.ReadLine());

                        UserAnswer answs = new UserAnswer();
                        answs.tryID = newTry.IDTry;
                        answs.answerID = selectAnswer[answUser - 1].IDAnswer;

                        DatabaseConnector.AddTryWithAnswer(answs);
                        userAnswers.Add(answs);

                        /*

                        */


                    }
                    newTry.Finish = DateTime.Now.ToString();
                    DatabaseConnector.UpdateTry(newTry);
                    //Добавить обдовленную запись в список(обновить)

                    /*
                    foreach (var trysUser in trys)
                    {
                        if (trysUser.userID == currentUser.IDUser)
                            trysUser.PrintInfo();
                    }

                    */
                    foreach (var item in userAnswers)
                    {
                        if (item.tryID == newTry.IDTry)
                            foreach (var temp in temps)
                            {
                                if (temp.answerID == item.answerID)
                                {
                                    //Console.WriteLine("Yes");

                                    res[temp.resultID - min] += temp.countScore;
                                    sum += temp.countScore;

                                }
                            }
                    }

                    selectRes = results.Where(x => x.testID == selectTestUser).ToList();

                    Console.WriteLine("\tРезультаты:");
                    for (int i = 0; i < res.Length; i++)
                        Console.WriteLine("На " + Math.Round((double)(res[i] / sum) / 0.01) + "% похож на " + selectRes[i].resultDescripcion);
                }
            }
            Console.WriteLine("Exit");
            return 0;

        }  

    }
}

/*
                     var selectedAnswer = selectAnswer[answUser - 1];
                     for (int j = 0; j < countAnswer; j++)
                    {
                        var selectTemp = temps.Where(x => x.answerID == selectAnswer[j].IDAnswer).ToList();
                        int minAnsw = selectTemp.Min(x => x.answerID);
                        foreach (var temp in selectTemp)
                        {
                            //temp.PrintInfo();
                            if (temp.answerID == selectedAnswer.IDAnswer)
                            {
                                //Console.WriteLine("yes");
                                res[temp.resultID - min] += temp.countScore;
                                sum += temp.countScore;

                            }
                        }

                    }
 */