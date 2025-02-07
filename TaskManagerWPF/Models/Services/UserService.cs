using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TaskManagerWPF.Models.Contexts;
using TaskManagerWPF.Models.Dtos;

namespace TaskManagerWPF.Models.Services
{
    public class UserService : BaseService<UserDto, User>
    {



        public override void AddModel(User model)
        {
            DatabaseContext.Users.Add(model);
            DatabaseContext.SaveChanges();
        }

        public override User CreateModel()
        {
            return new User()
            {
                IsActive = true,
                CreatedAt = DateTime.Now
            };
        }

        public override void UpdateModel(User model)
        {
            throw new NotImplementedException("");
        }

        public override User GetModel(int id)
        {
            throw new NotImplementedException("");
        }

       
        public override void DeleteModel(UserDto model)
        {
            var user = DatabaseContext.Users.FirstOrDefault(item => item.UserId == model.UserId);

            if (user != null)
            {
                user.IsActive = false;
                user.DeletedAt = DateTime.Now;
                DatabaseContext.SaveChanges();
            }
        }

        
        public override List<UserDto> GetModels()
        {
            IQueryable<User> query = DatabaseContext.Users.Include(u => u.Role)
                .Where(u => u.DeletedAt == null);

            
            if (!string.IsNullOrEmpty(SearchInput))
            {
                if (SearchProperty == nameof(User.Username))
                {
                    query = query.Where(u => u.Username.Contains(SearchInput));
                }
                if (SearchProperty == nameof(User.Email))
                {
                    query = query.Where(u => u.Email.Contains(SearchInput));
                }
                else if (SearchProperty == "RoleName")
                {
                    query = query.Where(u => u.Role != null && u.Role.RoleName.Contains(SearchInput));
                }
            }

            return query
            .Select(u => new UserDto
            {
                UserId = u.UserId,
                Username = u.Username,

                Email = u.Email,
                RoleName = u.Role != null ? u.Role.RoleName : "No role"
            })
            .ToList();
        }

      
   
     public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(User.Username),
                    DisplayName = "Username"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(User.Email),
                    DisplayName = "Email"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = "RoleName",
                    DisplayName = "Role"
                },
            };
        }

        public override string ValidateProperty(string columnName, User model)
        {
            if (columnName == nameof(User.Username))
            {
                if (string.IsNullOrEmpty(model.Username))
                {
                    return "Username is required";
                }
            }
            if(columnName == nameof(User.Email))
            {
                if (string.IsNullOrEmpty(model.Email))
                {
                    return "Email is required";
                }
                if(!model.Email.Contains("@"))
                {
                    return "Invalid email format (must contain '@')";
                }

            }
            return string.Empty;
        }





    }  
        }

       
    

