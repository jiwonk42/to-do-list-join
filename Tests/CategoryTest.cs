using Xunit;
using ToDoList.Objects;
using System;
using System.Collections.Generic;

namespace ToDoList
{
    public class CategoryTest : IDisposable
    {
        public CategoryTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=todo_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_CategoryDatabaseFirstEmpty()
        {

            //Arrange
            int result = Task.GetAll().Count;

            //Assert
            Assert.Equal(result, 0);

        }

        [Fact]
        public void Test_Save_SaveCategoryToDatabase()
        {
            // Arrange
                Category newCategory = new Category("Lawn Chores");

            // Act
                newCategory.Save();
                List<Category> allCategories = Category.GetAll();
                List<Category> testList = new List<Category>{newCategory};

            // Assert
                Assert.Equal(testList, allCategories);

        }

        [Fact]
        public void Test_Save_AssignsIdToCategory()
        {
            // Arrange
            Category newCategory = new Category("Chores");

            // Act
            newCategory.Save();
            Category savedCategory = Category.GetAll()[0];

            int resultId = savedCategory.GetId();
            int testId = newCategory.GetId();

            // Assert
            Assert.Equal(testId, resultId);
        }

        [Fact]
        public void Test_Find_FindTaskInDatabase()
        {
            //Arrange
            Category newCategory = new Category("Chores");
            newCategory.Save();

            //Act
            Category foundCategory = Category.Find(newCategory.GetId());

            //Assert
            Assert.Equal(newCategory, foundCategory);
        }

        [Fact]
        public void Test_GetTasks_RetrieveAllTasksInCategory()
        {
            //Arrange
            Category newCategory = new Category("Kitchen");
            newCategory.Save();

            Task firstTask = new Task("Wash Dishes", newCategory.GetId(), "2000-01-01");
            firstTask.Save();
            Task secondTask = new Task("Empty Dishwasher", newCategory.GetId(), "2000-01-01");
            secondTask.Save();

            //Act
            List<Task> testTaskList = new List<Task> {firstTask, secondTask};
            List<Task> resultTaskList = newCategory.GetTasks();

            //Assert
            Assert.Equal(testTaskList, resultTaskList);

        }


        public void Dispose()
        {
            Task.DeleteAll();
            Category.DeleteAll();
        }
    }
}