using System;
using System.Collections.Generic;
using Bogus;
using Microsoft.EntityFrameworkCore;
namespace Models
{
    public static class DataSeeding
    {
        public static string lang = "fr";

        public static ModelBuilder Videos(this ModelBuilder modelBuilder)
        {
            int id = 1;
            var list = new[] {
                "جديد تعلم اللغه الانجليزيه عن طريق التواصل للمبتدئين ",
                "EP13 حوارات لتعلم اللغة الفرنسية للمبتدئين : الذهاب إلى المحكمة",
                "EP12  حوارات لتعلم اللغة الفرنسية للمبتدئين : حجز رحلة طيران",
                "12 تعلم الإنجليزية من الصفر مع أنفو أكاديمي أسماء الحاجيات الشخصية  الحلقة",
                "11تعلم الإنجليزية من الصفر مع أنفو أكاديمي أسماء الملابس الحلقة",
                "EP 11 حوارات لتعلم اللغة الفرنسية للمبتدئين : الوجبات السريعة و بعض الصفات",
                "جديد تعلم اللغه الانجليزيه عن طريق التواصل للمبتدئين ",
                "EP13 حوارات لتعلم اللغة الفرنسية للمبتدئين : الذهاب إلى المحكمة",
                "EP12  حوارات لتعلم اللغة الفرنسية للمبتدئين : حجز رحلة طيران",
                "12 تعلم الإنجليزية من الصفر مع أنفو أكاديمي أسماء الحاجيات الشخصية  الحلقة",
                "11تعلم الإنجليزية من الصفر مع أنفو أكاديمي أسماء الملابس الحلقة",
                "EP 11 حوارات لتعلم اللغة الفرنسية للمبتدئين : الوجبات السريعة و بعض الصفات",
            };

            var videos = new[] {
                "https://www.youtube.com/watch?v=5GI5Xma6WHA",
                "https://www.youtube.com/watch?v=Pqe3kKoG_Ao",
                "https://www.youtube.com/watch?v=vNjbwk4S-LI",
                "https://www.youtube.com/watch?v=t7Rsnoxitnw",
                "https://www.youtube.com/watch?v=98wpFWaGceg",
                "https://www.youtube.com/watch?v=d_vUZEByM2o",
                "https://www.youtube.com/watch?v=5GI5Xma6WHA",
                "https://www.youtube.com/watch?v=Pqe3kKoG_Ao",
                "https://www.youtube.com/watch?v=vNjbwk4S-LI",
                "https://www.youtube.com/watch?v=t7Rsnoxitnw",
                "https://www.youtube.com/watch?v=98wpFWaGceg",
                "https://www.youtube.com/watch?v=d_vUZEByM2o",
            };

            var faker = new Faker<Video>(DataSeeding.lang)
                .CustomInstantiator(f => new Video { Id = id++ })
                .RuleFor(o => o.Title, f => list[id - 2])
                .RuleFor(o => o.Order, f => f.Random.Number(1, 10))
                .RuleFor(o => o.Description, f => f.Lorem.Word())
                .RuleFor(o => o.Date, f => f.Date.Past())
                .RuleFor(o => o.UrlVideo, f => videos[id - 2])
                ;
            modelBuilder.Entity<Video>().HasData(faker.Generate(10));
            return modelBuilder;
        }

        public static ModelBuilder LieuCourses(this ModelBuilder modelBuilder)
        {
            int id = 1;
            var l = new[] { "Donner des cours à distance", "Aller au domicile de l'élève", "Accueillez l'étudiant chez moi" };
            var l2 = new[] { "إعطاء دروس عن بعد", "الذهاب لمنزل التلميذ", "استقبال التلميذ في منزلي" };
            var faker = new Faker<LieuCours>(DataSeeding.lang)
                .CustomInstantiator(f => new LieuCours { Id = id++ })
                .RuleFor(o => o.Nom, f => l[id - 2])
                .RuleFor(o => o.NomAr, f => l2[id - 2])
                ;
            modelBuilder.Entity<LieuCours>().HasData(faker.Generate(3));
            return modelBuilder;
        }

