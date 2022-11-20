# Zoo-Blog wevb app
## Asp Net.Core MVC  web app using MSSQL as database and EF6

<p align="center" >Main catalog page where you can scroll and choose animel to explore and comment</p>
<p align="center">
  <img width="600"  src="https://user-images.githubusercontent.com/91791115/202900134-5fd41725-baf4-4177-ad3c-afe78cda40ce.jpg">
</p>

 ## About 

This Asp.NetCore web app demonstrates MVC pattern with one Layout view contains nav bar and renderend body with difrrent views and controllers.
I included one View-Component in order to keep the animels exploring divs more common between pages 
I also used boostrap style 

 ## Model

My model contain 3 objects : Category ,Animal and Comment.
I gave each of them severla propetries and fitting validation attributes .
I created two custom Vlidation Attributes: 
1. Birthdate to validate the animal is less than 150 years and was born in the current day or earlier
2. File validator to check wether if the file's content Type include the word "Image" and the size of the file limited to 10MB 

In order to generate the categories i made an Enum helper model which is not mapped in to the DataBase but i use to generate apropriate select tag

 ## View
 I've created several views for the controllers, one view component and 3 usefull partial view for layout styles and scripts and nav bar
 The nav bar is used to navigate between the diffrent views and actions
 
 <p align="center" >The app's nav bar</p>
<p align="center">
  <img width="700"  src="https://user-images.githubusercontent.com/91791115/202901618-8e64cce6-d52f-43da-82e9-15318e100dbb.jpg">
</p>


  ## Controllers
 This project contain 4 controllers :
 1. Home - displaying the two most commented animals
 2. Manager - Handling the CRUD operation on the animals data
 3. Catalog - view the animals in the blog and can sort them by category
 4. Animel Data - Explore the animals details and allow the user to leave a comment.
 
 <p align="center" >Hello world comment</p>
<p align="center">
  <img width="700"  src="https://user-images.githubusercontent.com/91791115/202901759-39421184-95c9-4a33-bd18-38543c79cc81.jpg">
</p>



