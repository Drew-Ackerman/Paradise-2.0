﻿
--Drop any existing tables
IF EXISTS (SELECT * FROM sys.tables WHERE NAME = N'Error')
	DROP TABLE Error;
IF EXISTS (SELECT * FROM sys.tables WHERE NAME = N'Admin')
	DROP TABLE Admin;
IF EXISTS (SELECT * FROM sys.tables WHERE NAME = N'Staff')
	DROP TABLE Staff;
IF EXISTS (SELECT * FROM sys.tables WHERE NAME = N'Donor')
	DROP TABLE Donor;
IF EXISTS (SELECT * FROM sys.tables WHERE NAME = N'Page')
	DROP TABLE Page;
IF EXISTS (SELECT * FROM sys.tables WHERE NAME = N'Event')
	DROP TABLE Event;
IF EXISTS (SELECT * FROM sys.tables WHERE NAME = N'Story')
	DROP TABLE Story;

---------------------------------------------------------------------
--Create all tables
---------------------------------------------------------------------
CREATE TABLE Staff
(staff_ID int IDENTITY(1,1) NOT NULL,
 firstName nvarchar(30) NOT NULL,
 lastName nvarchar(30) NOT NULL,
 jobTitle nvarchar(30) NOT NULL,
 position nvarchar(30) NOT NULL,
 foundation nvarchar(30) NOT NULL,
 email nvarchar(30) NULL,
 imageName nvarchar(30) NOT NULL,
 active bit NOT NULL);

 CREATE TABLE Admin
 (admin_ID int IDENTITY(1,1) NOT NULL,
  staff_ID int NOT NULL,
  userName nvarchar(20) NOT NULL,
  userPassword nvarchar(20) NOT NULL,
  superAdmin bit NOT NULL);

 CREATE TABLE Error
 (error_ID int IDENTITY(1,1) NOT NULL,
  errorDesc nvarchar(1000) NOT NULL,
  errorDate timestamp NOT NULL,
  admin_ID int NOT NULL);
 
 CREATE TABLE Donor
 (donor_ID int IDENTITY(1,1) NOT NULL,
  donorName nvarchar(50) NOT NULL,
  donorLevel nvarchar(50) NOT NULL,
  donorYear nvarchar(50) NOT NULL,
  phone nvarchar(11) NULL,
  email nvarchar(30) NULL,
  active bit NOT NULL);
  
 CREATE TABLE Page
 (page_ID int IDENTITY(1,1) NOT NULL,
  pageName nvarchar(20) NOT NULL,
  pageDesc nvarchar(50) NULL,
  content nvarchar(MAX) NOT NULL);
  
 CREATE TABLE Event
 (event_ID int IDENTITY(1,1) NOT NULL,
  eventName nvarchar(20) NOT NULL,
  eventDate date NOT NULL,
  eventTime nvarchar(15) NOT NULL,
  eventLocation nvarchar(50) NOT NULL,
  eventDetails nvarchar(200) NOT NULL,
  active bit NOT NULL);
  
  CREATE TABLE Story
  (story_ID int IDENTITY(1,1) NOT NULL,
   storyTitle nvarchar(50) NOT NULL,
   storyName nvarchar(50)  NOT NULL,
   storyContent nvarchar(max) NOT NULL,
   active bit NOT NULL);


 -----------------------------------------------------
--SET PK, AK, FK, and CK Constraints
------------------------------------------------------

--Staff Table
ALTER TABLE Staff
	ADD CONSTRAINT Pk_Staff
	PRIMARY KEY CLUSTERED (staff_ID);

--Admin Table
ALTER TABLE Admin
	ADD CONSTRAINT Pk_Admin 
	PRIMARY KEY CLUSTERED (admin_ID);

ALTER TABLE Admin
	ADD CONSTRAINT Fk_Admin_Staff
	FOREIGN KEY (staff_ID) REFERENCES Staff(staff_ID);
	
ALTER TABLE Admin
	ADD CONSTRAINT Ak_userName
	UNIQUE(userName);

--Error Table
ALTER TABLE Error
	ADD CONSTRAINT Pk_Error
	PRIMARY KEY CLUSTERED (error_ID);

ALTER TABLE Error
	ADD CONSTRAINT Fk_Error_Admin
	FOREIGN KEY (admin_ID) REFERENCES Admin(admin_ID);

--Donor Table
ALTER TABLE Donor
	ADD CONSTRAINT Pk_Donor
	PRIMARY KEY CLUSTERED (donor_ID);