        public static ModelBuilder TypeCourses(this ModelBuilder modelBuilder)
        {
            int id = 1;
            var list = new[] {
                "Cours a domicile individuel",
                "Cours a domicile groupe",
                "Cours en ligne individuel",
                "Cours en ligne groupe",
            };
            var listAr = new[] {
                "دروس منزلية فردية",
                "دروس جماعية في المنزل",
                "دورة فردية عبر الإنترنت" ,
                "دورة جماعية عبر الإنترنت" ,
            };
            var faker = new Faker<TypeCours>(DataSeeding.lang)
                .CustomInstantiator(f => new TypeCours { Id = id++ })
                .RuleFor(o => o.Nom, f => list[id - 2])
                .RuleFor(o => o.NomAr, f => listAr[id - 2])
                ;
            modelBuilder.Entity<TypeCours>().HasData(faker.Generate(4));
            return modelBuilder;
        }

        public static ModelBuilder TypeActivites(this ModelBuilder modelBuilder)
        {
            int id = 1;
            var list = new[] { "Soutien scolaire", "Langues", "Activites paralelle" };
            var listAr = new[] { "الدفاع المدرسي", "لغة", "الأنشطة الموازية" };
            var isActive = new[] { true, false, false };

            var faker = new Faker<TypeActivite>(DataSeeding.lang)
                .CustomInstantiator(f => new TypeActivite { Id = id++ })
                .RuleFor(o => o.Nom, f => list[id - 2])
                .RuleFor(o => o.NomAr, f => listAr[id - 2])
                .RuleFor(o => o.ImageUrl, f => "")
                .RuleFor(o => o.Active, f => isActive[id - 2])
                ;

            modelBuilder.Entity<TypeActivite>().HasData(faker.Generate(3));
            return modelBuilder;
        }

        public static ModelBuilder Villes(this ModelBuilder modelBuilder)
        {
            int id = 1;
            var faker = new Faker<Ville>(DataSeeding.lang)
                .CustomInstantiator(f => new Ville { Id = id++ })
.RuleFor(o => o.Nom, f => f.Lorem.Word())
.RuleFor(o => o.NomAr, f => f.Lorem.Word())
;
            modelBuilder.Entity<Ville>().HasData(faker.Generate(10));
            return modelBuilder;
        }

        public static ModelBuilder Users(this ModelBuilder modelBuilder)
        {
            int id = 1;
            var faker = new Faker<User>(DataSeeding.lang)
                .CustomInstantiator(f => new User { Id = id++ })
                .RuleFor(o => o.Nom, f => f.Name.FirstName())
                .RuleFor(o => o.Prenom, f => f.Name.LastName())
                .RuleFor(o => o.Tel1, f => f.Phone.PhoneNumber())
                .RuleFor(o => o.Tel2, f => f.Phone.PhoneNumber())
                .RuleFor(o => o.Email, f => id - 1 == 1 ? "prof@angular.io" : (id - 1 == 51 ? "student@angular.io" : f.Internet.Email()))
                .RuleFor(o => o.Password, f => "123")
                .RuleFor(o => o.IsActive, f => id - 1 == 1 ? true : f.Random.Bool())
                .RuleFor(o => o.Date, f => f.Date.Past())
                .RuleFor(o => o.Adresse, f => f.Address.FullAddress())
                .RuleFor(o => o.ImageUrl, f => "assets/prof.jpg")
                .RuleFor(o => o.Cin, f => f.Lorem.Word())
                .RuleFor(o => o.Role, f => id - 1 <= 50 ? "prof" : "student")
                .RuleFor(o => o.IdVille, f => f.Random.Number(1, 3))
                ;
            modelBuilder.Entity<User>().HasData(faker.Generate(100));
            return modelBuilder;
        }

        public static ModelBuilder Discussions(this ModelBuilder modelBuilder)
        {
            int id = 1;
            var faker = new Faker<Discussion>(DataSeeding.lang)
                .CustomInstantiator(f => new Discussion { Id = id++ })
.RuleFor(o => o.UnReaded, f => f.Random.Number(1, 10))
.RuleFor(o => o.Date, f => f.Date.Past())
.RuleFor(o => o.IdMe, f => f.Random.Number(1, 10))
.RuleFor(o => o.IdOtherUser, f => f.Random.Number(1, 10))
;
            modelBuilder.Entity<Discussion>().HasData(faker.Generate(10));
            return modelBuilder;
        }

