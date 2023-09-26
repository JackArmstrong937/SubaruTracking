
<h1>
  <img src="https://github.com/JackArmstrong937/SubaruTracking/blob/main/wwwroot/Includes/Images/SubaruLogo.jpg" width="70" height="50" style="border-radius:50%"/> Subaru Efficiency Tracking Website 
</h1> 
This Website was designed and developed to be used as a means of more efficiently tracking customer vehicle service times and other Subaru Lube Technician-related statistics/performance.
<h2>Tech Stack Used</h2>
<div>
  <ul style="list-style: none">
    <li>Frameworks - MVC & .NET5</li>
    <li>Backend - C#</li>
    <li>Frontend - HTML / CSS / Bootstrap / Javascript / JQuery</li>
  </ul>
</div>

<h2>
  Overview and Features
</h2>
  <div>
    <ul style="list-style: none">
      <li>Login and Authentication</li>
      <li>User Profiles (Technician and Admin)</li>
      <li>Navigation Tabs/Pages</li>
      <li>Overall Statistics of Lube Technician</li>
      <li>Individual Technician Statistics Tracking</li>
      <li>Vehicle Service Tracking Clock</li>
      <li>Repair Order (RO) Information Submission Form</li>
      <li>Statistical Data and Overview</li>
      <li>Data Entry Search Function</li>
      <li>Pay Period Division</li>
    </ul>
  </div>

  <h2>Login and Authentication</h2>
    <img src="https://github.com/JackArmstrong937/SubaruTracking/assets/101604941/1c3c1759-5b32-4781-b234-9f41edfee84a"/>
    <h2></h2>
    <div>
      When first loading the website, the user is required to put in the correct login credentials in order to access the site. In this application, there are two users: a lube technician and an admin. The lube technician is the basic user who has restricted access to
      certain functions such as the Repair Order (RO) information submission form and permission to change and update technician data. The admin has full access to the website's features and functions.
    <div>
    <h2></h2>

  <h2>Navigation Tabs/Pages</h2>
    <img src="https://github.com/JackArmstrong937/SubaruTracking/assets/101604941/6e9d91d9-60b0-4336-97d3-267d1e86891d"/>
    <h2></h2>
    <div> 
      Upon successful login, the user will be indexed to this main page which serves as the main overview of statistical data and location for the RO information submission form used by the admin. At the top right highlighted in red is the website navigation/pages.
      The Efficiencies tab directs back to the current page shown above, and the Entries tab directs to another page that provides a more detailed data set of RO entries and their respective lube technician details. The Techs tab shows all lube technician information such as name, tech 
      number, etc.
      On the Techs page, lube technician information can be changed/updated and new techs can be inputted. Next, the RO-Clock tab directs to a page that has a stopwatch used for tracking the time spent working on a vehicle. Finally, the logout tab simply logs the user out, redirects them 
      to the login page, and requires them to input the correct credentials to log back in.
    <div>
    <h2></h2>

  <h2>Overall Statistics of Lube Technician</h2>
    <img src="https://github.com/JackArmstrong937/SubaruTracking/assets/101604941/c991c80b-2456-4692-98c4-43ebebea8bf8"/>
    <h2></h2>
    <div>
      The highlighted area is the main table that displays the overall combined statistical data of all lube technicians for that respective pay period, which can be changed using the pay period drop-down box at the top left of the page. The data shown in the table gives an overview of each tech's performance for that pay period. For a better understanding of the data shown, allow me to explain what each column of data represents. Total RO hours represents the combined time taken to complete repairs on all cars worked on by that tech. This timestamp is from the time the car was checked in to the time the car was checked out. This does not specifically represent how much time the tech actually spent on making the repairs, rather it is the whole servicing process from start to finish. Total expected hours is the amount of time each car repair should take. Each repair type has a fixed time it should take to complete that job. For example, a simple oil change has an expected time stamp of ~0.5 hours. The total expected hours is simply the addition of all timestamps for  each repair/type of repair for all serviced vehicles. The efficiency is then calculated based on the difference between Total RO Hours and Total Expected Hours. The closer the Total RO Hours is to the Total Expected Hours, the closer the efficiency percentage is to 100%. Finally, the RO-Clock Efficiency represents how much time the lube tech actually spent working on the vehicle, without respect to the overall time the car spent in the servicing process. Since a car could sit in the service drive for 10-30 or more minutes until being worked on, finding out how long the repairs actually took can be determined with the RO-Clock data variable. However, this RO-Clock data variable is dependent upon whether or not the lube tech used the RO-Clock feature, which will be explained later on, thus it is an option data point.  
    </div>
    <h2></h2>
     <img src="https://github.com/JackArmstrong937/SubaruTracking/assets/101604941/dae1e3cf-34fe-4325-8416-ca5e8251387b"/>
     <h2></h2>
     <div>
       In the Combined Efficiencies box, all efficiencies of all techs are shown. Both the open-close and RO-Clock efficiencies are calculated to have a better overall view of the lube techs and their performance for that pay period. 
     </div>


  
  
  

 
