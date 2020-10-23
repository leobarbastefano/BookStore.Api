using Autofac.Extras.Moq;
using AutoMapper;
using BookStore.Api.Controllers;
using BookStore.Api.Entities;
using BookStore.Api.Helpers;
using BookStore.Api.Models.Users;
using BookStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BookStore.Xunit.Test
{
    public class UserTest
    {
        [Fact]
        public void TestCreateUser()
        {           
            User _user = new User();

            _user.FirstName = "John";
            _user.LastName = "Papa";
            _user.Username = "jpapa";

            string password = "123";

            byte[] passwordHash, passwordSalt;
            UserService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            _user.PasswordHash = passwordHash;
            _user.PasswordSalt = passwordSalt;

            var user = UserService.CreateCheck(_user, password);

            Assert.Equal("John", user.FirstName);
            Assert.Equal("Papa", user.LastName);
            Assert.Equal("jpapa", user.Username);
            Assert.Equal(passwordHash.ToString(), user.PasswordHash.ToString());
            Assert.Equal(passwordSalt.ToString(), user.PasswordSalt.ToString());

        }

        [Fact]
        public void TestUpdateUser()
        {
            User _user = new User();

            _user.FirstName = "John";
            _user.LastName = "Papa";
            _user.Username = "jpapa";

            string password = "123";

            byte[] passwordHash, passwordSalt;
            UserService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            _user.PasswordHash = passwordHash;
            _user.PasswordSalt = passwordSalt;

            var user = _user;
            var userReturn = UserService.UpdateCheck(_user, user, password);

            Assert.Equal("John", userReturn.FirstName);
            Assert.Equal("Papa", userReturn.LastName);
            Assert.Equal("jpapa", userReturn.Username);
            Assert.Equal(passwordHash.ToString(), userReturn.PasswordHash.ToString());
            Assert.Equal(passwordSalt.ToString(), userReturn.PasswordSalt.ToString());

        }

    }
}
