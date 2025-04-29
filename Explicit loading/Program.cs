using Microsoft.EntityFrameworkCore;
using static Explicit_loading.Models;

using (ApplicationContext db = new ApplicationContext())
{
    // пересоздадим базу данных
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

    // добавляем начальные данные
    Company microsoft = new Company { Name = "Microsoft" };
    Company google = new Company { Name = "Google" };
    db.Companies.AddRange(microsoft, google);


    User tom = new User { Name = "Tom", Company = microsoft };
    User bob = new User { Name = "Bob", Company = google };
    User alice = new User { Name = "Alice", Company = microsoft };
    User kate = new User { Name = "Kate", Company = google };
    db.Users.AddRange(tom, bob, alice, kate);

    db.SaveChanges();
}
//метод с использованием Load
using (ApplicationContext db = new ApplicationContext())
{
    Company? company = db.Companies.FirstOrDefault();
    if (company != null)
    {
        db.Users.Where(u => u.CompanyId == company.Id).Load();

        Console.WriteLine($"Company: {company.Name}");
        foreach (var u in company.Users)
            Console.WriteLine($"User: {u.Name}");
    }
}
//с использованием метода Collection
using (ApplicationContext db = new ApplicationContext())
{
    Company? company = db.Companies.FirstOrDefault();
    if (company != null)
    {
        db.Entry(company).Collection(c => c.Users).Load();

        Console.WriteLine($"Company: {company.Name}");
        foreach (var u in company.Users)
            Console.WriteLine($"User: {u.Name}");
    }
}
//с использованием метода Reference
using (ApplicationContext db = new ApplicationContext())
{
    User? user = db.Users.FirstOrDefault();  // получаем первого пользователя
    if (user != null)
    {
        db.Entry(user).Reference(u => u.Company).Load();
        Console.WriteLine($"{user.Name} - {user.Company?.Name}");   // Tom - Microsoft
    }
}
//обращение с использованием Local
using (ApplicationContext db = new ApplicationContext())
{
    Company? company1 = db.Companies.Find(1);
    if (company1 != null)
    {
        db.Users.Where(u => u.CompanyId == company1.Id).Load();
        foreach (var u in company1.Users) Console.WriteLine($"User: {u.Name}");
    }

    Company? company2 = db.Companies.Find(2);
    if (company2 != null)
    {
        db.Users.Where(u => u.CompanyId == company2.Id).Load();
        foreach (var u in company2.Users) Console.WriteLine($"User: {u.Name}");
    }
    // получаем всех сотрудников
    foreach (var u in db.Users) Console.WriteLine($"User: {u.Name}");
}