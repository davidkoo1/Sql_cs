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

        static int GetNewUserId(List<User> users)
        {
            return users.Max(x => x.IDUser) + 1;
        }

        static int Main(string[] args)
        {
            //вопросы дня
            /*
             1. Как устранить логические ошибки(типо повторение кода, мб где-то лучше использовать внутрений sql(типо функции и прочее)
             2. Как это будет работать не с моего компа(и как сделать чтобы норм работало)
             3. Дальше визуальная часть или осталось что-то доработать(*теги)

             */
            
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
                string inputLogin = Console.ReadLine();
                Console.Write("Password: ");
                string intputPassword = Console.ReadLine();


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
                while (true)
                {
                    Console.WriteLine("Menu for - " + currentUser.UserName + " " + currentUser.UserSurname);
                    Console.WriteLine("0 - Exit");
                    Console.WriteLine("1 - Users");
                    Console.WriteLine("2 - Tests");

                    int k = int.Parse(Console.ReadLine());

                    switch (k)
                    {
                        case 0: return 0;
                        case 1://Users
                            {
                                bool ret = true;
                                while (ret)
                                {
                                    Console.WriteLine("Users");
                                    Console.WriteLine("0 - Return main menu");
                                    Console.WriteLine("1 - Show");
                                    Console.WriteLine("2 - Add new");
                                    Console.WriteLine("3 - Update");
                                    Console.WriteLine("4 - Delete");

                                    int action = int.Parse(Console.ReadLine());

                                    switch (action)
                                    {
                                        case 0: ret = false; break;
                                        case 1: //ShowUsers
                                            {
                                                Console.WriteLine("Show : ");
                                                Console.WriteLine("0 - Everyone");
                                                Console.WriteLine("1 - Admins");
                                                Console.WriteLine("2 - Teachers");
                                                Console.WriteLine("3 - Students");
                                                Console.WriteLine("4 - History");

                                                int standing = int.Parse(Console.ReadLine());

                                                Console.WriteLine("UserId | UserLogin | UserPassword | UserName | UserSurname | UserStanding | WhoRed | StatusinLine: ");
                                                switch (standing)
                                                {
                                                    case 0:
                                                        {
                                                            foreach (var user in users)
                                                                user.PrintInfo();
                                                            break;
                                                        }
                                                    case 1 or 2 or 3:
                                                        {
                                                            foreach (var user in users)
                                                                if (user.UserStanding == standing - 1)
                                                                    user.PrintInfo();
                                                            break;
                                                        }
                                                    case 4:
                                                        {
                                                            Console.WriteLine("Input search Name : ");
                                                            string searchName = Console.ReadLine();
                                                            Console.WriteLine("Input search Surname : ");
                                                            string searchSurname = Console.ReadLine();
                                                            User searchUser = new User();
                                                            foreach (var user in users)
                                                                if (user.UserName == searchName && user.UserSurname == searchSurname)
                                                                    searchUser = user;
                                                            Console.WriteLine("Tests created by " + searchUser.UserName);
                                                            foreach (var test in tests)
                                                                if (test.CreaterID == searchUser.IDUser)
                                                                    test.PrintInfo();
                                                            Console.WriteLine("\nTrys ");
                                                            foreach (var userTry in trys)
                                                                if (userTry.userID == searchUser.IDUser)
                                                                    userTry.PrintInfo();
                                                            break;
                                                        }
                                                    default:
                                                        break;
                                                }
                                                 
                                                break;
                                            }
                                        case 2://Add
                                            {
                                                Console.WriteLine("Add");
                                                Console.WriteLine("0 - Return");
                                                Console.WriteLine("1 - Tegs for user");
                                                Console.WriteLine("2 - Only user");
                                                //3 - 1&2

                                                int add = int.Parse(Console.ReadLine());

                                                switch (add)
                                                {
                                                    case 0: break;
                                                    case 1:
                                                        {
                                                            break;
                                                        }
                                                    case 2:
                                                        {
                                                            User newUser = new User();
                                                            newUser.IDUser = GetNewUserId(users);
                                                            Console.WriteLine("Input login for new user");
                                                            newUser.UserLogin = Console.ReadLine();
                                                            Console.WriteLine("Input password for new user");
                                                            newUser.UserPassword = Console.ReadLine();
                                                            Console.WriteLine("Input name for new user");
                                                            newUser.UserName = Console.ReadLine();
                                                            Console.WriteLine("Input surname for new user");
                                                            newUser.UserSurname = Console.ReadLine();
                                                            Console.WriteLine("Who is:\n0 - admin\n1 - Teacher\n2 - Student");
                                                            int standing = int.Parse(Console.ReadLine());
                                                            newUser.UserStanding = standing;
                                                            newUser.UserWhoRegId = currentUser.IDUser;
                                                            newUser.UserStatus = false;

                                                            DatabaseConnector.AddUser(newUser);
                                                            users.Add(newUser);


                                                            break;
                                                        }
                                                    default:
                                                        break;
                                                }

                                                break;
                                            }
                                        case 3://Update
                                            {
                                                Console.WriteLine("Input name user");
                                                string searchName = Console.ReadLine();
                                                Console.WriteLine("Input surname user");
                                                string searchSurname = Console.ReadLine();
                                                User updateUser = new User();
                                                foreach (var user in users)
                                                    if (user.UserName == searchName && user.UserSurname == searchSurname)
                                                    {
                                                        updateUser = user;
                                                        break;
                                                    }

                                                bool save = false;
                                                while(!save)
                                                {
                                                    Console.WriteLine("Update: ");
                                                    
                                                    Console.WriteLine("0 - Exit");
                                                    Console.WriteLine("1 - Name");
                                                    Console.WriteLine("2 - Surname");
                                                    Console.WriteLine("3 - Login");
                                                    Console.WriteLine("4 - Password");
                                                    Console.WriteLine("5 - Standing");
                                                    Console.WriteLine("6 - Evrything");

                                                    

                                                    //6
                                                    updateUser.UserName = Console.ReadLine();
                                                    updateUser.UserSurname = Console.ReadLine();
                                                    updateUser.UserLogin = Console.ReadLine();
                                                    updateUser.UserPassword = Console.ReadLine();
                                                    updateUser.UserStanding = int.Parse(Console.ReadLine());

                                                    DatabaseConnector.UpdateUser(updateUser);
                                                    //обновить в списке надо?//не надо, а вот тут я не понял почему 

                                                    save = true;
                                                }

                                                break;
                                            }
                                        case 4://Delete
                                            {
                                                Console.WriteLine("Input name user");
                                                string searchName = Console.ReadLine();
                                                Console.WriteLine("Input surname user");
                                                string searchSurname = Console.ReadLine();
                                                User deleteUser = new User();
                                                foreach (var user in users)
                                                    if (user.UserName == searchName && user.UserSurname == searchSurname)
                                                    {
                                                        deleteUser = user;
                                                        break;
                                                    }
                                                DatabaseConnector.DeleteUser(deleteUser);
                                                //а вот тут список обновить надобно, как работают у нас списки с бд?-_-
                                                break;
                                            }
                                    
                                    }


                                    
                                }
                                break;
                            }
                        case 2:
                            {

                                foreach (var test in tests)
                                    test.PrintInfo();

                                Console.WriteLine("Input selected testId");
                                int selectTest = int.Parse(Console.ReadLine());

                                foreach(var question in questions)
                                {
                                    if (question.testID == selectTest)
                                    {
                                        Console.WriteLine(question.questionDescription);
                                        foreach (var ans in answers)
                                            if(ans.questionID == question.IDQuestion)
                                             Console.WriteLine(ans.answerDescription);
                                    }
                                       
                                }

                                foreach(var showTry in trys)
                                {
                                    if (showTry.testID == selectTest)
                                    {
                                        foreach(var user in users)
                                            if(user.IDUser == showTry.userID )//те кто онлайн к примеру или по тегу или просмотреть результаты позьзователя
                                                Console.WriteLine(user.UserName + " "+ showTry.Start + " " + showTry.Finish);
                                    }
                                       
                                }

                                break;
                            }
                        default:
                            break;
                    }
                }
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
                    int time = selectTest.testTime;
                    if (time <= 0)
                        time = int.MaxValue - 1;
                    DateTime dt = DateTime.Now.AddSeconds(time);
                    for (int i = 0; i < countQuestion && DateTime.Now <= dt; i++)
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
                        //в список надо?
                    }
                    newTry.Finish = DateTime.Now.ToString();
                    DatabaseConnector.UpdateTry(newTry);
                    //Добавить обдовленную запись в список(обновить)

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