        public static ModelBuilder Messages(this ModelBuilder modelBuilder)
        {
            int id = 1;
            var i = 1;
            var faker = new Faker<Message>(DataSeeding.lang)
                .CustomInstantiator(f => new Message { Id = id++ })
                .RuleFor(o => o.Object, f => f.Lorem.Word())
                .RuleFor(o => o.Content, f => f.Lorem.Word())
                .RuleFor(o => o.Vu, f => id - 1 == 1 ? true : f.Random.Bool())
                .RuleFor(o => o.Date, f => f.Date.Past())
                .RuleFor(o => o.IdCours, f => f.Random.Number(1, 10))
                .RuleFor(o => o.OtherUserName, f => f.Lorem.Word())
                .RuleFor(o => o.OtherUserImage, f => "")
                .RuleFor(o => o.IdMe, f => 1)
                .RuleFor(o => o.IdOtherUser, f =>
                {
                    i++;
                    if (i + 51 <= 61)
                    {
                        return i + 51;
                    }
                    else
                    {
                        i = 1;
                        return i + 51;
                    }
                })
                .RuleFor(o => o.IdDiscussion, f => i)
                ;
            modelBuilder.Entity<Message>().HasData(faker.Generate(10));
            return modelBuilder;
        }

        public static ModelBuilder EventProfs(this ModelBuilder modelBuilder)
        {
            int id = 1;
            var faker = new Faker<EventProf>(DataSeeding.lang)
                .CustomInstantiator(f => new EventProf { Id = id++ })
                .RuleFor(o => o.Title, f => f.Lorem.Word())
                .RuleFor(o => o.Start, f => f.Date.Past())
                .RuleFor(o => o.End, f => f.Date.Past())
                .RuleFor(o => o.Color, f => "#ad2121_#FAE3E3")
                .RuleFor(o => o.Draggable, f => id - 1 == 1 ? true : f.Random.Bool())
                .RuleFor(o => o.Resizable, f => "1_1")
                .RuleFor(o => o.Month, f => f.Random.Number(1, 12))
                .RuleFor(o => o.Year, f => f.Random.Number(2021, 2022))
                .RuleFor(o => o.IdUser, f => f.Random.Number(1, 10))
                ;
            modelBuilder.Entity<EventProf>().HasData(faker.Generate(10));
            return modelBuilder;
        }

        public static ModelBuilder DetailUserActivites(this ModelBuilder modelBuilder)
        {
            int id = 1;
            var faker = new Faker<DetailUserActivite>(DataSeeding.lang)
                .CustomInstantiator(f => new DetailUserActivite { Id = id++ })
                .RuleFor(o => o.Date, f => f.Date.Past())
                .RuleFor(o => o.IdUser, f => f.Random.Number(1, 10))
                .RuleFor(o => o.IdActivite, f => f.Random.Number(1, 9))
                ;
            modelBuilder.Entity<DetailUserActivite>().HasData(faker.Generate(10));
            return modelBuilder;
        }

        public static ModelBuilder Activites(this ModelBuilder modelBuilder)
        {

            var list = new Activite[] {
                new Activite {Id = 1, Nom = "Math", NomAr = "رياضيات", ImageUrl = "", IdTypeActivite = 1},
                new Activite {Id = 2, Nom = "Arab", NomAr = "عرب", ImageUrl = "", IdTypeActivite = 1},
                new Activite {Id = 3, Nom = "Français", NomAr = "فرنسي", ImageUrl = "", IdTypeActivite = 1},
                new Activite {Id = 4, Nom = "Arab", NomAr = "عرب", ImageUrl = "", IdTypeActivite = 2},
                new Activite {Id = 5, Nom = "Français", NomAr = "فرنسي", ImageUrl = "", IdTypeActivite = 2},
                new Activite {Id = 6, Nom = "Anglais", NomAr = "الإنجليزية", ImageUrl = "", IdTypeActivite = 2},
                new Activite {Id = 7, Nom = "dessin", NomAr = "رسم", ImageUrl = "", IdTypeActivite = 3},
                new Activite {Id = 8, Nom = "calcule", NomAr = "محسوب", ImageUrl = "", IdTypeActivite = 3},
                new Activite {Id = 9, Nom = "music", NomAr = "موسيقى", ImageUrl = "", IdTypeActivite = 3},
            };
            modelBuilder.Entity<Activite>().HasData(list);
            return modelBuilder;
        }

