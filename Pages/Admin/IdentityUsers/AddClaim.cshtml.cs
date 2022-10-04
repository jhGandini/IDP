using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Xml.Linq;

namespace IDP.Pages.Admin.IdentityUsers;

public class AddClaimModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;

    public AddClaimModel(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        public InputModel(IdentityUser user, List<Claim> claims)
        {
            User = user;
            Claims = new List<Claim>(claims);
        }

        public IdentityUser User { get; set; }

        public List<Claim> Claims { get; set; }
    }

    public string error { get; set; }

    public async void OnGetAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            error = "Usuário não encontrado!";            
        }
        else
        {
            Input = new InputModel(user, (await _userManager.GetClaimsAsync(user)).ToList());            
        }
    }
}
