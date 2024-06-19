using KafeRest.Data;
using Microsoft.AspNetCore.Mvc;

namespace KafeRest.ViewComponents;

	public class CategoryList:ViewComponent
	{
	   public readonly ApplicationDbContext _db;
	   
	   public CategoryList(ApplicationDbContext db)
	   {
	   	_db=db;
	   }
	   public IViewComponentResult Invoke()
	   {
	   	var category=_db.Categories.ToList();
	   	return View(category);
	   }
    }

