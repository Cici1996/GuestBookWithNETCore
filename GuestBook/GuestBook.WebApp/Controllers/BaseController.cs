using GuestBook.WebApp.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GuestBook.WebApp.Controllers
{
    [ServiceFilter(typeof(GlobalDynamicAuthorizeAttribute))]
    public class BaseController : Controller
    {
    }
}