using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.OData.Query.SemanticAst;
using Salon.Services.Models;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Services
{
   public interface IAdministratorService
    {
        IEnumerable<UserViewModel> ListAllUser();

        Task <UserRoleModel>  UserRole(string id);

        Task SetUserRole(string id, string role);

        List<SelectListItem> SetUserRole();
    }
}
