
using System;
using LegacyApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//Test Jednostkowy тестируют только наш код , внешние сервисы и базу данных не можем , за это отвечает уже интеграционные тесты 
[TestClass]
public class ProgramTest

{
    [TestMethod]
    public void AddUser_ReturnFalseWhenFirstNameNullOrEmpty()
    {
        //Arrange (creating variables for testing)
        var userService = new UserService();
        
        //Act (creating test , call some method)
        var result = userService.AddUser("n", "Diduk", "s27736@pjwstk.edu.pl", DateTime.Parse("1982-03-21"), 1);
        
        Console.WriteLine(result);
        
        //Assert ,check result for our expectations
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void AddUser_ReturnFalseWhenBadEmail()
    {
        //Arrage
        var userService = new UserService();
        var email = "s51516516@.12jfr";
        
        //Act
        var result = userService.AddUser("n", "Diduk", email, DateTime.Parse("1982-03-21"), 1);
        Console.WriteLine(result);
        
        //Assert
        Assert.IsTrue(result);
    }


    [TestMethod]
    public void AddUser_ReturnFalseWhenUserOlder21()
    {
        
        //Arrage
        var userService = new UserService();
        //Act
        var result = userService.AddUser("n", "Diduk", "s27736@pjwstk.edu.pl", DateTime.Parse("2003-03-21"), 1);
        Console.WriteLine(result);
        //Assert
        Assert.IsTrue(result);
    }


    [TestMethod]
    public void AddUser_ReturnsTrueWhenVeryImportantClient()
    {   
        //Arrage
        ClientRepository clientRepository = new ClientRepository();
        var client = clientRepository.GetById(2).Type;
        
        //Act
        Console.WriteLine(client);
        
        //Assert
        Assert.AreEqual("VeryImportantClient",client);
        
    }
    
}