        public static ModelBuilder Profs(this ModelBuilder modelBuilder)
        {
            int id = 1;
            var faker = new Faker<Prof>(DataSeeding.lang)
                .CustomInstantiator(f => new Prof { Id = id++ })
                .RuleFor(o => o.Lien, f => "https://meet.google.com/iib-cxdx-joe")
                .RuleFor(o => o.Description, f => f.Lorem.Paragraph())
                .RuleFor(o => o.Experience, f => f.Lorem.Paragraph())
                .RuleFor(o => o.Approche, f => f.Lorem.Paragraph())
                .RuleFor(o => o.Intro, f => f.Lorem.Paragraph())
                .RuleFor(o => o.VideoUrl, f => "")
                .RuleFor(o => o.CvUrl, f => "")
                .RuleFor(o => o.Note, f => f.Random.Number(100, 1000))
                .RuleFor(o => o.PrixHrWeb, f => f.Random.Number(20, 100))
                .RuleFor(o => o.PrixHrHome, f => f.Random.Number(20, 100))
                .RuleFor(o => o.PrixHrWebGroupe, f => f.Random.Number(20, 100))
                .RuleFor(o => o.PrixHrHomeGroupe, f => f.Random.Number(20, 100))
                .RuleFor(o => o.IdsTypeActivites, f => f.PickRandom(new[] { ";1;", ";2;", ";3;" }))
                .RuleFor(o => o.IdsActivites, f => f.PickRandom(new[] { ";1;", ";2;", ";3;", ";4;", ";5;", ";6;", ";7;", ";8;", ";9;" }))
                .RuleFor(o => o.IdsTypeCours, f => f.PickRandom(new[] { ";1;", ";2;" }))
                .RuleFor(o => o.IdsLieuCours, f => f.PickRandom(new[] { ";1;", ";2;", ";3;" }))
                .RuleFor(o => o.IdsNiveauScolaires, f => f.PickRandom(new[] { ";1;", ";2;", ";3;", ";4;", ";5;", ";6;", ";7;", ";8;", ";9;" }))
                .RuleFor(o => o.IdUser, f => id - 1)
                ;
            modelBuilder.Entity<Prof>().HasData(faker.Generate(50));
            return modelBuilder;
        }

        public static ModelBuilder Students(this ModelBuilder modelBuilder)
        {
            int id = 1;
            var faker = new Faker<Student>(DataSeeding.lang)
                .CustomInstantiator(f => new Student { Id = id++ })
                .RuleFor(o => o.Ecole, f => f.Lorem.Word())
                .RuleFor(o => o.Niveau, f => f.Random.Number(1, 10))
                .RuleFor(o => o.Branche, f => f.Random.Number(1, 10))
                .RuleFor(o => o.NomParent, f => f.Name.FirstName())
                .RuleFor(o => o.PrenomParent, f => f.Name.LastName())
                .RuleFor(o => o.Tel1Parent, f => f.Phone.PhoneNumber())
                .RuleFor(o => o.Tel2Parent, f => f.Phone.PhoneNumber())
                .RuleFor(o => o.IdUser, f => (id - 1) + 50)
                .RuleFor(o => o.IdActivite, f => f.Random.Number(1, 9))
                ;
            modelBuilder.Entity<Student>().HasData(faker.Generate(50));
            return modelBuilder;
        }

        public static ModelBuilder NiveauScolaires(this ModelBuilder modelBuilder)
        {
            int id = 1;
            var list = new[] {
                "1ére Primaire",
                "2éme Primaire",
                "3éme Primaire",
                "4éme Primaire",
                "5éme Primaire",
                "6éme Primaire",
                "1ére Collège",
                "2éme Collège",
                "3éme Collège",
                "Tronc commun",
                "1ére Bac",
                "2éme Bac",
            };

            var listAr = new[] {
                "الابتدائية",
                "الثاني الابتدائي",
                "الثالثة الابتدائية",
                "الرابع الابتدائي",
                "الخامسة الابتدائية",
                "6 الابتدائية",
                "1st كلية",
                "2 الكلية",
                "الكلية الثالثة",
                "جذع مشترك",
                "1st البكالوريا",
                "2nd البكالوريا",
            };

            var values = new double[,]
            {
                {150, 127.5, 120, 105},
                {150, 127.5, 120, 105},
                {150, 127.5, 120, 105},
                {150, 127.5, 120, 105},
                {150, 127.5, 120, 105},

                {180, 150, 142.5, 120},

                {195, 172.5, 157.5, 142.5},
                {195, 172.5, 157.5, 142.5},
                {210, 187.5, 165, 150},

                {225, 202.5, 180, 165},
                {240, 217.5, 195, 180},
                {255, 232.5, 210, 187.5},
            };

            var faker = new Faker<NiveauScolaire>(DataSeeding.lang)
                .CustomInstantiator(f => new NiveauScolaire { Id = id++ })
                .RuleFor(o => o.Nom, f => list[id - 2])
                .RuleFor(o => o.NomAr, f => listAr[id - 2])
                .RuleFor(o => o.IdCycle, f => (id - 2 <= 6) ? 1 : (id <= 9 ? 2 : 3))
                .RuleFor(o => o.CoursLigneGroupe, f => values[id - 2, 0])
                .RuleFor(o => o.CoursLigneIndividuel, f => values[id - 2, 1])
                .RuleFor(o => o.CoursDomicileGroupe, f => values[id - 2, 2])
                .RuleFor(o => o.CoursDomicileIndividuel, f => values[id - 2, 3])
                ;
            modelBuilder.Entity<NiveauScolaire>().HasData(faker.Generate(12));
            return modelBuilder;
        }

