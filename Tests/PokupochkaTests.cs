using DbLib.DB;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class PokupochkaTests
    {
        [TestMethod]
        public void TestAddWorker()
        {
            Worker worker = new();
            worker.SecondName = "TEST";
            worker.FirstName = "TEST";
            worker.Patronymic = "TEST";
            worker.Phone = "+7(777) 777-77-77";
            worker.Email = "TEST@yandex.ru";
            worker.User!.Login = "test";
            worker.User!.Password = "test";
            worker.User!.RoleId = (int)RoleKey.Administratior;
            worker.AddOrChange();

            Worker? findWorker = DbConnect.Db.Workers.FirstOrDefault(w => w.User!.Login == worker.User!.Login);
            Assert.AreEqual(worker, findWorker, "Работник не найден в БД");
            
            worker.User!.Delete();
        }

        [TestMethod]
        public void TestChangeWorker()
        {
            Random rnd = new();
            List<Worker> workers = DbConnect.Db.Workers.ToList();

            Worker rndWorker = workers[rnd.Next(workers.Count)];
            string oldLogin = rndWorker.User!.Login;
            string newLogin = "TEST";

            rndWorker.User!.Login = newLogin;
            rndWorker.AddOrChange();

            Worker? findWorker = DbConnect.Db.Workers.FirstOrDefault(w => w.Id == rndWorker.Id);
            Assert.AreEqual(newLogin, findWorker!.User!.Login, "Данные работника не изменились");

            rndWorker.User!.Login = oldLogin;
            rndWorker.AddOrChange();
        }

        [TestMethod]
        public void TestDeleteWorker()
        {
            Worker worker = new();
            worker.SecondName = "TEST";
            worker.FirstName = "TEST";
            worker.Patronymic = "TEST";
            worker.Phone = "+7(777) 777-77-77";
            worker.Email = "TEST@yandex.ru";
            worker.User!.Login = "test";
            worker.User!.Password = "test";
            worker.User!.RoleId = (int)RoleKey.Administratior;
            worker.AddOrChange();
            worker.User!.Delete();

            Worker? findWorker = DbConnect.Db.Workers.FirstOrDefault(w => w.User!.Login == worker.User!.Login);
            Assert.AreEqual(null, findWorker, "Работник не удалён из БД");
        }
    }
}