ALTER TABLE Donor
	ADD CONSTRAINT CK_Donor_phone
	CHECK (phone LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]');

--Page Table
ALTER TABLE Page
	ADD CONSTRAINT Pk_Page
	PRIMARY KEY CLUSTERED (page_ID);
	
ALTER TABLE Page
	ADD CONSTRAINT Ak_pageName
	UNIQUE(pageName);
	
--Event TABLE
ALTER TABLE Event
	ADD CONSTRAINT Pk_Event
	PRIMARY KEY CLUSTERED (event_ID);
	
--Story TABLE
ALTER TABLE Story
	ADD CONSTRAINT Pk_Story
	PRIMARY KEY CLUSTERED (story_ID);


-------------------------
--Inserting Data
-------------------------
INSERT INTO Staff(firstName, lastName, jobTitle, position, foundation, email, imageName, active) VALUES('Super', 'Admin', 'Admin', 'Admin', 'Paradise', NULL, 'Paradise.png', 0);
INSERT INTO Staff(firstName, lastName, jobTitle, position, foundation, email, imageName, active) VALUES('Normal', 'Admin', 'Admin', 'Admin', 'Paradise', NULL, 'Paradise.png', 0);
INSERT INTO Staff(firstName, lastName, jobTitle, position, foundation, email, imageName, active) VALUES('Justine', 'Murray', 'Program Manager', 'President', 'A3 Utah', 'jmurray@yfut.org', 'staff_justine.jpg', 1);
INSERT INTO Staff(firstName, lastName, jobTitle, position, foundation, email, imageName, active) VALUES('Susan', 'McBride', 'Floor Staff Co-Lead', 'Executive Director', 'Youth Futures', 'kristen@yfut.org', 'staff_susan.jpg', 1);
INSERT INTO Staff(firstName, lastName, jobTitle, position, foundation, email, imageName, active) VALUES('Alyson', 'Deussen', 'Floor Staff Co-Lead', 'Ouelessebougou', 'Utah Alliance', 'aallred@yfut.org', 'staff_alyson.jpg', 1);

INSERT INTO Donor VALUES('MILLER FAMILY FOUNDATION LARRY H. & GAIL', 'platinum', '2015 & 2016', NULL, NULL, 1);
INSERT INTO Donor VALUES('IVY LANE PEDIATRICS', 'platinum', '2016', NULL, NULL, 1);
INSERT INTO Donor VALUES('SORENSON LEGACY FOUNDATION', 'platinum', '2015', NULL, NULL, 1);

INSERT INTO Admin VALUES('1', 'Paradise', 'Password', 1);
INSERT INTO Admin VALUES('2', 'Admin', 'Password', 0);

INSERT INTO Event VALUES('Event Sample', '12/25/2017', '8pm', 'Ogden UT, Weber state university', 'FUN!', 1);

INSERT INTO Story VALUES('Test', 'The story of a lowly tester', 'Much work, much sugar, little sleep.', 1);

