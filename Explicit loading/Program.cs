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
using (ApplicationContext db = new ApplicationContext())
{
    Company? company1 = db.Companies.Find(1);
    db.Users.Where(u => u.CompanyId == company1.Id).Load();
    

    if (company1 != null)
    {
        db.Users.Where(u => u.CompanyId == company1.Id).Load();
        foreach (var u in company1.Users) Console.WriteLine($"User: {u.Name}");
    }

    Company? company2 = db.Companies.Find(2);
    db.Users.Where(u => u.CompanyId == company2.Id).Load();
    if (company2 != null)
    {
        db.Users.Where(u => u.CompanyId == company2.Id).Load();
        foreach (var u in company2.Users) Console.WriteLine($"User: {u.Name}");
    }
    // получаем всех сотрудников
    foreach (var u in db.Users) Console.WriteLine($"User: {u.Name}");
}
