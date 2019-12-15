﻿using BlogSystem.Dto;
using BlogSystem.IBLL;
using BlogSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BLL
{
    public class UserManager : IUserManager
    {

        public async Task ChangePassword(string email, string oldPwd, string newPwd)
        {
            using (IDAL.IUserService userSvc = new DAL.UserService())
            {
                if (await userSvc.GetAllAsync().AnyAsync(predicate: m => m.Email == email && m.Password == oldPwd))
                {
                    var user = await userSvc.GetAllAsync().FirstAsync(predicate: m => m.Email == email);
                    user.Password = newPwd;
                    await userSvc.EditAsync(user);
                }
            }
        }

        public Task ChangeUserInfomation(string email, string siteName, string imagePath)
        {
            throw new NotImplementedException();
        }

        public async Task<UserformationDto> GetUserEmail(string email)
        {
            using (IDAL.IUserService userSvc = new DAL.UserService())
            {
                if (await userSvc.GetAllAsync().AnyAsync(predicate: m => m.Email == email))
                {
                    return await userSvc.GetAllAsync().Where(m => m.Email == email).Select(m => new UserformationDto()
                    {
                        Id = m.Id,
                        Email = m.Email,
                        FansCount = m.FansCount,
                        ImagePath = m.ImagePath,
                        SiteName = m.SiteName,
                        Followings = m.Followings
                    }).FirstAsync();
                }
                else
                {
                    throw new ArgumentException(message: "邮箱地址不存在");
                }
            }
        }

        public async Task<bool> Login(string email, string password)
        {
            using (IDAL.IUserService userSvc = new DAL.UserService())
            {
                return await userSvc.GetAllAsync().AnyAsync(predicate: m => m.Email == email && m.Password == password);
            }
        }

        public async Task Register(string email, string password)
        {
            using (IDAL.IUserService userSvc = new DAL.UserService())
            {
                await userSvc.CreateAsync(new User()
                {
                    Email = email,
                    Password = password,
                    SiteName = "默认站点",
                    ImagePath = "default.png"
                });
            }
        }
    }
}