        public static ModelBuilder Branches(this ModelBuilder modelBuilder)
        {
            int id = 1;

            var list = new Branche[] {
                new Branche {Id = id++, Nom = "Tronc Commun", NomAr = "جذع مشترك", IdNiveauScolaire = 10},
                new Branche {Id = id++, Nom = "Sciences", NomAr = "علم", IdNiveauScolaire = 10},
                new Branche {Id = id++, Nom = "Technologies", NomAr = "التقنيات", IdNiveauScolaire = 10},
                new Branche {Id = id++, Nom = "Lettres et Sciences Humaines", NomAr = "الآداب والعلوم الإنسانية", IdNiveauScolaire = 10},
                new Branche {Id = id++, Nom = "Sciences Mathématiques", NomAr = "العلوم الرياضية", IdNiveauScolaire = 11},
                new Branche {Id = id++, Nom = "Sciences Expérimentales", NomAr = "علوم تجريبية", IdNiveauScolaire = 11},
                new Branche {Id = id++, Nom = "Sciences et Technologies Électriques", NomAr = "العلوم والتقنيات الكهربائية", IdNiveauScolaire = 11},
                new Branche {Id = id++, Nom = "Sciences et Technologies Mécaniques", NomAr = "العلوم الميكانيكية والتقنيات", IdNiveauScolaire = 11},
                new Branche {Id = id++, Nom = "Sciences Économiques et Gestion", NomAr = "الاقتصاد والإدارة", IdNiveauScolaire = 11},
                new Branche {Id = id++, Nom = "Lettres et Sciences Humaines", NomAr = "الآداب والعلوم الإنسانية", IdNiveauScolaire = 11},
                new Branche {Id = id++, Nom = "Sciences Mathématiques A", NomAr = "العلوم الرياضية أ", IdNiveauScolaire = 12},
                new Branche {Id = id++, Nom = "Sciences Mathématiques B", NomAr = "العلوم الرياضية ب", IdNiveauScolaire = 12},
                new Branche {Id = id++, Nom = "Sciences Physiques", NomAr = "علوم فيزيائية", IdNiveauScolaire = 12},
                new Branche {Id = id++, Nom = "Sciences de la Vie et de la Terre (SVT)", NomAr = "علوم الأرض والحياة (SVT)", IdNiveauScolaire = 12},
                new Branche {Id = id++, Nom = "Sciences Agronomiques", NomAr = "العلوم الزراعية", IdNiveauScolaire = 12},
                new Branche {Id = id++, Nom = "Sciences et Technologies Électriques", NomAr = "العلوم والتقنيات الكهربائية", IdNiveauScolaire = 12},
                new Branche {Id = id++, Nom = "Sciences et Technologies Mécaniques", NomAr = "العلوم الميكانيكية والتقنيات", IdNiveauScolaire = 12},
                new Branche {Id = id++, Nom = "Sciences Économiques", NomAr = "علوم إقتصادية", IdNiveauScolaire = 12},
                new Branche {Id = id++, Nom = "Sciences de Gestion Comptable (SGC)", NomAr = "علوم إدارة المحاسبة (SGC)", IdNiveauScolaire = 12},
                new Branche {Id = id++, Nom = "Lettres", NomAr = "حروف", IdNiveauScolaire = 12},
                new Branche {Id = id++, Nom = "Sciences Humaines", NomAr = "علوم اجتماعية", IdNiveauScolaire = 12},
            };
            modelBuilder.Entity<Branche>().HasData(list);
            return modelBuilder;
        }

        public static ModelBuilder Matiers(this ModelBuilder modelBuilder)
        {
            int id = 1;
            var faker = new Faker<Matier>(DataSeeding.lang)
                .CustomInstantiator(f => new Matier { Id = id++ })
            .RuleFor(o => o.Name, f => f.Lorem.Word())
            .RuleFor(o => o.NameAr, f => f.Lorem.Word())
            ;
            modelBuilder.Entity<Matier>().HasData(faker.Generate(10));
            return modelBuilder;
        }

