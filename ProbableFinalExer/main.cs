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
            DateTime dateTime = new DateTime();
            dateTime = DateTime.Now;
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
            foreach (var item in score)
                item.PrintInfo();
            */
            foreach (var item in users)
                item.PrintInfo();

            User currentUser = new User();
            bool input = false;
            while (!input)
            {
                Console.Write("Login: ");
                string inputLogin = Console.ReadLine();
                Console.Write("Password: ");
                string intputPassword = Console.ReadLine();


                foreach (var item in users)
                {
                    if (item.UserLogin == inputLogin && item.UserPassword == intputPassword)
                    {
                        currentUser = item;
                        currentUser.UserStatus = true;
                        input = true;
                        break;
                    }
                }
            }

            
            //if(input && currentUser.IsAdmin)

            /*
            foreach (var item in tests)
            {
                Console.Write(item.testName + " ");
                foreach (var item2 in users)
                {
                    if (item2.IDUser == item.CreaterID)
                        Console.WriteLine(item2.UserName);
                }
                
            }
            */
            foreach (var item in tests)
                item.PrintInfo();

            int selectTestUser = int.Parse(Console.ReadLine());
            var selectTest = tests.Where(x => x.IDTest == selectTestUser).FirstOrDefault();
            
            Console.WriteLine("Вы выбрали тест - " + "\"" + selectTest.testName + "\"");

            var selectQuestion = questions.Where(x => x.testID == selectTestUser).OrderBy(question => question.questionNumber).ToList();
            Try newTry = new Try();
            newTry.IDTry = GetNewTryId(trys);
            newTry.userID = currentUser.IDUser;
            newTry.testID = selectTestUser;
            newTry.Start = "Yes";
            //DateTime customDate = new DateTime(2023, 4, 9, 12, 30, 0, 0);
            newTry.Finish = "NO";
            DatabaseConnector.AddTry(newTry);
            trys.Add(newTry);
            foreach (var item in trys)
            {
                item.PrintInfo();
            }

            int countQuestion = selectQuestion.Count();

            int countRes = results.Where(x => x.testID == selectTestUser).Count();
            int min = results.Where(x => x.testID == selectTestUser).Min(x => x.IDResult);

            decimal[] res = new decimal[countRes];
            decimal sum = 0;

            int[] q = new int[countQuestion];
            int qNow = 0;
            Random rnd = new Random();
           
            for(int i = 0; i < countQuestion; i++)
            {
               // int qNow = 0, l = 0;
                bool isFlag = true;
                while (isFlag)
                {
                    //qNow = rnd.Next(selectQuestion.Min(x => x.IDQuestion), selectQuestion.Max(x => x.IDQuestion) + 1) - 1;
                    qNow = rnd.Next(0, countQuestion + 1);
                    if (qNow != 0)
                        qNow -= 1;
                    q[i] = qNow;
                    if (i > 0)
                    {
                        for (int l = 0; l < i; l++)
                        {
                            if (q[l] == qNow)
                            {
                                isFlag = true;
                                break;
                                
                            }
                            else
                                isFlag = false;
                        }
                    }
                    else isFlag = false;
                }



                //Console.WriteLine("\t\t"+ qNow);


                //Console.WriteLine((i+1) + " " + selectQuestion[i].questionDescription);
                Console.WriteLine((i + 1) + " " + selectQuestion[qNow].questionDescription);
                //var selectAnswer = answers.Where(x => x.questionID == selectQuestion[i].IDQuestion).ToList();
                var selectAnswer = answers.Where(x => x.questionID == selectQuestion[qNow].IDQuestion).ToList();
                int countAnswer = selectAnswer.Count();
                

                for(int j = 0; j < countAnswer; j++)
                    Console.WriteLine((j+1) + " " + selectAnswer[j].answerDescription);

                int answUser = int.Parse(Console.ReadLine());
                var selectedAnswer = selectAnswer[answUser - 1];

                for(int j = 0; j < countAnswer; j++)
                {
                    var selectTemp = temps.Where(x => x.answerID == selectAnswer[j].IDAnswer).ToList();
                    int minAnsw = selectTemp.Min(x => x.answerID);
                    foreach(var temp in selectTemp)
                    {
                        //temp.PrintInfo();
                        if(temp.answerID == selectedAnswer.IDAnswer)
                        {
                            //Console.WriteLine("yes");
                            res[temp.resultID - min] += temp.countScore;
                            sum += temp.countScore;
                            
                        }
                    }

                }                 
            }  

            /*
            for(int i = 0; i < res.Length; i++)
                Console.WriteLine((i + 1) + " " + res[i]);
            //*/

            var selectRes = results.Where(x => x.testID == selectTestUser).ToList();

            Console.WriteLine("\tРезультаты:");
            for (int i = 0; i < res.Length; i++)
                Console.WriteLine("На " + Math.Round((double)(res[i] / sum) / 0.01) + "% похож на " + selectRes[i].resultDescripcion);
            
