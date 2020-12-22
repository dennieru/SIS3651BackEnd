namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserDAL : IDAL<User>
    {
        private PetControlEntities context;

        public UserDAL()
        {
        }

        public UserDAL(PetControlEntities context)
        {
            this.context = context;
        }

        public bool Delete(string id)
        {
            try
            {
                using (this.context = new PetControlEntities())
                {
                    var user = new User { id = new Guid(id) };
                    this.context.Users.Attach(user);
                    this.context.Users.Remove(user);
                    this.context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public User Get(string id)
        {
            try
            {
                User user;
                using (this.context = new PetControlEntities())
                {
                    user = this.context.Users.SingleOrDefault(u => u.id == new Guid(id));
                }

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<User> GetList()
        {
            using (this.context = new PetControlEntities())
            {
                return this.context.Users.ToList();
            }
        }

        public bool Post(User user)
        {
            try
            {
                user.id = Guid.NewGuid();
                using (this.context = new PetControlEntities())
                {
                    this.context.Users.Add(user);
                    this.context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Update(ref User newUser)
        {
            try
            {
                var id = newUser.id;
                using (this.context = new PetControlEntities())
                {
                    var oldUser = this.context.Users.SingleOrDefault(b => b.id == id);
                    if (oldUser != null)
                    {
                        oldUser.address = newUser.address;
                        oldUser.description = newUser.description;
                        oldUser.email = newUser.email;
                        oldUser.firstName = newUser.firstName;
                        oldUser.lastName = newUser.lastName;
                        oldUser.nickName = newUser.nickName;
                        this.context.SaveChanges();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
