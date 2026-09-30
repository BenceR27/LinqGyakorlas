using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LinqGyakorlo
{
    class Program
    {
        static void Main(string[] args)
        {
            // A feladatok leírását a Feladatlap.md fájlban találod.
            // Minden feladathoz tartozik egy Feladat##() metódus itt lent.
            // Írd meg a LINQ lekérdezést a metódus törzsében, majd
            // vedd ki a kommentet a hívása elől, hogy lásd az eredményt.
            Feladat01();
            Feladat02();
            Feladat03();
            Feladat04();
            Feladat05();
            Feladat06();
            Feladat07();
            Feladat08();
            Feladat09();
            Feladat10();
            Feladat11();
            Feladat12();
            Feladat13();
            Feladat14();
            Feladat15();
            Feladat16();
            Feladat17();
            Feladat18();
            Feladat19();
            Feladat20();
            Feladat21();
            Feladat22();
            Feladat23();
            Feladat24();
            Feladat25();
            Feladat26();
            Feladat27();
            Feladat28();
            Feladat29();
            // Feladat30();
            // Feladat31();
            // Feladat32();
            // Feladat33();
            // Feladat34();
            // Feladat35();
            // Feladat36();
            // Feladat37();
            // Feladat38();
            // Feladat39();
            // Feladat40();
        }

        // ---------- 1. Szűrés — Where ----------

        // 1. Hallgatók, akiknek 4.0 fölötti az átlaga.
        static void Feladat01()
        {
            var result = SampleData.Students.Where(s => s.GradeAverage > 4);
            foreach (var student in result)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine("\n");
        }

        // 2. Budapesti hallgatók.
        static void Feladat02()
        {
            var result = SampleData.Students.Where(s => s.City == "Budapest");
            foreach (var student in result)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine("\n");
        }

        // 3. Kurzusok, amelyek kreditértéke legalább 5.
        static void Feladat03()
        {
            var result = SampleData.Courses.Where(c => c.Credit >= 5);
            foreach (var course in result)
            {
                Console.WriteLine(course);
            }
            Console.WriteLine("\n");
        }

        // 4. Hallgatók 20-23 év között (határokkal), akik nem budapestiek.
        static void Feladat04()
        {
            var result = SampleData.Students.Where(s => s.Age >= 20 && s.Age <= 23 && s.City != "Budapest");
            foreach (var student in result)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine("\n");
        }

        // ---------- 2. Vetítés — Select, SelectMany ----------

        // 5. Csak a hallgatók nevei.
        static void Feladat05()
        {
            var result = SampleData.Students.Select(s => s.Name);
            foreach (var name in result)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine("\n");
        }

        // 6. Anonim típusú lista: Name, GradeAverage.
        static void Feladat06()
        {
            var result = SampleData.Students.Select(s => new { s.Name, s.GradeAverage }); 
            foreach (var item in result)
            {
                Console.WriteLine($"Name: {item.Name}, GradeAverage: {item.GradeAverage}");
            }
            Console.WriteLine("\n");
        }

        // 7. Kurzus neve + a kurzust tartó tanár neve (Select, Join nélkül).
        static void Feladat07()
        {
            var result = SampleData.Courses.Select(c => new { c.Name, c.TeacherId });
            foreach (var item in result)
            {
                var teacher = SampleData.Teachers.FirstOrDefault(t => t.Id == item.TeacherId);
                if (teacher != null)
                {
                    Console.WriteLine($"Course: {item.Name}, Teacher: {teacher.Name}");
                }
            }
            Console.WriteLine("\n");
        }

        // 8. SelectMany: beiratkozások lapos listája hallgató névvel.
        static void Feladat08()
        {
            var result = SampleData.Enrollments
                .SelectMany(e => SampleData.Students
                    .Where(s => s.Id == e.StudentId)
                    .Select(s => s.Name));
            foreach (var name in result)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine("\n");
        }

        // ---------- 3. Rendezés — OrderBy, ThenBy, Reverse ----------

        // 9. Hallgatók átlag szerint csökkenő sorrendben.
        static void Feladat09()
        {
            var result = SampleData.Students.OrderByDescending(s => s.GradeAverage);
            foreach (var student in result)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine("\n");
        }

        // 10. Hallgatók város szerint, majd név szerint növekvő sorrendben.
        static void Feladat10()
        {
            var result = SampleData.Students.OrderBy(s => s.City).ThenBy(s => s.Name);
            foreach (var student in result)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine("\n");
        }

        // 11. Kurzusok eredeti sorrendjének megfordítása (Reverse).
        static void Feladat11()
        {
            var result = SampleData.Courses.AsEnumerable().Reverse();
            foreach (var course in result)
            {
                Console.WriteLine(course);
            }
            Console.WriteLine("\n");
        }

        // ---------- 4. Csoportosítás — GroupBy ----------

        // 12. Hallgatók száma városonként.
        static void Feladat12()
        {
            var result = SampleData.Students
                .GroupBy(s => s.City)
                .Select(g => new { City = g.Key, Count = g.Count() });

            foreach (var item in result)
            {
                Console.WriteLine(item);
                }
            Console.WriteLine("\n");
        }

        // 13. Átlagos tanulmányi átlag városonként.
        static void Feladat13()
        {
            var result = SampleData.Students.GroupBy(s => s.City)
                .Select(g => new { City = g.Key, AverageGrade = g.Average(s => s.GradeAverage) });
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("\n");
        }

        // 14. Kurzusnevek kategóriánként.
        static void Feladat14()
        {
            var result = SampleData.Courses.GroupBy(c => c.Category)
                .Select(g => new {KurzusKategoria = g.Key, KurzusNevek = g.Select(c => c.Name).ToList() });
            foreach (var item in result)
            {
                Console.WriteLine($"Kategória: {item.KurzusKategoria}, Kurzusok: {string.Join(", ", item.KurzusNevek)}");
            }
            Console.WriteLine("\n");
        }

        // ---------- 5. Összekapcsolás — Join, GroupJoin ----------

        // 15. Enrollments + Students Join: hallgató neve minden beiratkozáshoz.
        static void Feladat15()
        {
           var result = SampleData.Enrollments.Join(
                SampleData.Students,
                e => e.StudentId,
                s => s.Id,
                (e, s) => new { Enrollment = e, StudentName = s.Name }
            );
            foreach (var item in result)
            {
                Console.WriteLine($"Hallgató neve: {item.StudentName}, Beiratkozás: {item.Enrollment}");
            }
            Console.WriteLine("\n");
        }

        // 16. Háromtáblás Join: hallgató neve, kurzus neve, érdemjegy.
        static void Feladat16()
        {
            var result = SampleData.Enrollments.Join(
                SampleData.Students,
                e => e.StudentId,
                s => s.Id,
                (e, s) => new { Enrollment = e, StudentName = s.Name }
            ).Join(
                SampleData.Courses,
                es => es.Enrollment.CourseId,
                c => c.Id,
                (es, c) => new { StudentName = es.StudentName, CourseName = c.Name, Grade = es.Enrollment.Grade }
            );
            foreach (var item in result)
            {
                Console.WriteLine($"Hallgató neve: {item.StudentName}, Kurzus: {item.CourseName}, Osztályzat: {item.Grade}");
            }
            Console.WriteLine("\n");
        }

        // 17. GroupJoin: hallgatónként a beiratkozásai (azok is, akiknek nincs).
        static void Feladat17()
        {
            var result = SampleData.Students.GroupJoin(
                SampleData.Enrollments,
                s => s.Id,
                e => e.StudentId,
                (s, enrollments) => new { StudentName = s.Name, Enrollments = enrollments }
            );
            foreach (var item in result)
            {
                Console.WriteLine($"Hallgató: {item.StudentName}, Beiratkozások: {string.Join(", ", item.Enrollments.Select(e => e.CourseId))}");
            }
            Console.WriteLine("\n");
        }

        // ---------- 6. Halmazműveletek — Distinct, Union, Intersect, Except, Concat, Zip ----------

        // 18. Hány különböző város van a hallgatók között (Distinct).
        static void Feladat18()
        {
            var result = SampleData.Students.Select(s => s.City).Distinct();
            Console.WriteLine($"Különböző városok száma: {result.Count()}");
            Console.WriteLine("\n");
        }
        // 19. Különböző kurzuskategóriák (Distinct).
        static void Feladat19()
        {
            var result = SampleData.Courses.Select(c => c.Category).Distinct();
            Console.WriteLine($"Különböző kurzuskategóriák száma: {result.Count()}");
            Console.WriteLine("\n");
        }

        // 20. Union, Intersect, Except a "kiváló" (átlag >= 4.5) és "budapesti" hallgatók nevei között.
        static void Feladat20()
        {
            var kivaloHallgatok = SampleData.Students.Where(s => s.GradeAverage >= 4.5).Select(s => s.Name);
            var budapestiHallgatok = SampleData.Students.Where(s => s.City == "Budapest").Select(s => s.Name);
             
        }

        // 21. Concat: Matematika + Informatika kurzusnevek.
        static void Feladat21()
        {
            var result = SampleData.Courses.Where(c => c.Category == "Matematika" || c.Category == "Informatika")
                .Select(c => c.Name);
            
            foreach (var courseName in result)
            {
                Console.WriteLine(courseName);
            }
            Console.WriteLine("\n");
        }

        // 22. Zip: első 4 hallgató neve + első 4 kurzus neve párban.
        static void Feladat22()
        {
            var hallgatok = SampleData.Students.Take(4).Select(s => s.Name);
            var kurzusok = SampleData.Courses.Take(4).Select(c => c.Name);
            var result = hallgatok.Zip(kurzusok, (h, k) =>
            new { Hallgato = h, Kurzus = k });
            foreach (var item in result)
            {
                Console.WriteLine($"Hallgató: {item.Hallgato}, Kurzus: {item.Kurzus}");
            }
            Console.WriteLine("\n");
        }

        // ---------- 7. Aggregálás — Count, Sum, Average, Min, Max, Aggregate ----------

        // 23. Hallgatók száma összesen, illetve akiknek átlaga > 4.0 (Count).
        static void Feladat23()
        {
            var totalCount = SampleData.Students.Count();
            var countAbove4 = SampleData.Students.Count(s => s.GradeAverage > 4.0);
            Console.WriteLine($"Összes hallgató: {totalCount}");
            Console.WriteLine($"Átlag > 4.0: {countAbove4}");
            Console.WriteLine("\n");
        }

        // 24. Az összes kurzus kredit-összege (Sum).
        static void Feladat24()
        {
            var totalCredits = SampleData.Courses.Sum(c => c.Credit);
            Console.WriteLine($"Összes kredit: {totalCredits}");
            Console.WriteLine("\n");
        }

        // 25. Hallgatók átlagéletkora (Average).
        static void Feladat25()
        {
            var averageAge = SampleData.Students.Average(s => s.Age);
            Console.WriteLine($"Átlagéletkor: {averageAge}");
            Console.WriteLine("\n");
        }

        // 26. Legfiatalabb és legidősebb hallgató életkora (Min, Max).
        static void Feladat26()
        {
            var minAge = SampleData.Students.Min(s => s.Age);
            var maxAge = SampleData.Students.Max(s => s.Age);
            Console.WriteLine($"Legfiatalabb hallgató életkora: {minAge}");
            Console.WriteLine($"Legidősebb hallgató életkora: {maxAge}");
            Console.WriteLine("\n");
        }

        // 27. Aggregate: hallgatónevek vesszővel elválasztva egy stringbe.
        static void Feladat27()
        {
            var studentNames = SampleData.Students.Select(s => s.Name);
            var result = studentNames.Aggregate((current, next) => current + ", " + next);
            Console.WriteLine($"Hallgatónevek: {result}");
            Console.WriteLine("\n");
        }

        // ---------- 8. Elemkiválasztás — First, Last, Single, ElementAt ----------

        // 28. Első szegedi hallgató (First/FirstOrDefault).
        static void Feladat28()
        {
            var firstSzegedi = SampleData.Students.FirstOrDefault(s => s.City == "Szeged");
            Console.WriteLine($"Első szegedi hallgató: {firstSzegedi?.Name ?? "Nincs ilyen hallgató"}");
            Console.WriteLine("\n");
        }

        // 29. Az egyetlen "Lakatos Kata" nevű hallgató (Single/SingleOrDefault),
        //     majd egy olyan eset kipróbálása try-catch-csel, ahol több találat van.
        static void Feladat29()
        {
            try
            {
                var lakatosKata = SampleData.Students.Single(s => s.Name == "Lakatos Kata");
                Console.WriteLine($"Lakatos Kata: {lakatosKata.Name}");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Nem egyedi a nevük vagy nincs ilyen hallgató.");
            }
            Console.WriteLine("\n");
        }

        // 30. A 3. indexű (0-tól) hallgató (ElementAt).
        static void Feladat30()
        {
            
        }

        // ---------- 9. Particionálás — Skip, Take, SkipWhile, TakeWhile, Chunk ----------

        // 31. TOP 3 hallgató átlag szerint (Take).
        static void Feladat31()
        {
            // TODO
        }

        // 32. Az első 3 utáni hallgatók (Skip).
        static void Feladat32()
        {
            // TODO
        }

        // 33. Életkor szerint rendezve: TakeWhile (21 évnél fiatalabbak), majd SkipWhile (a többi).
        static void Feladat33()
        {
            // TODO
        }

        // 34. Hallgatók felbontása 4 fős csoportokra (Chunk).
        static void Feladat34()
        {
            // TODO
        }

        // ---------- 10. Egyéb — Any, All, Contains, ToDictionary, ToHashSet, DefaultIfEmpty ----------

        // 35. Van-e hallgató 2.5 alatti átlaggal (Any).
        static void Feladat35()
        {
            // TODO
        }

        // 36. Minden hallgató 18 évesnél idősebb-e (All).
        static void Feladat36()
        {
            // TODO
        }

        // 37. Szerepel-e "Pécs" a városok között (Contains).
        static void Feladat37()
        {
            // TODO
        }

        // 38. Dictionary<int, string> a hallgatók Id-je és neve alapján (ToDictionary).
        static void Feladat38()
        {
            // TODO
        }

        // 39. HashSet<string> a kurzuskategóriákból (ToHashSet).
        static void Feladat39()
        {
            // TODO
        }

        // 40. Nem létező kurzushoz tartozó beiratkozások, DefaultIfEmpty kezeléssel.
        static void Feladat40()
        {
            // TODO
        }
    }
}
