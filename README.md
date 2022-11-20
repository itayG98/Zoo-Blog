# Zoo-Blog wevb app
## Asp Net.Core MVC  web app using MSSQL as database and EF6

<p align="center" >Main catalog page where you can scroll and choose animel to explore and comment</p>
<p align="center">
  <img width="600"  src="https://user-images.githubusercontent.com/91791115/202901910-3d779ee1-089e-45d9-8590-581d5680a814.jpg">
</p>


 ## About 

This Asp.NetCore web app demonstrates MVC pattern with one Layout view contains nav bar and renderend body with difrrent views and controllers.
I included one View-Component in order to keep the animels exploring divs more common between pages 
I also used boostrap style 

 ## Model
 
<p align="center" >MSSQLA Diagram</p>
<p align="center">
  <img width="600"  src="https://user-images.githubusercontent.com/91791115/202903162-5d404a42-0fa5-4d17-b041-de35c044f036.jpg)">

My model contain 3 objects : Category ,Animal and Comment.
I gave each of them severla propetries and fitting validation attributes including Regex patterns, Data type custom messege error etc..
I created two custom Vlidation Attributes: 
1. Birthdate to validate the animal is less than 150 years and was born in the current day or earlier
2. File validator to check wether if the file's content Type include the word "Image" and the size of the file limited to 10MB 

https://github.com/itayG98/Zoo-Blog/blob/0a9486196c9483055d269c8b58c11fc163ce114d/Model/Models/Attributes/ImageFileValidationAttribute.cs#L11-L26

In order to generate the categories i made an Enum helper model which is not mapped in to the DataBase but i use to generate apropriate select tag

The model project contains also the data access layer the repository and one image formatter service.
which help me to save the images files as bytes array and generate the image back on the client side


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

## Unit Testing
This web app solution includes one class of testing for the repository layer checking and validating the who ReposiroeyBase class for both sunc and async method .
 
  <p align="center" > Test example :</p>
<p align="center">
 https://github.com/itayG98/Zoo-Blog/blob/0a9486196c9483055d269c8b58c11fc163ce114d/ModelTests/RepositoryTests.cs#L317-L335
</p>


