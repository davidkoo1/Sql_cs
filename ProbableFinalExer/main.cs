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

            }
            else if(currentUser.IsTeacher && input)
            {

            }
            else if(input)
            {            
                foreach (var test in tests)
                    test.PrintInfo();

                int selectTestUser = int.Parse(Console.ReadLine());
                Console.WriteLine("Выберите тест"); 

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

                int countRes = results.Where(x => x.testID == selectTestUser).Count();
                int min = results.Where(x => x.testID == selectTestUser).Min(x => x.IDResult);

                decimal[] res = new decimal[countRes];
                decimal sum = 0;

                for (int i = 0; i < countQuestion; i++)
                {
                    Console.WriteLine((i + 1) + " " + selectQuestion[i].questionDescription);
                    //Console.WriteLine((i + 1) + " " + selectQuestion[qNow].questionDescription);
                    var selectAnswer = answers.Where(x => x.questionID == selectQuestion[i].IDQuestion).ToList();
                    //var selectAnswer = answers.Where(x => x.questionID == selectQuestion[qNow].IDQuestion).ToList();
                    int countAnswer = selectAnswer.Count();


                    for (int j = 0; j < countAnswer; j++)
                        Console.WriteLine((j + 1) + " " + selectAnswer[j].answerDescription);

                    int answUser = int.Parse(Console.ReadLine());
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
                }
                newTry.Finish = DateTime.Now.ToString();
                DatabaseConnector.UpdateTry(newTry);

                var selectRes = results.Where(x => x.testID == selectTestUser).ToList();

                Console.WriteLine("\tРезультаты:");
                for (int i = 0; i < res.Length; i++)
                    Console.WriteLine("На " + Math.Round((double)(res[i] / sum) / 0.01) + "% похож на " + selectRes[i].resultDescripcion);
            }
            Console.WriteLine("Exit");
            return 0;

        }  

    }
}
