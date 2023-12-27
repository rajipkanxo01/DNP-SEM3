using System.Diagnostics;
using BlazorServerApp.Pages;
using HTTPClients.ClientInterfaces;
using HTTPClients.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace TestProject;

using Bunit;
using Xunit;

public class UnitTest1
{
    

    [Fact]
    public void Test1()
    {
    }

    [Fact]
    public void ErrorMessageWhenFullNameIsNull()
    {
        TestContext context = new TestContext();
        context.Services.AddSingleton<IUserService>(new UserService(new HttpClient()));

        var renderedComponent = context.RenderComponent<RegisterUser>();
        
        renderedComponent.Instance.name = "";
        renderedComponent.Instance.GoToStep2();
        
        
        Assert.Equal("Error: Full Name Cannot Be Empty", renderedComponent.Instance.errorLabel);
    } 
    
   
}