<h1> ShopsRUs Code Practises </h1>
<b> Author:  Ali CAKIL, alicakil78@gmail.com </b>
<hr/>

<b> Description </b> <br>
This is a Resftful API service taht showing code practises based on requirepments descripbed in UserStory document file. 


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
Entity Framework Core<br>
Repository Patern<br>
ilogger<br>
SOLID priciples<br>
Data Annooations (Display, Range, MaxLength)<br>
Regular expressions<br>


 <h3> Testing the Project   </h3>
 <hr>
 -Solution have UnitTest project<br>
	- Unit Tests (not fully covered yet..)<br>
	- Performance Test<br>
	- Integration Test<br>
   

 -Injection & Penetration Tests
 -Moq
 -Performance Test
 -Stress Test (multi thread)



<h3> Future Plans </h3>
<hr>
 - Following paterns will be added: UnitOfWork, SingleTon, Builder <br>
 - login, sign up (Authentication)<br>
 - password reset (send email)<br>
 - Excel Export<br>
 - Ref~Out, Overloading, method overriding (such as string), Extensions, Destructor (for garbage collector)<br>