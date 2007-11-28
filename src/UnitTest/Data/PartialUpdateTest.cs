
using System;
using System.Collections.Generic;
using System.Text;
using Lephone.Data;
using Lephone.Data.Definition;
using Lephone.MockSql.Recorder;
using NUnit.Framework;

namespace Lephone.UnitTest.Data
{
    [TestFixture]
    public class PartialUpdateTest
    {
        public abstract class User : DbObjectModel<User>
        {
            public abstract string Name { get; set; }
            public abstract int Age { get; set; }
        }

        public static readonly DbContext de = new DbContext("SQLite");

        [SetUp]
        public void SetUp()
        {
            StaticRecorder.ClearMessages();
        }

        [Test]
        public void Test1()
        {
            User u = User.New();
            u.Id = 1;
            u.Name = "tom";
            de.Save(u);
            Assert.AreEqual("Update [User] Set [Name]=@Name_0  Where [Id] = @Id_1;\n", StaticRecorder.LastMessage);
        }

        [Test]
        public void Test2()
        {
            User u = User.New();
            u.Id = 1;
            u.Age = 19;
            de.Save(u);
            Assert.AreEqual("Update [User] Set [Age]=@Age_0  Where [Id] = @Id_1;\n", StaticRecorder.LastMessage);
        }
    }
}