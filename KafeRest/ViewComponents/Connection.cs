using KafeRest.Data;
using Microsoft.AspNetCore.Mvc;

namespace KafeRest.ViewComponents;

    public class Connection:ViewComponent
    {
       private readonly ApplicationDbContext _db;
    public Connection(ApplicationDbContext db)
    {
        _db= db;

    }
    public IViewComponentResult Invoke()
    {
        var connection = _db.Connections.ToList();

        return View(connection);
    }

}

