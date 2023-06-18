## About The Project 

### Shoeperstar is a footwear eCommerce site.

It's **core features** include the following.
<br>&nbsp;&nbsp; - Home page showing the trending/popular products 
<br>&nbsp;&nbsp; - Details page which shows all the details for the specific product
<br>&nbsp;&nbsp; - View All products page with filter and search functionality
<br>&nbsp;&nbsp; - Cart page with email notification for the order during checkout
<br>&nbsp;&nbsp; - Customer orders page
<br>&nbsp;&nbsp; - User Login and Registration
<br>&nbsp;&nbsp; - Administrator management of all products and items
<br>&nbsp;&nbsp; - Cancellation of expired orders (Admin)
<br>&nbsp;&nbsp; - Order status tracking

For a full demo of all the features, [click here](https://www.youtube.com/watch?v=PpxUL8YlaX8) to watch the video.




## Installation and Usage

Download the project and open package manager console and run the following command.

```
update-database
```


Use the following accounts when logging in or you may register an account:
<br>&nbsp; **Default User account**:
 <br>&nbsp;&nbsp; Email: user@shoeperstar.com
  <br>&nbsp;&nbsp; Password: Coding@1234?
  <br><br>&nbsp; **Admin account**:
 <br>&nbsp;&nbsp; Email: admin@shoeperstar.com
  <br>&nbsp;&nbsp; Password: Coding@1234?


**Email Functionality**
<br>&nbsp; - To make the email functionality during checkout work, read the following [article on how to enable less secure apps in gmail.](https://wpmailsmtp.com/gmail-less-secure-apps/)

&nbsp; After setting up less secure apps on your gmail account, open the `appsettings.json` file.

```
"EmailConfiguration": {
    "From": "youremail@gmail.com",
    "SmtpServer": "smtp.gmail.com",
    "Port": 465,
    "Username": "youremail@gmail.com",
    "Password": "your password"
  }
```

Modify `"From"` and `"Username"` and replace with your gmail account. 

For `"Password"` field, input the gmail generated app password.


## Ongoing and Future Improvements
* Add Unit Tests
* Create Product rating and review system
* Create Inventory page
* Create Sales Tracking page

## Techologies used

<img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white">

<img src="https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white">

<img src="https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white">

<img src="https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white">

<img src="https://img.shields.io/badge/MySQL-005C84?style=for-the-badge&logo=mysql&logoColor=white">
