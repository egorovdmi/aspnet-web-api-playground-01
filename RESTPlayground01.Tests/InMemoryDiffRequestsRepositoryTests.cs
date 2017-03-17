using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTPlayground01.Core.Models;
using RESTPlayground01.Core.Repositories;

namespace RESTPlayground01.Tests
{
    [TestClass]
    public class InMemoryDiffRequestsRepositoryTests
    {
        [TestMethod]
        public void Single_RequstNonExistentObject_Null()
        {
            // arrange
            var repository = new InMemoryDiffRequestsRepository();

            // action
            var result = repository.Single(1);

            // assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Single_RequstExistentObject_NotNull()
        {
            // arrange
            var repository = new InMemoryDiffRequestsRepository();
            repository.Update(1, new Core.Models.DiffRequest());

            // action
            var result = repository.Single(1);

            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Update_PutNewObject_ExpectedDataInFields()
        {
            // arrange
            var repository = new InMemoryDiffRequestsRepository();
            var id = 1;
            var newObject = new DiffRequest()
            {
                Left = new byte[] { 0 },
                Right = new byte[] { 1 }
            };

            // action
            repository.Update(id, newObject);
            var result = repository.Single(id);

            // assert
            Assert.AreEqual(0, result.Left[0]);
            Assert.AreEqual(1, result.Right[0]);
        }

        [TestMethod]
        public void Update_ReplaceObject_ExpectedDataInFields()
        {
            // arrange
            var repository = new InMemoryDiffRequestsRepository();
            var id = 1;
            var oldObject = new DiffRequest() { Left = new byte[] { 0 } };
            var newObject = new DiffRequest() { Left = new byte[] { 1 } };

            // action
            repository.Update(id, oldObject);
            oldObject = repository.Single(id);

            repository.Update(id, newObject);
            newObject = repository.Single(id);

            // assert
            Assert.AreNotEqual(oldObject.Left[0], newObject.Left[0]);
        }

        [TestMethod]
        public void Update_ReplaceObject_ReferencesAreNotEqual()
        {
            // arrange
            var repository = new InMemoryDiffRequestsRepository();
            var id = 1;
            var oldObject = new DiffRequest() { Left = new byte[] { 0 } };
            repository.Update(id, oldObject);
            oldObject = repository.Single(id);

            // action
            repository.Update(id, oldObject);
            var newObject = repository.Single(id);

            // assert
            Assert.IsFalse(ReferenceEquals(oldObject, newObject));
        }
    }
}
