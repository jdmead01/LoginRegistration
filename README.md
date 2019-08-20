# C# Steps

Create a new project
```terminal
dotnet new mvc --no-https -o <ProjectName>
```
Open Project in Virtual Studio Code
```
code .
```
Navigate to the learn platform and follow the steps on the platform in ENTITY

Add dependencies:
```terminal
dotnet add package Pomelo.EntityFrameworkCore.MySql -v 2.2.0
```
Go to ***appsettings.json*** replace all info 
```javascript
{
    "DBInfo":
    {
        "Name": "MySQLconnect",
        "ConnectionString": "server=localhost;userid=root;password=root;port=3306;database=mydb;SslMode=None"
    }
}

```
Navigate to **startup.cs** in ConfigureServices Method

```
services.AddDbContext<MyContext>(options => options.UseMySql(Configuration["DBInfo:ConnectionString"]));
```
Add the following to the top of the startup.cs file 

```
using Microsoft.Extensions.Configuration;
public class Startup
```

 - [ ] Within your models folder create a 'context' file inside the .Models namespace

```javascript
using Microsoft.EntityFrameworkCore;

  

namespace LoginRegistration.Models
{
	public class MyContext  :  DbContext
	{
	public  MyContext(DbContextOptions options)  : base(options)  {  }

	// add DB sets here

	}
}
```

 - [ ] in your startup make sure you add the following imports: 
	 - [ ] ``using Microsoft.EntityFrameworkCore;``
	 - [ ] ```using <ProjectName>.Models;```

# Create Model Files

Model Files are located within the "Models" Folder and namespace 

 - [ ] Add all the following to top of all your Models File
	```javascript
	using Microsoft.AspNetCore.Mvc; 
	using System; using System.Collections.Generic; 
	using System.ComponentModel.DataAnnotations; 
	using System.ComponentModel.DataAnnotations.Schema;
	```
 - [ ] Add the namespace to each file at the top
	 - [ ] example: ```namespace LoginRegistration.Models```
	 - [ ] Make the class on the same file named what the table would be. Example if you're making a Login table ```public class Login```
	 - [ ] Next step add the primary ```[key]```  and all appropriate fields for the database (*this is also the space you will include validations*).

yourModel.cs
```
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginRegistration.Models{

	public class Register{

		[Key]
		public int registerId {get; set;}
		
		[Required]
		[MinLength(2, ErrorMessage="Your Name Must contain at least 2 		Characters!")]
		public string firstName {get; set;}

		[Required]
		[MinLength(2, ErrorMessage="Your First Name Must contain at least 2 Characters!")]
		public string lastName {get; set;}

		[Required]
		[EmailAddress]
		public string email {get; set;}

		[Required]
		[MinLength(2)]
		public string password {get; set;}
		
		[NotMapped]
		[Compare("password", ErrorMessage="Passwords do not match!")]
		public string confirmPassword {get; set;}

	  public  DateTime createdAt {get; set;}  =  DateTime.Now;
	  public  DateTime updatedAt{get; set;}  =  DateTime.Now;
	}
}
```

## MyContext.cs

Once you're sure you have what you need in your models (aka db structure) navigate to your context file which is located in your models folder. 

 - [ ] Add  a ```public DbSet<ModelClassName> {get; set;}```
	 - [ ] The names of these sets will always be PLURAL of your class name and will be lowercased. 
	 example:
	 
	 ```public  DbSet<Login> logins {get; set;}```

## Migrations

Before going back to your controller file. Migrate what you have thus far using the following commands in the terminal 
```terminal 
dotnet ef migrations add MigrationName
dotnet ef database update
````
## Test database using the terminal. 
In your terminal open your MySql Database(CLI). 
```mysql -u root -p```

You will be asked for password 

password:
```root```

See all databases:
```mysql> show databases;```

Switch to applicable database:
```use <databaseName>;```

See that the tables were all populated with the proper fieldTypes do the following:
```describe <tableName>;```

## Controllers Folder 
**HomeController.cs**
After you are sure that your context is hooked up to your database inject it as a depency to your controller 

```javascript
using Microsoft.EntityFrameworkCore;
using YourNamespace.Models;
using System.Linq;
// Other using statements
namespace MyProject.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
     
        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
     
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<User> AllUsers = dbContext.Users.ToList();
            
            return View();
        }
    }
 }
 ```
return type ```IActionResult```: This lets us either render OR redirect in any method from our controller. 

return ```View();```: This is used to render a page directly and can also be used to pass in a model directly 

return ```RedirectToAction();```: This is used to redirect to a  **function name** which will, in turn, run its own processing

From here on out you will be developing both the controller and any views you need. 

 1. On the controller side you will be querying the database to pass in LISTS of DATA as Models to your views 
 2. On the Views Side, for example, we will be creating forms ```"asp-for"``` for the appropriate fields within your Models.
 3. make sure your HomeController.cs file has the following at the top of the page ```using Microsoft.AspNetCore.Http;```
 4. Make sure your "Home" page "Index" has the route of ``[HttpGet("")]``

## Example: Posting Forms

 - [ ] First: Go to your View and make a form with all the fields you need to collect based on your database(model) and your wire frame. 

Example:

       <form asp-action="Create" asp-controller="Home" method="post">
            <span asp-validation-for="Username"></span>
            <label asp-for="Username"></label>
            <input asp-for="Username">
            
            <span asp-validation-for="Email"></span>
            <label asp-for="Email"></label>
            <input asp-for="Email">
            <span asp-validation-for="Password"></span>
            <label asp-for="Password"></label>
            <input asp-for="Password">
            <input value="Add User" type="submit">    
        </form>

 - [ ] Next make a route/function for the form's action to go to. 
	 - [ ] Then add this 'if' statement in your HomeController.cs file within the function you created above:

```javascript
if  (ModelState.IsValid){
	return  RedirectToAction("Success");
}
return  View("Index");
```
if your form is successful return a redirect. If your form is not successful you want the return/render a View to show (list) validations.

> Redirecting clears any validation messages which is why we render instead. 

## Test form 
Navigate to where the form you are testing lives (INDEX etc.. ). 

 1. Populate the form with data and submit, see if the correct View is returned based on your function. Meaning, are we directed to the proper place if the data is successful or redirected elsewhere if the data is NOT successfully added to the database. 
 2. To ensure the entry exists navigate to the MySQL CLI in your shell (terminal) and type ```select * from <tableName>```

# ViewModel Wrapper (Two forms one page)

create the **indexViewModel.cs** within your **Model folder**
```javascript
public class IndexViewModel
{
    public Register NewResgister {get;set;}
    public Login NewLogin {get;set;}
}
```
Navigate to the View file that you wish to display the second form on (Index.cshtml) add the form. 

example: 
```javascript
<form  asp-action="Login"  asp-controller="Home"  method="post">
<label  for="">Email:</label>
<input  type="text"  asp-for="newLogin.email"  /><br>
<label  for="">Password</label>
<input  type="text"  asp-for="newLogin.password"  /><br>
<button  type="submit">Sign In Whore</button>
</form>
```
Now go to the top of the file and add your View Model Wrapper as the model; because you cannot have more than 1 model per View (this allows you to have a number of them) (**at the top**)

```@model LoginRegistration.Models.IndexViewModel;```

> referencing the above code block you can see where the ```asp-for`` has been changed from previous examples by adding the reference or pointer name that was declared in the IndexViewModel.cs file

# Display Validations
