using System.Text.Json;
using SubjectManager.CommonComponents.Enum;
using SubjectManager.Model.Entity;

namespace Storage;

using Microsoft.Maui.Storage;

public class FileStorage : IStorage
{
         private static readonly string FileStoragePath = Path.Combine(FileSystem.AppDataDirectory, "BestPossibleStorageForSubjectManager");

        private async Task Init()
        {
            if (!Directory.Exists(FileStoragePath)||Directory.GetFiles(FileStoragePath).Length == 0)
                await PopulateDemoData();
        }

        private async Task PopulateDemoData()
        {
            
            Directory.CreateDirectory(FileStoragePath);
            
            var tasks = new List<Task>();
            var _lessons = new List<LessonEntity>();
            var _subjects = new List<SubjectEntity>();
            
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
            
            foreach (var subject in _subjects)
            {
                Directory.CreateDirectory(Path.Combine(FileStoragePath, subject.Id.ToString()));
                tasks.Add(File.WriteAllTextAsync(SubjectFilePath(subject.Id), JsonSerializer.Serialize(subject)));

            }
            foreach (var lesson in _lessons)
            {
                tasks.Add(File.WriteAllTextAsync(LessonFilePath(lesson.SubjectId, lesson.Id), JsonSerializer.Serialize(lesson)));
            }
            
            await Task.WhenAll(tasks);
        }

        private string SubjectFilePath(Guid subjectId)
        {
            return Path.Combine(FileStoragePath, subjectId.ToString()+".json");
        }
        private string SubjectDirectoryPath(Guid subjectId)
        {
            return Path.Combine(FileStoragePath, subjectId.ToString());
        }
        private string LessonFilePath(Guid subjectId, Guid lessonId)
        {
            return LessonFilePath(SubjectDirectoryPath(subjectId), lessonId);
        }
        private string LessonFilePath(string subjectFolderPath, Guid lessonId)
        {
            return Path.Combine(subjectFolderPath, lessonId.ToString() + ".json");
        }

        public async Task<SubjectEntity> GetSubjectAsync(Guid subjectId)
        {
            await Init();
            var filePath = SubjectFilePath(subjectId);
            if (!File.Exists(filePath))
                return null;
            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<SubjectEntity>(json);
        }
        
public async Task SaveSubjectAsync(SubjectEntity subject)
{
    await Init();
    var filePath = SubjectFilePath(subject.Id);
    var subjectDir = SubjectDirectoryPath(subject.Id);
    if (!Directory.Exists(subjectDir))
        Directory.CreateDirectory(subjectDir);
    var json = JsonSerializer.Serialize(subject);
    await File.WriteAllTextAsync(filePath, json);
}

public async Task DeleteSubjectAsync(Guid subjectId)
{
    await Init();

    // Delete the subject file
    var subjectFile = SubjectFilePath(subjectId);
    if (File.Exists(subjectFile))
        File.Delete(subjectFile);

    // Delete the subject folder and all its lessons
    var subjectDir = SubjectDirectoryPath(subjectId);
    if (Directory.Exists(subjectDir))
        Directory.Delete(subjectDir, true); // recursive delete
}

        public async IAsyncEnumerable<SubjectEntity> GetSubjectsAsync()
        {
            await Init();
            foreach (var file in Directory.GetFiles(FileStoragePath, "*.json"))
            {
                var json = await File.ReadAllTextAsync(file);
                yield return JsonSerializer.Deserialize<SubjectEntity>(json);
            }
        }

        public async Task<LessonEntity> GetLessonAsync(Guid lessonId)
        {
            await Init();
            foreach (var directory in Directory.GetDirectories(FileStoragePath))
            {
                var filePath = LessonFilePath(directory, lessonId);
                if (!File.Exists(filePath))
                    continue;
                var json = await File.ReadAllTextAsync(filePath);
                return JsonSerializer.Deserialize<LessonEntity>(json);
            }
            return null;
        }

        public async Task<IEnumerable<LessonEntity>> GetLessonsBySubjectAsync(Guid subjectId)
        {
            await Init();
            var lessons = new List<LessonEntity>();
            var subjectDirectory = SubjectDirectoryPath(subjectId);
            if (!Directory.Exists(subjectDirectory))
                return lessons;
            foreach (var file in Directory.GetFiles(subjectDirectory,"*.json"))
            {
                var json = await File.ReadAllTextAsync(file);
                lessons.Add(JsonSerializer.Deserialize<LessonEntity>(json));
            }
            return lessons;
        }

        public async Task SaveLessonAsync(LessonEntity lesson)
        {
            await Init();
            var subjectDirectory = SubjectDirectoryPath(lesson.SubjectId);
            if (!Directory.Exists(subjectDirectory))
                Directory.CreateDirectory(subjectDirectory);
            var filePath = LessonFilePath(subjectDirectory, lesson.Id);
            await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(lesson));
        }

        public async Task DeleteLessonAsync(Guid lessonId)
        {
            await Init();
            foreach (var directory in Directory.GetDirectories(FileStoragePath))
            {
                var filePath = LessonFilePath(directory, lessonId);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return;
                }
            }
        }
}