        public static ModelBuilder Courses(this ModelBuilder modelBuilder)
        {
            int id = 1;
            var list = new Cours[] {
                 new Cours {Id = id++, IdBranche = 1, Nom = "Mathématiques", NomAr = "الرياضيات"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 1 , IdBranche = null, Nom = "Activité scientifique", NomAr = "النشاط العلمي"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 1 , IdBranche = null, Nom = "Arabe", NomAr = "العربية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 1 , IdBranche = null, Nom = "Français", NomAr = "الفرنسية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 1 , IdBranche = null, Nom = "Education Islamique", NomAr = "تربية إسلامية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 1 , IdBranche = null, Nom = "Éducation artistique", NomAr = "التربية الفنية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 2 , IdBranche = null, Nom = "Mathématiques", NomAr = "الرياضيات"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 2 , IdBranche = null, Nom = "Activité scientifique", NomAr = "النشاط العلمي"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 2 , IdBranche = null, Nom = "Arabe", NomAr = "العربية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 2 , IdBranche = null, Nom = "Français", NomAr = "الفرنسية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 2 , IdBranche = null, Nom = "Education Islamique", NomAr = "تربية إسلامية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 2 , IdBranche = null, Nom = "Éducation artistique", NomAr = "التربية الفنية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 3 , IdBranche = null, Nom = "Mathématiques", NomAr = "الرياضيات"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 3 , IdBranche = null, Nom = "Activité scientifique", NomAr = "النشاط العلمي"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 3 , IdBranche = null, Nom = "Arabe", NomAr = "العربية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 3 , IdBranche = null, Nom = "Français", NomAr = "الفرنسية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 3 , IdBranche = null, Nom = "Education Islamique", NomAr = "تربية إسلامية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 3 , IdBranche = null, Nom = "Éducation artistique", NomAr = "التربية الفنية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 4 , IdBranche = null, Nom = "Mathématiques", NomAr = "الرياضيات"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 4 , IdBranche = null, Nom = "Activité scientifique", NomAr = "النشاط العلمي"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 4 , IdBranche = null, Nom = "Arabe", NomAr = "العربية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 4 , IdBranche = null, Nom = "Français", NomAr = "الفرنسية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 4 , IdBranche = null, Nom = "Education Islamique", NomAr = "تربية إسلامية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 4 , IdBranche = null, Nom = "Éducation artistique", NomAr = "التربية الفنية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 4 , IdBranche = null, Nom = "Histoire Géographie", NomAr = "التاريخ والجغرافيا"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 5 , IdBranche = null, Nom = "Mathématiques", NomAr = "الرياضيات"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 5 , IdBranche = null, Nom = "Activité scientifique", NomAr = "النشاط العلمي"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 5 , IdBranche = null, Nom = "Arabe", NomAr = "العربية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 5 , IdBranche = null, Nom = "Français", NomAr = "الفرنسية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 5 , IdBranche = null, Nom = "Education Islamique", NomAr = "تربية إسلامية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 5 , IdBranche = null, Nom = "Éducation artistique", NomAr = "التربية الفنية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 5 , IdBranche = null, Nom = "Histoire Géographie", NomAr = "التاريخ والجغرافيا"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 6 , IdBranche = null, Nom = "Mathématiques", NomAr = "الرياضيات"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 6 , IdBranche = null, Nom = "Activité scientifique", NomAr = "النشاط العلمي"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 6 , IdBranche = null, Nom = "Arabe", NomAr = "العربية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 6 , IdBranche = null, Nom = "Français", NomAr = "الفرنسية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 6 , IdBranche = null, Nom = "Education Islamique", NomAr = "تربية إسلامية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 6 , IdBranche = null, Nom = "Éducation artistique", NomAr = "التربية الفنية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 6 , IdBranche = null, Nom = "Histoire Géographie", NomAr = "التاريخ والجغرافيا"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 7 , IdBranche = null, Nom = "Mathématiques", NomAr = "الرياضيات"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 7 , IdBranche = null, Nom = "Physique et Chimie", NomAr = "الفيزياء والكيمياء"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 7 , IdBranche = null, Nom = "Sciences de la Vie et de la Terre ", NomAr = "علوم الأرض والحياة"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 7 , IdBranche = null, Nom = "Arabe", NomAr = "العربية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 7 , IdBranche = null, Nom = "Français", NomAr = "الفرنسية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 7 , IdBranche = null, Nom = "Education Islamique", NomAr = "تربية إسلامية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 7 , IdBranche = null, Nom = "Informatique", NomAr = "علوم الكمبيوتر"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 7 , IdBranche = null, Nom = "Histoire Géographie", NomAr = "التاريخ والجغرافيا"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 8 , IdBranche = null, Nom = "Mathématiques", NomAr = "الرياضيات"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 8 , IdBranche = null, Nom = "Physique et Chimie", NomAr = "الفيزياء والكيمياء"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 8 , IdBranche = null, Nom = "Sciences de la Vie et de la Terre ", NomAr = "علوم الأرض والحياة"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 8 , IdBranche = null, Nom = "Arabe", NomAr = "العربية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 8 , IdBranche = null, Nom = "Français", NomAr = "الفرنسية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 8 , IdBranche = null, Nom = "Education Islamique", NomAr = "تربية إسلامية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 8 , IdBranche = null, Nom = "Informatique", NomAr = "علوم الكمبيوتر"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 8 , IdBranche = null, Nom = "Histoire Géographie", NomAr = "التاريخ والجغرافيا"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 8 , IdBranche = null, Nom = "Technologie Industrielle", NomAr = "التكنولوجيا الصناعية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 9 , IdBranche = null, Nom = "Mathématiques", NomAr = "الرياضيات"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 9 , IdBranche = null, Nom = "Physique et Chimie", NomAr = "الفيزياء والكيمياء"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 9 , IdBranche = null, Nom = "Sciences de la Vie et de la Terre ", NomAr = "علوم الأرض والحياة"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 9 , IdBranche = null, Nom = "Arabe", NomAr = "العربية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 9 , IdBranche = null, Nom = "Français", NomAr = "الفرنسية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 9 , IdBranche = null, Nom = "Education Islamique", NomAr = "تربية إسلامية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 9 , IdBranche = null, Nom = "Informatique", NomAr = "علوم الكمبيوتر"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 9 , IdBranche = null, Nom = "Histoire Géographie", NomAr = "التاريخ والجغرافيا"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 9 , IdBranche = null, Nom = "Technologie Industrielle", NomAr = "التكنولوجيا الصناعية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 9 , IdBranche = null, Nom = "Anglais", NomAr = "الإنجليزية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 10, IdBranche = 1, Nom = "Mathématiques", NomAr = "الرياضيات"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 10, IdBranche = 1, Nom = "Physique et Chimie", NomAr = "الفيزياء والكيمياء"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 10, IdBranche = 1, Nom = "Sciences de la Vie et de la Terre ", NomAr = "علوم الأرض والحياة"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 10, IdBranche = 1, Nom = "Arabe", NomAr = "العربية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 10, IdBranche = 1, Nom = "Français", NomAr = "الفرنسية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 10, IdBranche = 1, Nom = "Education Islamique", NomAr = "تربية إسلامية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 10, IdBranche = 1, Nom = "Informatique", NomAr = "علوم الكمبيوتر"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 10, IdBranche = 1, Nom = "Histoire Géographie", NomAr = "التاريخ والجغرافيا"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 10, IdBranche = 1, Nom = "Philosophie", NomAr = "الفلسفة"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 10, IdBranche = 1, Nom = "Anglais", NomAr = "الإنجليزية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 11, IdBranche = 6, Nom = "Mathématiques", NomAr = "الرياضيات"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 11, IdBranche = 6, Nom = "Physique et Chimie", NomAr = "الفيزياء والكيمياء"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 11, IdBranche = 6, Nom = "Sciences de la Vie et de la Terre ", NomAr = "علوم الأرض والحياة"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 11, IdBranche = 6, Nom = "Arabe", NomAr = "العربية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 11, IdBranche = 6, Nom = "Français", NomAr = "الفرنسية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 11, IdBranche = 6, Nom = "Education Islamique", NomAr = "تربية إسلامية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 11, IdBranche = 6, Nom = "Histoire Géographie", NomAr = "التاريخ والجغرافيا"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 11, IdBranche = 6, Nom = "Philosophie", NomAr = "الفلسفة"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 11, IdBranche = 6, Nom = "Anglais", NomAr = "الإنجليزية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 12, IdBranche = 13, Nom = "Mathématiques", NomAr = "الرياضيات"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 12, IdBranche = 13, Nom = "Physique et Chimie", NomAr = "الفيزياء والكيمياء"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 12, IdBranche = 13, Nom = "Sciences de la Vie et de la Terre ", NomAr = "علوم الأرض والحياة"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 12, IdBranche = 13, Nom = "Arabe", NomAr = "العربية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 12, IdBranche = 13, Nom = "Français", NomAr = "الفرنسية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 12, IdBranche = 13, Nom = "Education Islamique", NomAr = "تربية إسلامية"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 12, IdBranche = 13, Nom = "Philosophie", NomAr = "الفلسفة"},
            new Cours {Id = id++, Semester = 1, IdNiveauScolaire = 12, IdBranche = 13, Nom = "Anglais", NomAr = "الإنجليزية"}
            };

            modelBuilder.Entity<Cours>().HasData(list);
            return modelBuilder;
        }

