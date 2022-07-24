# ProjeIT Pre Project! <br>
This project has been written for ProjeIT.<br>

### Description<br>
Simple task assignment and management system based on Eisenhower Matrix (Priority Matrix) using Asp .Net Core 6.0, Entity Framework Core Identity, Razor Pages and MsSQL in N-Layer Architecture with MVC.<br>

### Getting Started<br>
### Dependencies<br>
* .Net Core 6.0
* Entity Framework Core Identity<br>
* Microsoft SQL Server multi relational tables<br>
* Microsoft AspNetCore Mvc NewtonsoftJson - JSON Serializer<br>
### Installing<br>
* Download files and run solution file with IDE which supports Asp .Net Core 6.0.
* The project will create local database, make migrations, create Admin and Manager roles, create admin user.<br>
* Credentials for Admin role:<br>
  * Username: admin<br>
  * Password: Admin1.<br>
* Default credentials for Employee role: After creating Employee user, default username and password will be showed in a popup where right-bottom of the screen.<br>
  * Username: Name.Surname (e.g. for John Doe => John.Doe)<br>
  * Password: Name.surname1 (e.g. for John Doe => John.Doe1)<br>
### Version History
* 1.0<br>
  * Initial relase.<br>
### Acknowledgments:
Tools has been used:<br>
* ASP .Net Core 6.0 to prepare N-Tier Architecture with MVC.<br>
* Microsoft SQL Server to store datas in relational tables.<br>
* Entity Framework Core Indetity for database, account, authorization and role based authentication processes.<br>
* Bootstrap, jQuery, ajax with Razor Pages to Client Side Design.<br>
* FlatLab Template: https://github.com/torans/flatlab<br>

Text color in the tasks list is determines according to Eisenhower Matrix Color which is indicated below.<br>
* Important + Urgent = Red<br>
* Important + Not Urgent = Yellow<br>
* Not Important + Urgent = Orange<br>
* Not Important + Not Urgent = Green