INSERT INTO Page VALUES('Home', 'The home page.', '<div class="container-fluid"><div class="row main-header pad"><div class="col-xs-10 header-box"><br><div class="row pad"><p class="header-greeting">Hi</p></div><div class="sub-text-box row"><p class="col-xs-10">14 WARM BEDS. YOUTH 12-17. YOUR TEMPORARY HOME <span class="lightgreen-font">:) </span></p></div></div></div><div class="row lightgreen-background"><h6 class="white-font col-xs-12 text-center">Have questions? Send us a text! (801) 528-1214 </h6></div><section class="container-fluid vertical-pad-small"><div class="row"><div class="col-xs-12 text-center"><h4 class="">Services</h4><p class="side-pad-large">Our programming is divided into three main areas. Each program area has specific components to meet the needs of the youth in need.</p></div></div><div class="sections-row"><div class="boxes"><article class="col-xs-3 panel"><section class="panel-img-container"><img src="/Content/Image/house_icon.png" alt="House" class="panel-img"></section><section><p class="quicksand-light-32 panel-heading">Overnight Shelter</p></section><section><p class="mont-light-light-18 panel-summary">Located in the heart of downtown Ogden, Utah, Youth Futures provides emergency shelter, temporary residence and supportive services for runaway, homeless, unaccompanied and at-risk youth ages 12-17. The shelter is open 24 hours per day.</p></section><section class="panel-footer"><p class="quicksand-light-24-green"><a href="/Program/Program">Learn more &gt;</a></p></section></article><article class="col-xs-3 panel"><section class="panel-img-container"><img src="/Content/Image/door_icon.png" alt="Door" class="panel-img"></section><section><p class="quicksand-light-32 panel-heading">Drop-in Services</p></section><section><p class="mont-light-light-18 panel-summary">Available to any youth ages 12-18. Drop-in services allow for the youth to access food, clothing, hygiene items, laundry facilities, computer stations, and case management. Drop-in hours are 6:30 am to 8:00 pm every day of the week.</p></section><section class="panel-footer"><p class="quicksand-light-24-green"><a href="/Program/Edit?categoryid=Drop-In#dropin-anchor">Learn more &gt;</a></p></section></article><article class="col-xs-3 panel"><section class="panel-img-container"><img src="/Content/Image/van_icon.png" alt="Van" class="panel-img van-img"></section><section><p class="quicksand-light-32 panel-heading">Street Outreach</p></section><section><p class="mont-light-light-18 panel-summary">Youth Futures™ Street Outreach is conducted once per week and provides outreach and crisis services to youth in Ogden City, Utah.</p></section><section class="panel-footer"><p class="quicksand-light-24-green"><a href="/Program/Edit?categoryid=Street#street-anchor">Learn more &gt;</a></p></section></article></div></div></section><div class="row purpose"><h2 class="white-font col-xs-12">OUR</h2><h2 class="lightgreen-font col-xs-12">PURPOSE</h2><div class="col-xs-12"><div class="row purpose-detail"><div class="col-xs-12"><h6 class="white-font text-center">To provide unaccompanied, runaway and homeless youth with a safe and nurturing environment where they can develop the needed skills to become active, healthy, successful members of our future world.</h6></div></div><div class="row purpose-stats"><div class="col-xs-12"><p><span class="lightgreen-font">7,085&nbsp;</span>MEALS SERVED. <span class="lightgreen-font">511&nbsp;</span>DROP-IN SERVICES.</p><p><span class="lightgreen-font">245&nbsp;</span>STREET OUTREACH HOURS. <span class="lightgreen-font">64&nbsp;</span>SHELTERED YOUTH.</p></div></div></div></div><section class="row sponsor-bar"><h4 class="col-xs-12 text-center lightgreen_font">Our Sponsors</h4><!-- Effect to make sponsors scroll --><marquee behavior="scroll" direction="left"><div class="col-xs-12 col-lg-3"><div class="partner-logo"><img src="/Content/Image/mckaydee_hospital.png" alt="McKay Dee Hospital" class="mckay"></div></div><div class="col-xs-12 col-lg-3"><div class="partner-logo"><img src="/Content/Image/the_group_logo.png" alt="The Group Real Estate" class="thegroup"></div></div><div class="col-xs-12 col-lg-3"><div class="partner-logo"><img src="/Content/Image/giv_development.png" alt="GIV Development" class="giv"></div></div></marquee><!-- End effect to make sponsors scroll --></section><section class="row top-pad-small"><div class="col-xs-12 col-md-4 thumbnail"><img src="/Content/Image/volunteer_icon.png" alt="Volunteer"><h6>Apply to Volunteer</h6><p>Make your mark where it matters. Vestibulum rutrum quam vitae fringilla tincidunt. Suspendisse.</p><a href="/AboutUs/Edit?categoryid=Contact#contact-anchor">Volunteer Now &gt;</a></div><div class="col-xs-12 col-md-4 thumbnail"><img src="/Content/Image/girl_icon.png" alt="Youth Stories"><h6>Youth Stories</h6><p>Vestibulum rutrum quam vitae fringilla tincidunt. Suspendisse nec tortor urna.</p><a href="stories.html#">Read the Stories &gt;</a></div><div class="col-xs-12 col-md-4 thumbnail"><img src="/Content/Image/calendar_icon.png" alt="Events"><h6>Events</h6><p>Vestibulum rutrum quam vitae fringilla tincidunt. Suspendisse nec tortor urna.</p><a href="/Program/Edit?categoryid=Events#events-anchor">View All Events &gt;</a></div></section></div>');
INSERT INTO Page VALUES('Program', 'The program page.', '<div class="container-fluid"><div class="row program-main"><div class="pad col-xs-14 text"><br><br><br><br><h2 class="white-font">Youth Futures</h2><h2 class="h2style1 white-font">Programs</h2></div></div><div class="row lightgreen-background"><h6 class="white-font col-xs-12 text-center">Have questions? Send us a text! (801) 528-1214 </h6></div><div class="row pad"><div class="col-xs-12"><h2>Shelters</h2></div><div class="col-xs-12"><p class="mont_regular_22_darkgray">Information about the shelter will go in this section....SAMPLE TEXT: Located in the heart of downtown Ogden, Youth Futures opened Utah''s first homeless Residential Support Temporary Youth Shelter on February 20, 2015. Youth Futures provides shelter and drop-in services to all youth ages 12-17, and will not exclude any youth who falls within these age ranges, regardless of circumstance. We provide 14 temporary overnight shelter beds and day-time drop-in services to youth, as well as intensive case management to help youth become re-united with family or self-sufficiently contributing to our community. Weekly outreach efforts assist in building rapport with street youth, ensuring they receive food and other basic necessities and educating them about options to living in unsafe conditions. Youth are guided in a loving, supportive and productive way, encouraging their own personal path for a healthy future.</p></div></div><div class="row pad"><div class="col-xs-12 col-md-4" style="text-align:center"><img src="/Content/Image/history_kristen_scott.jpg" class="img-responsive center-block" title="Kristen and Scott" alt="Kristen and Scott"><p>SAMPLE IMAGE 1</p></div><div class="col-xs-12 col-md-4" style="text-align:center"><img src="/Content/Image/history_shelter.jpg" class="img-responsive img-responsive center-block" title="Shelter" alt="Shelter"><p>SAMPLE IMAGE 2</p></div><div class="col-xs-12 col-md-4" style="text-align:center"><img src="/Content/Image/history_kristen.jpg" class="img-responsive center-block" title="Kristen" alt="Kristen"><p>SAMPLE IMAGE 3</p></div></div><div class="row pad" id="dropin-anchor"><div class="col-xs-12"><h2>Drop-Ins</h2><h2>Welcome</h2><p class="">This are is for more information on Drop-Ins. SAMPLE TEXT:                During the first full year of operations (February 20, 2015-March 31, 2016),                our Residential Support Temporary Youth Shelter has:            </p></div></div><div class="row pad"><div class="col-xs-12 col-md-4"><img src="/Content/Image/history_meal.svg" class="img-responsive center-block" title="Meal" alt="Meal"><p>Served <span class="green">7,085</span>meals; 3 meals a day and 2 snacks for shelter and drop-in youth. Opened the resource room <span class="green">354</span>times with access to basic nec-essities including clothing, hygiene items, back packs, blankets, sleeping bags, basic medical supplies, etc.</p></div><div class="col-xs-12 col-md-4"><img src="/Content/Image/history_hand.svg" class="img-responsive center-block" title="Outreach Hand" alt="Hand"><p>Conducted more than <span class="green">245</span>street outreach hours and provided items from the resource room to street youth.</p></div><div class="col-xs-12 col-md-4"><img src="/Content/Image/house_icon2.png" class="img-responsive center-block" title="House" alt="House"><p>Provided <span class="green">1,535</span>shelter night stays and <span class="green">511</span>drop in services including case management, connections to health care, mental health care and group therapy, facilitation with other youth service providers, computer access, showers, laundry facilities, etc.</p></div></div><div class="row outreach_main_img" id="street-anchor"><div class="col-xs-12"><h2 class="white-font">Street</h2><h2>Outreach</h2></div></div><div class="row pad"><p class="col-xs-12">Street Outreach is designed to meet the clients where they are on the street to build rapport and encourage youth to access drop-in and shelter services. This program offers, case management, hygiene items, food, sleeping bags, and other essential items as needed. Street Outreach currently take place once per week on Wednesdays. The team visits the same Ogden, Utah locations every week:</p></div><div class="row side-pad-large"><div class="col-xs-12 col-md-6"><ul><li>Jefferson Park</li><li>Marchall White Center Park</li><li>Lorin Farr Skate Park</li></ul></div><div class="col-xs-12 col-md-6"><ul><li>Basketball Court at Bonneville Park</li><li>Under the Ogden River Bridge (sporadic)</li><li>Lantern House Homeless Shelter</li></ul></div></div><div class="row pad" id="events-anchor"><div class="col-xs-12"><h2>Events</h2><div id="eventsDiv"><div class="row donor_list"><div class="col-xs-4 donor_box plat"><p class="donor_name">EVENT SAMPLE</p><p>January 17, 2010 @ 6pm</p></div><div class="col-xs-4 donor_box plat"><p class="donor_name">EVENT SAMPLE 2</p><p>February 1st, 2018 at 6pm</p></div><div class="col-xs-4 donor_box plat"><p class="donor_name">Event SAMPLE</p><p>March 1st, 2018 at 7pm</p></div></div></div></div></div><div class="row side-pad" id="donor-anchor"><h2>Donors</h2></div><div class="row donor_subheader pad"><div class="col-xs-12 "><p class="title_plat">Platinum Level Donors</p></div></div><div id="platinumDonors"><div class="row donor_list"><div class="col-xs-4 donor_box plat"><p class="donor_name">MILLER FAMILY FOUNDATION LARRY H. &amp; GAIL</p><p>2015 &amp; 2016</p></div><div class="col-xs-4 donor_box plat"><p class="donor_name">IVY LANE PEDIATRICS</p><p>2016</p></div><div class="col-xs-4 donor_box plat"><p class="donor_name">SORENSON LEGACY FOUNDATION</p><p>2015</p></div><div class="col-xs-4 donor_box plat"><p class="donor_name">h</p><p>d</p></div><div class="col-xs-4 donor_box plat"><p class="donor_name">g</p><p>g</p></div><div class="col-xs-4 donor_box plat"><p class="donor_name">h</p><p>h</p></div></div></div><div class="row donor_subheader pad"><div class="col-xs-12 "><p class="title_gold">Gold Level Donors</p></div></div><div id="goldDonors"><div class="row donor_list"><div class="col-xs-4 donor_box gold"><p class="donor_name">Test</p><p>2017</p></div></div></div><div class="row donor_subheader pad"><div class="col-xs-12 "><p class="title_silver">Silver Level Donors</p></div></div><div id="silverDonors"><div class="row donor_list"><div class="col-xs-4 donor_box silver"><p class="donor_name">Test</p><p>2042</p></div></div></div><div class="row donor_subheader pad"><div class="col-xs-12 "><p class="title_bronze">Bronze Level Donors</p></div></div><div id="bronzeDonors"><div class="row donor_list"><div class="col-xs-4 donor_box bronze"><p class="donor_name">Test</p><p>1776</p></div></div></div><div class="row" id="donate-anchor"><div class="col-xs-6 pad"><h2 class="lightgreen_font">Donate</h2></div></div><div class="row donate_help"><div class="col-xs-12 pad"><h4>HOW CAN YOU HELP?</h4><p>Your generosity helps keep the doors open and the lights on, providing a sanctuary for those in need. Please consider a donation. </p></div></div><section class="row pad"><div class="col-xs-12 col-lg-4"><img src="/Content/Image/donate_dollar.svg" class="img-responsive center-block" title="Dollar" alt="Dollar"><p class="caption">Monetary donations are our biggest need right now. Recurring <span class="green-font"><a href="#">PayPal</a>&nbsp;</span>donations can be scheduled from our website, even $10 makes a difference. </p></div><div class="col-xs-12 col-lg-4 thumbnail"><img src="/Content/Image/shoppingcart_icon.png" class="img-responsive center-block" title="Cart" alt="Cart"><p class="caption">Donate through rewards programs: <span class="green-font"><a href="#">Amazon Smile,</a></span><span class="green-font"><a href="#">Smiths Community Rewards,</a></span>or <span class="green-font"><a href="#">United Way,</a></span><span class="green-font"><a href="#">Federal and State Employee contributions,</a></span><span class="green-font"><a href="#">LoveUTGiveUT</a></span></p></div><div class="col-xs-12 col-lg-4 thumbnail"><img src="/Content/Image/donate_hand.svg" class="img-responsive center-block" title="Hand" alt="Hand"><p class="caption">Donate your time as a volunteer to help with needs around the shelter! <span class="green"><a href="/AboutUs/Edit?categoryid=Contact#contact-anchor">Sign up here.</a></span></p></div></section><div class="row pad"><div class="col-xs-12"><h2>LIST OF NEEDS</h2></div></div><div class="row side-pad"><div class="col-xs-12 col-md-4"><p>MOST IMPORTANT NEEDS                (In order of priority)            </p><ul><li>Cash donations</li><li>Printer Paper</li><li>Canned meat &amp; Jerky</li><li>Scotch tape</li><li>Bus tokens or passes</li><li>Earbud Headphones</li><li>Cinch bags</li><li>Batteries</li><li>Sweat Pants</li><li>Pajama Bottoms </li><li>Sports bras</li><li>Trail mix individuals</li><li>Toilet Paper</li><li>Condoms</li><li>Tampons	</li><li>Carabiners</li><li>Paper plates and cups</li><li>Men''s and Women''s Underwear</li><li>Socks </li><li>Kleenex individuals</li><li>Undershirts, S M L XL</li><li>Garbage bags 30 Gallon</li><li>Garbage sacks small    bathroom</li><li>Lip balm</li><li>Ziploc bags, quart and gallon</li><li>Energy Bars</li><li>Heavy duty plastic storage bins that won''t melt if heated in shed</li></ul></div><div class="col-xs-12 col-md-4"><p>MISC. NEEDS</p><ul><li>Minivan</li><li>NEW Printer</li></ul><p>GIFT CARDS FOR</p><ul><li>Walmart</li><li>Fun things to do</li><li>Grocery store</li><li>Maverick</li><li>Restaurants</li><li>Movies</li><li>Bus passes or tokens</li><li>Phone minutes </li><li>Beauty salons/haircuts</li><li>For shoe stores</li><li>Lagoon passes</li></ul></div><div class="col-xs-12 col-md-4"><p>HOUSEHOLD FURNISHINGS NEEDS</p><ul><li>NEW pots and pans</li><li>New Couches</li></ul><p>VOLUNTEERS</p><ul><li>Mentors</li><li>Educators</li><li>Group activity facilitators</li><li>Meal preparers/providers</li><li>Tutors</li><li>Life skills trainers</li><li>Beauticians</li><li>Street Outreach Workers</li><li>Artists for classes</li><li>Yard work</li><li>Interior painters</li></ul><p>REPAIR NEEDS</p><ul><li>Concrete or pavers 1500 sq. feet</li><li>Cement sidewalk repair&amp; labor</li></ul></div></div></div>
');
INSERT INTO Page VALUES('AboutUs', 'The about us page.', '<div class="container-fluid"><div class="row hist_header"><div class="col-xs-14 pad"><h2 class="white-font">Youth Futures</h2><h2>History</h2></div></div><div class="row lightgreen-background"><h6 class="white-font col-xs-12 text-center">Have questions? Send us a text! (801) 528-1214 </h6></div><div class="row pad"><p class="col-xs-12">Located in the heart of downtown Ogden,            Youth Futures opened Utah''s first homeless Residential Support            Temporary Youth Shelter on February 20, 2015. Youth Futures provides            shelter and drop-in services to all youth ages 12-17,            and will not exclude any youth who falls within these age ranges,            regardless of circumstance.            We provide 14 temporary overnight shelter beds and day-time            drop-in services to youth, as well as intensive case management to            help youth become re-united with family or self-sufficiently            contributing to our community. Weekly outreach efforts assist            in building rapport with street youth, ensuring they receive            food and other basic necessities and educating them about            options to living in unsafe conditions.            Youth are guided in a loving, supportive and productive way,            encouraging their own personal path for a healthy future.        </p></div><div class="row pad"><div class="col-xs-12 col-md-4" style="text-align:center"><img src="/Content/Image/history_kristen_scott.jpg" class="img-responsive center-block" title="Kristen and Scott" alt="Kristen and Scott"><p>Kristen and Scott</p></div><div class="col-xs-12 col-md-4" style="text-align:center"><img src="/Content/Image/history_shelter.jpg" class="img-responsive center-block" title="Shelter" alt="Shelter"><p>The shelter home</p></div><div class="col-xs-12 col-md-4" style="text-align:center"><img src="/Content/Image/history_kristen.jpg" class="img-responsive center-block" title="Kristen" alt="Kristen"><p>Kristen</p></div></div><div class="row pad"><p class="col-xs-12">Youth Futures was founded by Kristen Mitchell and Scott Catuccio, who had been conceptually planning to provide shelter and case management services to youth for over six years. It was identified that there was a lack of shelter services for the estimated 5,000 youth who experience homelessness for at least one night a year in Utah, so Scott and Kristen began researching other states that provide shelter services to youth. It was quickly identified that the largest barrier to providing services to homeless youth in Utah was a law prohibiting the opening of shelter facilities for youth. </p></div><div class="row hist_2nd_back_img"><h4 class="col-xs-12 hist_2nd_back_img_text white-font">5,000 youth experience homelessness for at least one night a year in Utah</h4></div><section class="row pad"><p class="col-xs-12">During the 2014 Legislative Session, HB132 was passed, which allowed for rewriting the prohibitive law and drafting licensing procedures for residential support programs for temporary homeless youth shelter in Utah. Youth Futures and other homeless youth service providers participated in the rules writing process. The licensing rules enrolled on October 22, 2014, and the founders began to set-up the facility for licensing. Youth Futures received the first license for homeless youth shelter granted in the State of Utah under the new law. </p><br /><h6 class="col-xs-12 green-font text-center">During the first full year of operations (February 20, 2015-March 31, 2016), our Residential Support Temporary Youth Shelter has:</h6></section><section class="purpose-boxes side-pad-large"><div class="col-xs-3 panel"><img src="/Content/Image/history_meal.svg" title="Meal" alt="Meal" class="img-responsive center-block"><p>Served <span class="green">7,085</span>meals; 3 meals a day and 2 snacks for shelter and drop-in youth. Opened the resource room <span class="green">354</span>times with access to basic nec-essities including clothing, hygiene items, back packs, blankets, sleeping bags, basic medical supplies, etc.</p></div><div class="col-xs-3 panel"><img src="/Content/Image/history_hand.svg" title="Outreach Hand" alt="Hand" class="img-responsive center-block"><p>Conducted more than <span class="green">245</span>street outreach hours and provided items from the resource room to street youth.</p></div><div class="col-xs-3 panel"><img src="/Content/Image/house_icon2.png" title="House" alt="House" class="img-responsive center-block"><p>Provided <span class="green">1,535</span>shelter night stays and <span class="green">511</span>drop in services including case management, connections to health care, mental health care and group therapy, facilitation with other youth service providers, computer access, showers, laundry facilities, etc.</p></div></section><div class="row pad" id="board-anchor"><div class="col-xs-12 text-uppercase"><h2 class="lightgrey_font">Board of</h2><h2 class="lightgreen_font">Directors</h2></div></div><div class="row side-pad-large"><div class="col-xs-12 col-md-4 thumbnail"><p>SCOTT CATUCCIO<br />Board President                <br />President, A3 Utah<br /><a href="mailto:scott.catuccio@gmail.com">scott.catuccio@gmail.com</a></div><div class="col-xs-12 col-md-4"><p><span class="name">KRISTEN MITCHELL</span><br />Board Vice President                <br />Executive Director, Youth Futures<br /><a href="mailto:kristen@yfut.org">kristen@yfut.org</a></div><div class="col-xs-12 col-md-4"><p><span class="name">ALYSON DEUSSEN</span><br />Board Secretary                <br />Ouelessebougou Utah Alliance<br /><a href="mailto:alysondeussen@gmail.com">alysondeussen@gmail.com</a></div></div><div class="row pad" id="staff-anchor"><div class="col-xs-6 text"><h2 class="lightgrey_font">Our</h2><h2 class="lightgreen_font">Staff</h2></div></div><div class="row side-pad-large" id="staff"><div class="col-xs-12 col-md-4 thumbnail" style="text-align:center"><img src="/Content/Image/staff_justine.jpg" title="Justine Murray" alt="Justine Murray" class="img-responsive center-block"><p class="caption">JUSTINE MURRAY                <br />Program Manager, Youth Futures                <br />President, A3 Utah                <br /><a href="mailto:jmurray@yfut.org">jmurray@yfut.org</a></p></div><div class="col-xs-12 col-md-4 thumbnail" style="text-align:center"><img src="/Content/Image/staff_susan.jpg" title="Susan McBride" alt="Susan McBride" class="img-responsive center-block"><p class="caption">SUSAN MCBRIDE                <br />Floor Staff Co-Lead, Youth Futures                <br />Executive Director, Youth Futures                <br /><a href="mailto:kristen@yfut.org">kristen@yfut.org</a></p></div><div class="col-xs-12 col-md-4 thumbnail" style="text-align:center"><img src="/Content/Image/staff_alyson.jpg" title="Alyson Deussen" alt="Alyson Deussen" class="img-responsive center-block"><p class="caption">ALYSON DEUSSEN                <br />Floor Staff Co-Lead, Youth Futures                <br />Ouelessebougou Utah Alliance                <br /><a href="mailto:aallred@yfut.org">aallred@yfut.org</a></p></div></div><div class="row pad" id="media-anchor"><div class="col-xs-12"><h2 class="lightgreen_font">Media</h2></div></div><div class="row side-pad-large"><div class="col-xs-12 plat-font"><p class="title">America First Provides an ''Assist'' to Homeless Shelter</p><p class="time">03/18/2015 10:03 pm<p><a class="link" href="#">inShare</a></div><div class="row thumbnail verticle-pad-small"><img class="col-xs-3 col-md-4 pad" src="/Content/Image/media_check.jpg" title="America First Check" alt="Check"><p class="col-xs-12 col-md-6 pad">From left to right: Kristen Mitchell, executive director of Youth Futures Utah and Scott Tuccio, president of the Board of Directorsof Youth Futures Utah, stand with Nicole Cypers, public relations and social media manager for America First Credit Union, at the Weber State basketball game for a check presentation in the amount of $3,400 on Saturday, March 7 at Weber State University. </p></div><div class="row"><div class="col-xs-12"><p>OGDEN, Utah--America First Credit Union awarded Youth Futures Utah, a homeless shelter for youth, with $3,400 during the Weber State University basketball game. America First paid the organization $10 for each assist the Weber State University basketball team completed throughout the 2014 â€“ 2015 season. With 340 assists, the donation amounted to $3,400 in total for the newly-opened youth homeless organization, located in Ogden, Utah.</p><p>Youth Futures Utah is a 501(c)3 organization committed to the well-being of the youth of Utah. The mission of Youth Futures Utah is to provide shelter, support, and guidance to homeless, unaccompanied and runaway youth in Utah. Youth Futures connects each youth with food, housing and to build the skills needed to support a healthy future.</p></div></div></div><div class="row side-pad" id="contact-anchor"><div class="col-xs-12"><div class="row"><h2 class="lightgreen_font">CONTACT</h2></div><div class="row pad"><div class="col-xs-10 col-md-4 form-group"><p>CONTACT US</p><form method="post" name="myemailform" action="form-to-email.php"><input type="text" class="form-control" name="firstName" placeholder="First Name" maxlength="20"><span class="change" id="fName_change">Required!</span><br><input type="text" class="form-control" name="lastName" placeholder="Last Name" maxlength="20"><span class="change" id="lName_change">Required!</span><br><input type="tel" class="form-control" name="phone" placeholder="Telephone (Optional)" maxlength="20"><br><input type="email" class="form-control" name="email" placeholder="Email Address" maxlength="40"><span class="change" id="email_change">Required!</span><br><textarea name="message" class="form-control" placeholder="Message" maxlength="255"></textarea><span class="change" id="message_change">Required!</span><br><input type="submit" class="col-sm-6 btn btn-default" id="submit" value="Submit"></form></div><div class="col-xs-7 col-md-4"><p>CALL US</p><p>Tel: 801-528-1214</p><p>COME SEE US</p><p>60 Adams Ave. Ogden, UT<br>Ogden, Utah 84401</p></div><div class="col-xs-7 col-md-4"><p>SITE MAP</p><iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d1784.5861387467835!2d-111.96804305268928!3d41.215141436084046!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x87530f2689623957%3A0x49def70f5ee0a151!2sOgden+Youth+Futures!5e0!3m2!1sen!2sus!4v1492809010928" class="gmap" allowfullscreen></iframe></div></div></div></div></div>
');
INSERT INTO Page VALUES('YouthStories', 'The youth stories page.', '<div class="container-fluid"><div class="row ys_header"><div class="col-xs-14 pad"><h2 class="white-font">Youth</h2><h2>Stories</h2></div></div><div class="row lightgreen-background"><h6 class="white-font col-xs-12 text-center">Stories About Our Youth </h6></div><div class="row pad"><p class="col-xs-12">BLOG PAGE DESIGN ETC WILL GO IN THIS PORTION OF THE PAGE!<br>Located in the heart of downtown Ogden,            Youth Futures opened Utah''s first homeless Residential Support            Temporary Youth Shelter on February 20, 2015. Youth Futures provides            shelter and drop-in services to all youth ages 12-17,            and will not exclude any youth who falls within these age ranges,            regardless of circumstance.            We provide 14 temporary overnight shelter beds and day-time            drop-in services to youth, as well as intensive case management to            help youth become re-united with family or self-sufficiently            contributing to our community. Weekly outreach efforts assist            in building rapport with street youth, ensuring they receive            food and other basic necessities and educating them about            options to living in unsafe conditions.            Youth are guided in a loving, supportive and productive way,            encouraging their own personal path for a healthy future.        </p></div></div>
');
