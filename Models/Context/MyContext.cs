using Microsoft.EntityFrameworkCore;

namespace Models
{
    public partial class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Discussion> Discussions { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<EventProf> EventProfs { get; set; }
        public virtual DbSet<Ville> Villes { get; set; }
        public virtual DbSet<DetailUserActivite> DetailUserActivites { get; set; }
        public virtual DbSet<TypeActivite> TypeActivites { get; set; }
        public virtual DbSet<Activite> Activites { get; set; }
        public virtual DbSet<Prof> Profs { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<TypeCours> TypeCourses { get; set; }
        public virtual DbSet<LieuCours> LieuCourses { get; set; }
        public virtual DbSet<NiveauScolaire> NiveauScolaires { get; set; }
        public virtual DbSet<Branche> Branches { get; set; }
        public virtual DbSet<Cours> Courses { get; set; }
        public virtual DbSet<ContactUs> ContactUss { get; set; }
        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<OffreProf> OffreProfs { get; set; }
        public virtual DbSet<Quiz> Quizs { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Response> Responses { get; set; }
        public virtual DbSet<Matier> Matiers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
{
    entity.HasKey(e => e.Id);
    entity.Property(e => e.Id).ValueGeneratedOnAdd();
    entity.Property(e => e.Nom);
    entity.Property(e => e.Prenom);
    entity.Property(e => e.Tel1);
    entity.Property(e => e.Tel2);
    entity.HasIndex(e => e.Email).IsUnique();
    entity.Property(e => e.Password);
    entity.Property(e => e.IsActive);
    entity.Property(e => e.Date);
    entity.Property(e => e.Adresse);
    entity.Property(e => e.ImageUrl);
    entity.Property(e => e.Cin);
    entity.Property(e => e.Role);
    entity.Property(e => e.IdVille);
    entity.HasOne(e => e.Ville).WithMany(e => e.Users).HasForeignKey(e => e.IdVille);
    entity.HasMany(e => e.DetailUserActivites).WithOne(p => p.User).HasForeignKey(e => e.IdUser).OnDelete(DeleteBehavior.Cascade);
    entity.HasMany(e => e.ContactUs).WithOne(p => p.User).HasForeignKey(e => e.IdUser).OnDelete(DeleteBehavior.Cascade);
});

            modelBuilder.Entity<Discussion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.UnReaded);
                entity.Property(e => e.Date);
                entity.Property(e => e.IdMe);
                entity.Property(e => e.IdOtherUser);
                entity.HasOne(e => e.Me).WithMany(e => e.Discussions).HasForeignKey(e => e.IdMe).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.OtherUser).WithMany(e => e.OtherUserDiscussions).HasForeignKey(e => e.IdOtherUser).OnDelete(DeleteBehavior.NoAction);
                entity.HasMany(e => e.Messages).WithOne(p => p.Discussion).HasForeignKey(e => e.IdDiscussion).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Object);
                entity.Property(e => e.Content);
                entity.Property(e => e.Vu);
                entity.Property(e => e.Date);
                entity.Property(e => e.IdCours);
                entity.Property(e => e.OtherUserName);
                entity.Property(e => e.OtherUserImage);
                entity.Property(e => e.IdMe);
                entity.Property(e => e.IdOtherUser);
                entity.Property(e => e.IdDiscussion);
                entity.HasOne(e => e.Me).WithMany(e => e.Messages).HasForeignKey(e => e.IdMe).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.OtherUser).WithMany(e => e.OtherUserMessages).HasForeignKey(e => e.IdOtherUser).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.Discussion).WithMany(e => e.Messages).HasForeignKey(e => e.IdDiscussion);
            });

            modelBuilder.Entity<EventProf>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Title);
                entity.Property(e => e.Start);
                entity.Property(e => e.End);
                entity.Property(e => e.Color);
                entity.Property(e => e.Draggable);
                entity.Property(e => e.Resizable);
                entity.Property(e => e.Month);
                entity.Property(e => e.Year);
                entity.Property(e => e.IdUser);
                entity.HasOne(e => e.User).WithMany(e => e.EventProfs).HasForeignKey(e => e.IdUser);
            });

            modelBuilder.Entity<Ville>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nom);
                entity.Property(e => e.NomAr);
                entity.HasMany(e => e.Users).WithOne(p => p.Ville).HasForeignKey(e => e.IdVille).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DetailUserActivite>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Date);
                entity.Property(e => e.IdUser);
                entity.Property(e => e.IdActivite);
                entity.HasOne(e => e.User).WithMany(e => e.DetailUserActivites).HasForeignKey(e => e.IdUser);
                entity.HasOne(e => e.Activite).WithMany(e => e.DetailUserActivites).HasForeignKey(e => e.IdActivite);
            });

            modelBuilder.Entity<TypeActivite>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nom);
                entity.Property(e => e.NomAr);
                entity.Property(e => e.ImageUrl);
                entity.Property(e => e.Active);
                entity.HasMany(e => e.Activites).WithOne(p => p.TypeActivite).HasForeignKey(e => e.IdTypeActivite).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Activite>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nom);
                entity.Property(e => e.NomAr);
                entity.Property(e => e.ImageUrl);
                entity.Property(e => e.IdTypeActivite);
                entity.HasOne(e => e.TypeActivite).WithMany(e => e.Activites).HasForeignKey(e => e.IdTypeActivite);
                entity.HasMany(e => e.DetailUserActivites).WithOne(p => p.Activite).HasForeignKey(e => e.IdActivite).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Prof>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Lien);
                entity.Property(e => e.Description);
                entity.Property(e => e.Experience);
                entity.Property(e => e.Approche);
                entity.Property(e => e.Intro);
                entity.Property(e => e.VideoUrl);
                entity.Property(e => e.CvUrl);
                entity.Property(e => e.Note);
                entity.Property(e => e.PrixHrWeb);
                entity.Property(e => e.PrixHrHome);
                entity.Property(e => e.PrixHrWebGroupe);
                entity.Property(e => e.PrixHrHomeGroupe);
                entity.Property(e => e.IdsTypeActivites);
                entity.Property(e => e.IdsActivites);
                entity.Property(e => e.IdsTypeCours);
                entity.Property(e => e.IdsLieuCours);
                entity.Property(e => e.IdsNiveauScolaires);
                entity.Property(e => e.IdUser);
                entity.HasOne(e => e.User).WithMany(e => e.Profs).HasForeignKey(e => e.IdUser);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Ecole);
                entity.Property(e => e.Niveau);
                entity.Property(e => e.Branche);
                entity.Property(e => e.NomParent);
                entity.Property(e => e.PrenomParent);
                entity.Property(e => e.Tel1Parent);
                entity.Property(e => e.Tel2Parent);
                entity.Property(e => e.IdUser);
                entity.HasOne(e => e.User).WithMany(e => e.Students).HasForeignKey(e => e.IdUser);
                entity.HasOne(e => e.Activite).WithMany(e => e.Students).HasForeignKey(e => e.IdActivite);
            });

            modelBuilder.Entity<TypeCours>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nom);
                entity.Property(e => e.NomAr);
            });

            modelBuilder.Entity<LieuCours>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nom);
                entity.Property(e => e.NomAr);
            });

            modelBuilder.Entity<NiveauScolaire>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nom);
                entity.Property(e => e.NomAr);
                entity.Property(e => e.IdCycle);
                entity.Property(e => e.CoursLigneGroupe);
                entity.Property(e => e.CoursLigneIndividuel);
                entity.Property(e => e.CoursDomicileGroupe);
                entity.Property(e => e.CoursDomicileIndividuel);
            });

            modelBuilder.Entity<Branche>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nom);
                entity.Property(e => e.NomAr);
                entity.Property(e => e.IdNiveauScolaire);
                entity.HasOne(e => e.NiveauScolaire).WithMany(e => e.Branches).HasForeignKey(e => e.IdNiveauScolaire);
            });

            modelBuilder.Entity<Matier>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name);
                entity.Property(e => e.NameAr);
                entity.HasMany(e => e.Courses).WithOne(p => p.Matier).HasForeignKey(e => e.IdMatier).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Cours>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nom);
                entity.Property(e => e.NomAr);
                entity.Property(e => e.FilesUrl);
                entity.Property(e => e.VideosUrl);
                entity.Property(e => e.Semester);
                entity.Property(e => e.IdBranche);
                entity.HasOne(e => e.Branche).WithMany(e => e.Courses).HasForeignKey(e => e.IdBranche);
                entity.Property(e => e.IdNiveauScolaire);
                entity.HasOne(e => e.NiveauScolaire).WithMany(e => e.Courses).HasForeignKey(e => e.IdNiveauScolaire).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.Matier).WithMany(e => e.Courses).HasForeignKey(e => e.IdMatier);

                entity.HasMany(e => e.Quizzes).WithOne(p => p.Context).HasForeignKey(e => e.IdContext).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ContactUs>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Object);
                entity.Property(e => e.Msg);
                entity.Property(e => e.Date);
                entity.Property(e => e.IdUser);
                entity.HasOne(e => e.User).WithMany(e => e.ContactUs).HasForeignKey(e => e.IdUser);
            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Title);
                entity.Property(e => e.Order);
                entity.Property(e => e.Description);
                entity.Property(e => e.Date);
                entity.Property(e => e.UrlVideo);
            });

            modelBuilder.Entity<OffreProf>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Interval);
                entity.Property(e => e.Value);
                entity.Property(e => e.IdTypeCours);
                entity.HasOne(e => e.TypeCours).WithMany(e => e.OffreProfes).HasForeignKey(e => e.IdTypeCours);
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Title);
                entity.Property(e => e.Description);
                entity.Property(e => e.EnableTime);
                entity.Property(e => e.Date);
                entity.Property(e => e.IsActive);
                entity.Property(e => e.IdContext);
                entity.HasOne(e => e.Context).WithMany(e => e.Quizzes).HasForeignKey(e => e.IdContext);
                entity.HasMany(e => e.Questions).WithOne(p => p.Quiz).HasForeignKey(e => e.IdQuiz).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Value);
                entity.Property(e => e.ResponsesString);
                entity.Property(e => e.Choices);
                entity.Property(e => e.IsMultiChoises);
                entity.Property(e => e.Time);
                entity.Property(e => e.IdQuiz);
                entity.HasOne(e => e.Quiz).WithMany(e => e.Questions).HasForeignKey(e => e.IdQuiz);
            });

            modelBuilder.Entity<Response>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.TrueResponse);
                entity.Property(e => e.UserResponse);
                entity.Property(e => e.Date);
                entity.Property(e => e.Note);
                entity.Property(e => e.IdQuestion);
                entity.HasOne(e => e.Question).WithMany(e => e.Responses).HasForeignKey(e => e.IdQuestion);
                entity.Property(e => e.IdUser);
                entity.HasOne(e => e.User).WithMany(e => e.Responses).HasForeignKey(e => e.IdUser);
            });




            modelBuilder
                .Videos()
                .LieuCourses()
                .TypeCourses()
                .TypeActivites()
                .Villes()
                .Users()
                .Discussions()
                .Messages()
                .EventProfs()
                .DetailUserActivites()
                .Students()
                .Activites()
                .Profs()
                .NiveauScolaires()
                .Branches()
                .Courses()
                .ContactUss()
                .OffreProfs()
                .Quizs()
                .Questions()
                .Responses()

                ;
        }


        // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