        public static ModelBuilder ContactUss(this ModelBuilder modelBuilder)
        {
            int id = 1;
            var faker = new Faker<ContactUs>(DataSeeding.lang)
                .CustomInstantiator(f => new ContactUs { Id = id++ })
                .RuleFor(o => o.Object, f => f.Lorem.Word())
                .RuleFor(o => o.Msg, f => f.Lorem.Word())
                .RuleFor(o => o.Date, f => f.Date.Past())
                .RuleFor(o => o.IdUser, f => f.Random.Number(1, 10))
                ;
            modelBuilder.Entity<ContactUs>().HasData(faker.Generate(10));
            return modelBuilder;
        }

        public static ModelBuilder OffreProfs(this ModelBuilder modelBuilder)
        {
            int id = 1;
            var list = new[] {
                "65% - 95%",
                "55% - 95%",
                "55% - 95%",
                "50% - 95%",
            };

            var faker = new Faker<OffreProf>(DataSeeding.lang)
                .CustomInstantiator(f => new OffreProf { Id = id++ })
                .RuleFor(o => o.Interval, f => list[id - 2])
                .RuleFor(o => o.Value, f => 75)
                .RuleFor(o => o.IdTypeCours, f => id - 1)
                ;
            modelBuilder.Entity<OffreProf>().HasData(faker.Generate(4));
            return modelBuilder;
        }

