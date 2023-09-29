
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
  Overview and Feature Outline
</h2>
  <div>
    <ul style="list-style: none">
      <li>Login and Authentication</li>
      <li>User Profiles (Technician and Admin)</li>
      <li>Statistical Data and Overview</li>
      <li>Navigation Tabs/Pages</li>
      <li>Overall Statistics of Lube Technician</li>
      <li>Repair Order (RO) Information Submission Form</li>
      <li>Individual Technician Statistics Tracking</li>
      <li>Data Entry Search Function</li>
      <li>Technician Creation and Updates</li>
      <li>Vehicle Service Tracking Clock</li>
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
    <h3></h3>

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
    <h3></h3>

  <h2>Overall Statistics of Lube Technician</h2>
    <img src="https://github.com/JackArmstrong937/SubaruTracking/assets/101604941/c991c80b-2456-4692-98c4-43ebebea8bf8"/>
    <h2></h2>
    <div>
      The highlighted area is the main table that displays the overall combined statistical data of all lube technicians for that respective pay period, which can be changed using the pay period drop-down box at the top left of the page. The data shown in the table gives an overview of each tech's performance for that pay period. For a better understanding of the data shown, allow me to explain what each column of data represents. Total RO hours represents the combined time taken to complete repairs on all cars worked on by that tech. This timestamp is from the time the car was checked in to the time the car was checked out. This does not specifically represent how much time the tech actually spent on making the repairs, rather it is the whole servicing process from start to finish. Total expected hours is the amount of time each car repair should take. Each repair type has a fixed time it should take to complete that job. For example, a simple oil change has an expected time stamp of ~0.5 hours. The total expected hours is simply the addition of all timestamps for  each repair/type of repair for all serviced vehicles. The efficiency is then calculated based on the difference between Total RO Hours and Total Expected Hours. The closer the Total RO Hours is to the Total Expected Hours, the closer the efficiency percentage is to 100%. Finally, the RO-Clock Efficiency represents how much time the lube tech actually spent working on the vehicle, without respect to the overall time the car spent in the servicing process. Since a car could sit in the service drive for 10-30 or more minutes until being worked on, finding out how long the repairs actually took can be determined with the RO-Clock data variable. However, this RO-Clock data variable is dependent upon whether or not the lube tech used the RO-Clock feature, which will be explained later on, thus it is an option data point.  
    </div>
    <h3></h3>
     <img src="https://github.com/JackArmstrong937/SubaruTracking/assets/101604941/dae1e3cf-34fe-4325-8416-ca5e8251387b"/>
     <h2></h2>
     <div>
       In the Combined Efficiencies box, all efficiencies of all techs are shown. Both the open-close and RO-Clock efficiencies are calculated to have a better overall view of all the lube techs and their performance for that pay period. 
     </div>
      <h3></h3>
     <h2>Repair Order (RO) Information Submission Form</h2>
     <img src="https://github.com/JackArmstrong937/SubaruTracking/assets/101604941/b6daf226-4031-4283-8c3c-21b9dcb61179"/>
     <h2></h2>
     <div>
       The RO Entry form as seen above is the main form used by the admin user to input entries into the database. All the admin user needs to do is popluate the boxes, select the corresponding technician from the drop-down for that specific data entry, pick the date of completing that vehicle service of the RO, then hit submit. This form is also equipped with a date-picker, automaticaly defaulting pay period population, an invalid/empty input validation checks which prevents the user from submitting an unfinished form. The pay period drop down in the form is linked to the pay period selected in the other drop-down at the top left of this page. If the top left pay period drop-down is changed, the one in the form will change, however, if the one in the form is changed, the other drop-down is uneffected. In addition, the pay period drop-down will default to whatever the current pay period is, that way the admin user doesn't have to select the right pay period every time when using the form.
     </div>
      <h3></h3>
     <img src="https://github.com/JackArmstrong937/SubaruTracking/assets/101604941/97144a05-209f-4a78-b32e-ca00a90eac80"/>
     <h2></h2>
     <div>
       The datepicker in this form is also configured so that only dates within that respective pay period can be selected. All out-of-bounds dates will be greyed out and unable to be picked. This feature is just another form of form consistency and error handling, preventing from entries having incorrect data accidently inputed. 
      <h3></h3>
     </div>
     <h2>Individual Technician Statistics Tracking</h2>
     <img src="https://github.com/JackArmstrong937/SubaruTracking/assets/101604941/8670593e-906d-4d52-afa5-4b458bc7547c"/>
     <h2></h2>
     <div>
       For a more detailed report of an individual lube technician, you can either click on the name of a tech on the first/main page or click the Entires tab at the top. Once on the Entries tab, you can select pay period, the desired tech you want to look at, or even search by a specific RO number, which is assigned to every customer vehicle going through service. On this page, you can see a more detailed data set for each individual car that tech worked on rather than a combined view. Additional information can be seen for each car worked on such as RO number, date worked on, individual open-close time, and RO-Clock efficiency time. You will also notice that some of the percentages and data columns are red and green. If percentages or certain data values fall below a desired proficiency level, the entry will be flagged red, signifying that something is either wrong in the data entry, or simply the tech did not meet performance expectations. On the other hand, if the data entry values are green, this signifies that the tech had exceptional performance levels on that particular car. The goal is to have mostly green values highlighted and figure out how to improve so that little to no red entries are seen.  
      <h3></h3>
     </div>
     <img src="https://github.com/JackArmstrong937/SubaruTracking/assets/101604941/5601f9c7-7ad3-44d8-9e67-7a60b4e62418"/>
     <h2></h2>
     <div>  
     At the right of each data entry is an edit button. When clicked, an edit modal as seen above will pop up, allowing the user to edit any information about that specific data entry. The modal will be populated with all data respective to which edit button was clicked but allowing any of the values to be changed. Then, once the saved button is clicked, that data entry will then be updated to whatever data values were entered/altered and the modal closed. There is also an option for deleting that entry; however, it is a soft delete, meaning that the entry still exists in the database but isn't used in any calculations. 
     <h3></h3>
     </div>
     <img src="https://github.com/JackArmstrong937/SubaruTracking/assets/101604941/43b0ae54-89a3-4332-9f2d-6412cc881710"/>
     <h2></h2>
     There is also a feature on this page that allows the user to search an RO entry by RO number. In the image above, the RO being searched is 500. Since RO numbers rotate and eventually there will be multiple entries with the same RO number, this search feature will bring up all RO entries matching the searched RO number, pulling up all needed information. This search feature makes it easier to find a specific RO entry for whatever reason. 
     <h3></h3>
<h2>Technician Creation and Updates</h2>
     <img src="https://github.com/JackArmstrong937/SubaruTracking/assets/101604941/1fb69358-3844-4c3f-b976-f870bc11aab3"/>
     <h2></h2>
     <div>
       On the Techs page, all lube technicians, both active and inactive, are shown with their respective information such as name (first and last), tech number, which store they work at, and if they are activily working at that respective store or not. In the image above, the table below the Show Inactive Lube Techs button will show up when the button is clicked. By default, this table is hidden. To the right of the page, there is a form used for adding new technicians. This form is also used to update technicians when the edit button next to the respective tech name is clicked. 
     </div>
     

     
     
     
     
     
     


  
  
  

 
