using System;
using System.IO;
using PN.Models;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PN.Services
{
    public class UserService : IDisposable
    {
        public UserService()
        {
            InizialiseComponents();
        }

        #region SETTERS AND GETTERS

        public string UserId { get; private set; }

        public UserInformation UserInformation { get; private set; }

        public ApplicationDbContext db { get; private set; }

        public RoleManager<IdentityRole> RoleManager { get; private set; }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        public bool Disposing { get; private set; }

        #endregion

        #region Public Methods

        public UserInformation GetUserInformation()
        {
            using (var _db = new AppDbContext())
            {
                return Task.Run(async () => await _db.StoredProcedureGetUserInformationId(UserId)).Result;
            }
        }

        public bool CreateRole(string name)
        {
            try
            {
                this.RoleManager.Create(new IdentityRole(name));
                return true;
            }
            catch (Exception) { return false; }
        }

        public bool DeleteRole(string name)
        {
            try
            {
                var role = this.RoleManager.FindByName(name);
                this.RoleManager.Delete(role);
                return true;
            }
            catch (Exception) { return false; }
        }

        public bool AddRoleToUser(string name, string userId = null)
        {
            try
            {
                var id = userId ?? this.UserId;
                this.UserManager.AddToRole(id, name);
                return true;
            }
            catch (Exception) { return false; }
        }

        public bool RemoveRoleFromUser(string name, string userId = null)
        {
            try
            {
                var id = userId ?? this.UserId;
                this.UserManager.RemoveFromRole(id, name);
                return true;
            }
            catch (Exception) { return false; }
        }

        public IList<string> GetUserRoles(string userId = null)
        {
            try
            {
                var id = userId ?? this.UserId;
                return this.UserManager.GetRoles(id);
            }
            catch (Exception) { return null; }
        }

        public bool IsUserInRole(string name, string userId = null)
        {
            try
            {
                var id = userId ?? this.UserId;
                return this.UserManager.IsInRole(id, name);
            }
            catch (Exception) { return false; }
        }

        #endregion

        #region Auxiliary Methods

        private void InizialiseComponents()
        {
            this.db = new ApplicationDbContext();
            this.UserId = HttpContext.Current.User.Identity.GetUserId();
            this.UserInformation = GetUserInformation();
            this.RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            this.Disposing = false;
        }

        public void Dispose(bool disposing)
        {
            Disposing = disposing;
            if (Disposing) Dispose();
        }

        public void Dispose()
        {
            UserId = null;
            UserInformation = null;
            if (db != null)
            {
                db.Dispose();
                db = null;
            }
            if (RoleManager != null)
            {
                RoleManager.Dispose();
                RoleManager = null;
            }
            if (UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
        }

        #endregion
    }

    /* Implementacion de clase
     * 
     * // Cerar role
     * if (User.Identity.IsAutenticated) { 
     *     var userService = new UserService();
     *     var resoult = userService.CreateRole("Admin");
     * }
     * 
     * // Eliminar role
     * if (User.Identity.IsAutenticated) { 
     *     var userService = new UserService();
     *     var resoult = userService.DeleteRole("Admin");
     * }
     * 
     * // Agregar role a usuario
     * if (User.Identity.IsAutenticated) { 
     *     var userService = new UserService();
     *     var resoult = userService.AddRoleToUser("Admin");
     * }User
     * 
     * // Eliminar usuario de role
     * if (User.Identity.IsAutenticated) { 
     *     var userService = new UserService();
     *     var resoult = userService.RemoveRoleFromUser("Admin");
     * }
     * 
     * // Obtener la lista de roles de un usuario
     * if (User.Identity.IsAutenticated) { 
     *     var userService = new UserService();
     *     var resoult = UserService.GetUserRoles();
     * }
     * 
     * // Verificar si el usuario esta en un role
     * if (User.Identity.IsAutenticated) { 
     *     var userService = new UserService();
     *     var resoult = userService.IsUserInRole("Admin");
     * }
     */
}