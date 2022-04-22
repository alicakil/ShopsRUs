<h1> ShopsRUs Code Practises </h1>
<b> Author:  Ali CAKIL
alicakil78@gmail.com alicakil.com 
 <a target='_blank' href='https://alicakil.com'> alicakil.com</a>
</b>
<hr/>

<b> Description </b> <br>
This is a Resftful API service taht showing code practises based on requirepments descripbed in  <a target='_blank' href='https://github.com/alicakil/ShopsRUs/blob/master/Api/UserStory.docx'> UserStory document.</a> document file. 


<h3> Usage </h3>
<hr>
Go to Api/Models/Context.cs file, provide a valid connection string such as local sql server for your machine.<br>
Build and Run the project. (I recomend Visiaul Studio 2022 Community)





<h3> FEATURES </h3>
-Customer<br>
	-To get customer data and create customer<br>
-Discount<br>
	- able to insert update or get invoice data<br>
	- Discounting algoritm based on rules as described in the <a target='_blank' href='https://github.com/alicakil/ShopsRUs/blob/master/Api/UserStory.docx'> UserStory document.</a><br>
	- Download Invoice list as xls file


<h3> Used Technologies </h3>
<hr>

Restful API <br>
Entity Framework Core<br>
Model Controller ViewModel structure <br>
Repository Patern<br>
ilogger<br>
SOLID priciples<br>
Data Annooations (Display, Range, MaxLength)<br>
Regular expressions<br>
Moq<br>
ClosedXML (Excel file download) <br>

 <h3> Testing the Project   </h3>
 <hr>
 -<b>Solution a have UnitTest project</b><br>
	- Unit Tests (not fully covered yet..)<br>
	- Performance Test<br>
	- Integration Test<br>
   

<h3> Future Plans </h3>
<hr>
 - Following paterns will be added: UnitOfWork, SingleTon, Builder <br>
 - login, sign up (Authentication)<br>
 - password reset (send email)<br>
 - Ref~Out, Overloading, method overriding (such as string), Extensions, Destructor (for garbage collector)<br>