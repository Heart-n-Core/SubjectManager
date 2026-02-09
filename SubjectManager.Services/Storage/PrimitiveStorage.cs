using SubjectManager.Model.Entity;
using SubjectManager.Model.Enum;

namespace Services.Storage;

using SubjectManager.Model;

internal static class PrimitiveStorage
{
    private static readonly List<SubjectEntity> _subjects;
    private static readonly List<LessonEntity> _lessons;
    internal static List<SubjectEntity> Subjects => _subjects;
    internal static List<LessonEntity> Lessons => _lessons;

    static PrimitiveStorage()
    {
        _subjects = [];
        _lessons = [];
        populateDemoData();
    }

    private static void populateDemoData()
    {
        var s1 = new SubjectEntity("Об'єктно-орієнтоване програмування", 5, FieldOfKnowledge.ComputerSciences);
        _subjects.Add(s1);
        _lessons.AddRange(new LessonEntity[]
        {
            new(s1.Id, "Вступ до ООП", LessonType.Lection, new DateTimeOffset(2026, 02, 10, 8, 30, 0, TimeSpan.Zero), new DateTimeOffset(2026, 02, 10, 9, 50, 0, TimeSpan.Zero)),
            new(s1.Id, "Класи та об'єкти", LessonType.Lection, new DateTimeOffset(2026, 02, 10, 10, 00, 0, TimeSpan.Zero), new DateTimeOffset(2026, 02, 10, 11, 20, 0, TimeSpan.Zero)),
            new(s1.Id, "Лабораторна: Створення класів", LessonType.Laboratory, new DateTimeOffset(2026, 02, 11, 11, 30, 0, TimeSpan.Zero), new DateTimeOffset(2026, 02, 11, 12, 50, 0, TimeSpan.Zero)),
            new(s1.Id, "Інкапсуляція", LessonType.Lection, new DateTimeOffset(2026, 02, 12, 8, 30, 0, TimeSpan.Zero), new DateTimeOffset(2026, 02, 12, 9, 50, 0, TimeSpan.Zero)),
            new(s1.Id, "Наслідування: теорія", LessonType.Lection, new DateTimeOffset(2026, 02, 12, 10, 00, 0, TimeSpan.Zero), new DateTimeOffset(2026, 02, 12, 11, 20, 0, TimeSpan.Zero)),
            new(s1.Id, "Практика: Наслідування", LessonType.Practice, new DateTimeOffset(2026, 02, 13, 08, 30, 0, TimeSpan.Zero), new DateTimeOffset(2026, 02, 13, 09, 50, 0, TimeSpan.Zero)),
            new(s1.Id, "Поліморфізм", LessonType.Lection, new DateTimeOffset(2026, 02, 13, 10, 00, 0, TimeSpan.Zero), new DateTimeOffset(2026, 02, 13, 11, 20, 0, TimeSpan.Zero)),
            new(s1.Id, "Інтерфейси", LessonType.Lection, new DateTimeOffset(2026, 02, 14, 08, 30, 0, TimeSpan.Zero), new DateTimeOffset(2026, 02, 14, 09, 50, 0, TimeSpan.Zero)),
            new(s1.Id, "Лабораторна: Інтерфейси", LessonType.Laboratory, new DateTimeOffset(2026, 02, 14, 10, 00, 0, TimeSpan.Zero), new DateTimeOffset(2026, 02, 14, 11, 20, 0, TimeSpan.Zero)),
            new(s1.Id, "Фінальний семінар", LessonType.Seminar, new DateTimeOffset(2026, 02, 15, 12, 00, 0, TimeSpan.Zero), new DateTimeOffset(2026, 02, 15, 13, 20, 0, TimeSpan.Zero))
        });

        var s2 = new SubjectEntity("Вища математика", 4, FieldOfKnowledge.Mathematics);
        _subjects.Add(s2);
        _lessons.AddRange(new LessonEntity[]
        {
            new(s2.Id, "Лінійна алгебра", LessonType.Lection, new DateTimeOffset(2026, 02, 16, 09, 00, 0, TimeSpan.Zero), new DateTimeOffset(2026, 02, 16, 10, 20, 0, TimeSpan.Zero)),
            new(s2.Id, "Практика: Матриці", LessonType.Practice, new DateTimeOffset(2026, 02, 16, 10, 30, 0, TimeSpan.Zero), new DateTimeOffset(2026, 02, 16, 11, 50, 0, TimeSpan.Zero))
        });

        var s3 = new SubjectEntity("Психологія спілкування", 2, FieldOfKnowledge.SocialSciences);
        _subjects.Add(s3);
    }
}