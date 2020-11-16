using Domain.AggregatesModel.PersonAggregate;
// using Ordering.Domain.Exceptions;
using System;
using Xunit;

public class PersonAggregateTest
{
    public PersonAggregateTest()
    { }

    [Fact]
    public void Create_person_item_success()
    {
        //Arrange    
        var identity = new Guid().ToString();
        var name = "fakeName";
        var surname = "fakeSurname";
        var company = "fakeCompany";

        //Act 
        var fakePersonItem = new Person(identity, name, surname, company);

        //Assert
        Assert.NotNull(fakePersonItem);
    }

    [Fact]
    public void Create_person_item_fail()
    {
        //Arrange    
        var identity = new Guid().ToString();
        var name = "fakeName";
        //Act - Assert
        Assert.Throws<ArgumentNullException>(() => new Person(identity, name, "", ""));
    }

    [Fact]
    public void add_contactinfo_success()
    {
        //Arrange    
        var identity = new Guid().ToString();
        var name = "fakeName";
        var surname = "fakeSurname";
        var company = "fakeCompany";
        var fakePersonItem = new Person(identity, name, surname, company);
        var location = "İstanbul";
        var description = "fakeDescription";
        Phone p = new Phone("5555555555");
        Email e = Email.Create("asdf@asdf.com");
        //Act

        var result = fakePersonItem.AddContactInformation(p, e, location, description);

        //Assert
        Assert.NotNull(result);
    }
    [Fact]
    public void remove_contactinfo_success()
    {
        //Arrange    
        var identity = new Guid().ToString();
        var name = "fakeName";
        var surname = "fakeSurname";
        var company = "fakeCompany";
        var fakePersonItem = new Person(identity, name, surname, company);

        var location = "İstanbul";
        var description = "fakeDescription";
        Phone p = new Phone("5555555555");
        Email e = Email.Create("asdf@asdf.com");



        //Act

        var contactInformation = fakePersonItem.AddContactInformation(p, e, location, description);
        bool result = fakePersonItem.RemoveContactInformation(contactInformation.Id);
        //Assert
        Assert.True(result);
    }

    [Fact]
    public void add_contactinfo_duplicate_success()
    {
        //Arrange    
        var identity = new Guid().ToString();
        var name = "fakeName";
        var surname = "fakeSurname";
        var company = "fakeCompany";
        var fakePersonItem = new Person(identity, name, surname, company);
        var location = "fakeLocation";
        var description = "fakeDescription";
        Phone p = new Phone("5555555555");
        Email e = Email.Create("asdf@asdf.com");
        //Act

        var result = fakePersonItem.AddContactInformation(p, e, location, description);
        var result2 = fakePersonItem.AddContactInformation(p, e, location, description);
        var count = fakePersonItem.GetContactInformationCount();
        //Assert
        Assert.Equal(count, 1);
    }



}