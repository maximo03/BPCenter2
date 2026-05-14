using BPCenter2.Data;
using BPCenter2.Models.DTOs;
using BPCenter2.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BPCenter2.Services
{
    public class ServicesLogin(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public UsersAccount? selectUser;
        public LoginDTO loginDTO = new LoginDTO();


        public bool IsAuthenticated { get; set; } = false;
        public async Task<LoginDTO> VerifyUser(string userEmail, string password)
        {
            selectUser = await _context.UsersAccount
                .FirstOrDefaultAsync(x => x.userEmail == userEmail);

            if (selectUser != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, selectUser!.userPassword))
                {
                    IsAuthenticated = true;
                }
                else
                {
                    IsAuthenticated = false;
                }
            }
            else
            {
                IsAuthenticated = false;
                loginDTO.IsAuthenticated = IsAuthenticated;
                return loginDTO;
            }

            loginDTO.userFullName = selectUser!.userNames + " " + selectUser!.userLastNames;
            loginDTO.userEmail = selectUser!.userEmail!;
            loginDTO.userRole = selectUser!.userRole!;
            loginDTO.IsAuthenticated = IsAuthenticated;

            return loginDTO;
        }

    }
}