        public static ModelBuilder Quizs(this ModelBuilder modelBuilder)
        {
            int id = 1;
            var faker = new Faker<Quiz>(DataSeeding.lang)
                .CustomInstantiator(f => new Quiz { Id = id++ })
.RuleFor(o => o.Title, f => f.Lorem.Word())
.RuleFor(o => o.Description, f => f.Lorem.Word())
.RuleFor(o => o.EnableTime, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.Date, f => f.Date.Past())
.RuleFor(o => o.IsActive, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.IdContext, f => f.Random.Number(1, 10))
;
            modelBuilder.Entity<Quiz>().HasData(faker.Generate(10));
            return modelBuilder;
        }

        public static ModelBuilder Questions(this ModelBuilder modelBuilder)
        {
            int id = 1;
            var faker = new Faker<Question>(DataSeeding.lang)
                .CustomInstantiator(f => new Question { Id = id++ })
                .RuleFor(o => o.Value, f => f.Lorem.Word())
                .RuleFor(o => o.ResponsesString, f => f.Lorem.Word())
                .RuleFor(o => o.Choices, f => f.Lorem.Word())
                .RuleFor(o => o.IsMultiChoises, f => id - 1 == 1 ? true : f.Random.Bool())
                .RuleFor(o => o.Time, f => f.Random.Number(1, 10))
                .RuleFor(o => o.IdQuiz, f => f.Random.Number(1, 10))
                ;
            modelBuilder.Entity<Question>().HasData(faker.Generate(10));
            return modelBuilder;
        }

        public static ModelBuilder Responses(this ModelBuilder modelBuilder)
        {
            int id = 1;
            var faker = new Faker<Response>(DataSeeding.lang)
                .CustomInstantiator(f => new Response { Id = id++ })
.RuleFor(o => o.TrueResponse, f => f.Lorem.Word())
.RuleFor(o => o.UserResponse, f => f.Lorem.Word())
.RuleFor(o => o.Date, f => f.Date.Past())
.RuleFor(o => o.Note, f => f.Random.Number(1, 10))
.RuleFor(o => o.IdQuestion, f => f.Random.Number(1, 10))
.RuleFor(o => o.IdUser, f => f.Random.Number(1, 10))
;
            modelBuilder.Entity<Response>().HasData(faker.Generate(10));
            return modelBuilder;
        }


    }
}