using KafeRest.Data;
using Microsoft.AspNetCore.Mvc;
namespace KafeRest.ViewComponents;


    public class Comments:ViewComponent
    {
      private readonly ApplicationDbContext _db;
      public Comments(ApplicationDbContext _db)
      {
        this._db = _db;
      }
     
     public IViewComponentResult Invoke()
    {
        var comment = _db.Blogs.Where(i => i.IsAgree).ToList();
        return View(comment);
    }


}

