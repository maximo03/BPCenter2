using BPCenter2.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BPCenter2.Data
{
    public class SeedDB(AppDbContext context)
    {

        private readonly AppDbContext _context = context;

        public async Task SeedAsync()
        {
            //roles: Admin, TS, EF
            await _context.Database.EnsureCreatedAsync();
            await CheckUsersAsync("NESTOR FRANCISCO", "VALLE RUIZ", "nestorrzv@gmail.com", "maximo", "Admin");
            await CheckUsersAsync("MARIA ISABEL", "HERRERA HERRERA", "maisauwu@gmail.com", "maisauwu", "Tetser");

        }

        private async Task<UsersAccount> CheckUsersAsync(string userNames, string userLastNames, string userEmail, string userPassword, string userRole)
        {
            var usuarioExistente = await _context.UsersAccount.FirstOrDefaultAsync(u => u.userEmail == userEmail);
            if (usuarioExistente != null)
            {
                return usuarioExistente!;
            }

            UsersAccount usuario = new()
            {

                userNames = userNames,
                userLastNames = userLastNames,
                userEmail = userEmail,
                userPassword = userPassword,
                userRole = userRole

            };

            usuario.userPassword = BCrypt.Net.BCrypt.HashPassword(usuario.userPassword);

            _context.UsersAccount.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

    }

}
