using IDP.Pages.Admin.ApiScopes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IDP.Pages.Admin.IdentityUsers
{
    [SecurityHeaders]
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public IndexModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IEnumerable<IdentityUser> Users { get; private set; }
        public string Filter { get; set; }        

        public async Task OnGetAsync(string filter = "")
        {            
            Filter = filter;
            Users = await _signInManager.UserManager.Users.Where(x => x.UserName.Contains(filter)).ToListAsync();
        }
    }
}
