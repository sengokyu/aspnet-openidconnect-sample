using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    [Authorize]
    public class ProtectedModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
