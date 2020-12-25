namespace BusinessLayer.Managers
{
    using Common;
    using Common.Views;
    using DataAccessLayer;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class UserManager
    {
        public static void SaveUser(UserView userToSave)
        {
            UserDAL userHandler = new UserDAL();
            User pet = new User
            {
                firstName = userToSave.FirstName,
                lastName = userToSave.LastName,
                address = userToSave.Address,
                description = userToSave.Description,
                email = userToSave.Email,
                nickName = userToSave.NickName,
                createdBy = Constant.ADMIN_EMAIL,
                createdDate = DateTime.Now,
            };
            userHandler.Post(pet);
        }

        public static List<UserView> GetUsers(int clientId = 0)
        {
            UserDAL userHandler = new UserDAL();
            List<UserView> userList = new List<UserView>();
            var users = userHandler.GetList();

            foreach (var user in users)
            {
                userList.Add(new UserView 
                {
                    FirstName = user.firstName,
                    LastName = user.lastName,
                    NickName = user.nickName,
                    Address = user.address,
                    Description = user.description,
                    Email = user.email,
                });
            }

            return userList;
        }

        public static UserView GetUser(string name)
        {
            UserDAL userHandler = new UserDAL();
            List<User> users = userHandler.GetList();
            User user = users.Where(u => u.nickName.Trim() == name.Trim()).FirstOrDefault();
            if (user != null)
            {
                return new UserView
                {
                    Address = user.address,
                    Description = user.description,
                    Email = user.email,
                    FirstName = user.firstName,
                    LastName = user.lastName,
                    NickName = user.nickName,
                    Id = user.id.ToString(),
                };
            }
            else
            {
                return null;
            }
        }

        public static UserView GetUser(Guid id)
        {
            UserDAL userHandler = new UserDAL();
            List<User> users = userHandler.GetList();
            User user = users.Where(u => u.id.Equals(id)).FirstOrDefault();
            if (user != null)
            {
                return new UserView
                {
                    Address = user.address,
                    Description = user.description,
                    Email = user.email,
                    FirstName = user.firstName,
                    LastName = user.lastName,
                    NickName = user.nickName,
                    Id = user.id.ToString(),
                };
            }
            else
            {
                return null;
            }
        }
    }
}
