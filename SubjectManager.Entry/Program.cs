namespace SubjectManager.Entry;

using Services;
using Utils;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var lessonService = new LessonService();
        var subjectService = new SubjectService();

        while (true)
        {
            Console.WriteLine("""
            Commands available
            'subjects' - display all subjects with further option to display all data about single selected subject
            'lessons' - display all lessons
            'exit' - exit the program
            """);
            Console.Write("Enter input: ");
            var input = Console.ReadLine()?.Trim() ?? string.Empty;
            if (input == "exit")
                break;
            switch (input)
            {
                case "subjects":
                {
                    Console.WriteLine("Displaying all subjects");
                    var subjects = Utils.GetIndexedSubjects(subjectService.GetAllSubjects());
                    foreach (var subject in subjects)
                    {
                        Console.WriteLine(subject.Key);
                        Console.WriteLine(subject.Value);
                        Console.WriteLine();
                    }
                    while (true)
                    {
                        Console.WriteLine("""
                        Enter index to display full info about single selected subject
                        Enter 'back' to go back to main menu
                        """);
                        Console.Write("Enter input: ");
                        var inputIndex = Console.ReadLine()?.Trim() ?? string.Empty;
                        if (inputIndex == "back")
                            break;
                        try
                        {
                            int index = int.Parse(inputIndex);
                            if (index < 1 || index > subjectService.GetAllSubjects().Count)
                                throw new Exception();
                            Console.WriteLine("Displaying selected subject");
                            Console.WriteLine(subjects[index].allData);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Invalid input");;
                        }
                    }
                    break;
                }
                case "lessons":
                {
                    Console.WriteLine("Displaying all lessons");
                    Console.WriteLine(string.Join("\n", lessonService.GetAllLessons()));
                    break;
                }
                default:
                {
                    Console.WriteLine("Invalid input");
                    break;
                }
            }
        }
    }


}