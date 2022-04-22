<h1> ShopsRUs Code Practises </h1>
<b> Author:  Ali CAKIL, alicakil78@gmail.com </b>
<hr/>

<b> Description </b> <br>
This is a Resftful API service for showing some code practises based on requirepments descripbed in *.doc file. 


<h3> Usage </h3>
<hr>
Go to Api/Models/Context.cs file, provide a valid connection string such as local sql server for your machine.
Build and Run the project. (I recomend Visiaul Studio 2022 Community)





<h3> FEATURES </h3>
-Customer
	-To get customer data and create customer
-Discount
	- able to insert update or get invoice data
	- Discounting algoritm based on rules as described in the UserStory document.



<h3> Used Technogies </h3>
<hr>

Restful API
Entity Framework Core
Repository Patern
ilogger
SOLID priciples



Model Design
 -Data Annooations (Display, Range, MaxLength)
 -Regular expressions (password etc)


 <h3> Testing the Project   </h3>
 <hr>
 -Solution have UnitTest project
	- Unit Tests (not fully covered yet..)
	- Performance Test
	- Integration Test
   

 -Injection & Penetration Tests
 -ddos attact test (high requests per sec from a same source)
 -Moq
 -Performance Test
 -Stress Test (multi thread)



<h3> Future Plans </h3>
<hr>
 - Following paterns will be added: UnitOfWork, SingleTon, Builder 
 - login, sign up (Authentication)
 - password reset (send email)
 - Excel Export
 - Ref~Out, Overloading, method overriding (such as string), Extensions, Destructor (for garbage collector)