/*
            while (true)
            {
                Console.WriteLine("Menu: ");
                Console.WriteLine("0 - Exit");
                Console.WriteLine("1 - Show Tables");
                Console.WriteLine("2 - Pass test");

                int k = int.Parse(Console.ReadLine());

                switch (k)
                {
                    case 0: return 0;
                    case 1:
                        {
                            bool isMenu = true;
                            while (isMenu)
                            {
                                Console.WriteLine("Table:");
                                Console.WriteLine("0 - Return Menu");
                                Console.WriteLine("1 - Tests");
                                Console.WriteLine("2 - Questions");
                                Console.WriteLine("3 - OptionAnswer");
                                Console.WriteLine("4 - Result");
                                Console.WriteLine("5 - Temp");

                                int tablesShow = int.Parse(Console.ReadLine());

                                switch (tablesShow)
                                {
                                    case 0: isMenu = false; break;
                                    case 1://Test
                                        {
                                            Console.WriteLine("Table [Test]");
                                            Console.WriteLine("1 - Show");
                                            Console.WriteLine("2 - Insert");
                                            Console.WriteLine("3 - Update");

                                            int answUser = int.Parse(Console.ReadLine());

                                            switch (answUser)
                                            {
                                                case 1:
                                                    {
                                                        Console.WriteLine("IDTest | Name | Time | Description");
                                                        foreach (var test in tests)
                                                            test.PrintInfo();

                                                        break;
                                                    }
                                                case 2:
                                                    {

                                                        Console.WriteLine("INSERT TO TABLE");
                                                        Console.Write("Test name >> ");
                                                        string testName = Console.ReadLine();
                                                        Console.Write("Time for test >> ");
                                                        int testTime = int.Parse(Console.ReadLine());
                                                        Console.WriteLine("Test Description :");
                                                        string testDescription = Console.ReadLine();

                                                        Test newTest = new Test();
                                                        newTest.IDTest = GetNewTestId(tests);
                                                        newTest.testName = testName;
                                                        newTest.testTime = testTime;
                                                        newTest.testDiscripcion = testDescription;

                                                        DatabaseConnector.AddTest(newTest);
                                                        tests.Add(newTest);
                                                        Console.WriteLine("Test add.");


                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }

                                            break;
                                        }
                                    case 2://Question
                                        {
                                            Console.WriteLine("Table[Question]");
                                            Console.WriteLine("IDTest | IDQuestion | Number | Description");
                                            foreach (var question in questions)
                                                question.PrintfInfo();
                                            break;
                                        }
                                    case 3://OptionAnswer
                                        {
                                            Console.WriteLine("Table[OptionAnswer]");
                                            Console.WriteLine("IDAnswer | IDQuestion | Description");
                                            foreach (var answer in answers)
                                                answer.PrintInfo();

                                            break;
                                        }
                                    case 4://Result
                                        {
                                            Console.WriteLine("Table[Result]");
                                            Console.WriteLine("IDTest | IDResult | Description");
                                            foreach (var result in results)
                                                result.PrintInfo();

                                            break;
                                        }
                                    case 5://Temp
                                        {
                                            Console.WriteLine("Table[Temp]");
                                            Console.WriteLine("IDAnswer | IDResult | Score");
                                            foreach (var temp in temps)
                                                temp.PrintInfo();

                                            break;
                                        }
                                    default: break;
                                }   
                            }
                            break;
                        }
                    case 2:
                        {
                           
                                    
                         
                                break;
                        }
                    default: break;
                    

                }
            }

            
            */


            Console.WriteLine("\n\tExit...");
            return 0;
        }
    }
}
