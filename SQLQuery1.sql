
CREATE TABLE ActivityTypes (
  ActivityTypeId decimal(18,0) NOT NULL,
  ActivityName varchar(50) DEFAULT NULL
);

-- --------------------------------------------------------

--
-- Table structure for table Addresses
--

CREATE TABLE Addresses (
  AddressId int NOT NULL,
  UserId varchar(50) NOT NULL,
  StreetAddress varchar(50) DEFAULT NULL,
  GoogleMapsLocation text DEFAULT NULL,
  LocationLabel varchar(50) DEFAULT NULL,
  Status char(1) NOT NULL
);

-- --------------------------------------------------------

--
-- Table structure for table Categories
--

CREATE TABLE Categories (
  CategoryId int NOT NULL,
  CategoryName varchar(50) DEFAULT NULL,
  ParentCategoryId int DEFAULT NULL,
  OutletId decimal(18,0) DEFAULT NULL
);

-- --------------------------------------------------------

--
-- Table structure for table Crashlog
--

CREATE TABLE Crashlog (
  CrashLogId int NOT NULL,
  ClassName varchar(20) NOT NULL,
  MethodName varchar(20) NOT NULL,
  ErrorMessage text DEFAULT NULL,
  ErrorInner text DEFAULT NULL,
  Data text DEFAULT NULL,
  TimeStamp datetime NOT NULL
);

-- --------------------------------------------------------

--
-- Table structure for table EmailAddresses
--

CREATE TABLE EmailAddresses (
  EMailPk int NOT NULL,
  UserId varchar(50) NOT NULL,
  IsPrimaryMail char(1) DEFAULT NULL,
  Status char(1) DEFAULT NULL,
  EmailAddress text DEFAULT NULL
);

--
-- Dumping data for table EmailAddresses
--

INSERT INTO EmailAddresses (EMailPk, UserId, IsPrimaryMail, Status, EmailAddress) VALUES
(0, '9e8fba01-d990-4f20-bc2d-f7327dff5ad0', 'Y', 'P', 'nafis_sadik@outlook.com');

-- --------------------------------------------------------

--
-- Table structure for table ContactNumbers
--

CREATE TABLE ContactNumbers (
  UserId varchar(50) NOT NULL,
  Number varchar(20) NOT NULL,
  IsBkash char(1) DEFAULT NULL,
  IsNagad char(1) DEFAULT NULL,
  IsRocket char(1) DEFAULT NULL,
  Status char(1) DEFAULT NULL
);

-- --------------------------------------------------------

--
-- Table structure for table Outlets
--

CREATE TABLE Outlets (
  OutletId decimal(18,0) NOT NULL,
  OutletName varchar(50) DEFAULT NULL,
  OutletAddresss text DEFAULT NULL,
  EComURL text DEFAULT NULL,
  UserId varchar(50) NOT NULL,
  RequestSite tinyint DEFAULT NULL,
  SiteUrl text DEFAULT NULL,
  Status char(1) NOT NULL
);

-- --------------------------------------------------------

--
-- Table structure for table SubscriptionLog
--

CREATE TABLE SubscriptionLog (
  Subscription char(10) DEFAULT NULL,
  UserId varchar(50) DEFAULT NULL,
  SubscriptionId decimal(18,0) DEFAULT NULL,
  SubscriptionDate datetime DEFAULT NULL,
  ExpirationDate datetime DEFAULT NULL
);

-- --------------------------------------------------------

--
-- Table structure for table Services
--

CREATE TABLE Services (
  SubscriptionId decimal(18,0) NOT NULL,
  SubscriptionName char(50) DEFAULT NULL,
  SubscriptionPrice decimal(18,0) DEFAULT NULL,
  DurationMonths decimal(18,0) DEFAULT NULL
);

-- --------------------------------------------------------

--
-- Table structure for table Users
--

CREATE TABLE Users (
  UserId varchar(50) NOT NULL,
  UserName varchar(50) NOT NULL,
  FirstName varchar(50) DEFAULT NULL,
  MiddleName varchar(50) DEFAULT NULL,
  LastName varchar(50) DEFAULT NULL,
  ProfilePicLoc text DEFAULT NULL,
  Status char(1) NOT NULL,
  Password text NOT NULL,
  AccountBalance decimal(18,0) DEFAULT NULL
);

--
-- Dumping data for table Users
--

INSERT INTO Users (UserId, UserName, FirstName, MiddleName, LastName, ProfilePicLoc, Status, Password, AccountBalance) VALUES
('9e8fba01-d990-4f20-bc2d-f7327dff5ad0', 'admin', 'Admin', 'Admin', 'Admin', NULL, 'A', '$2a$11$2oIwgNTYxbc5c83f4tq2HuEq3REJmtFVUM4CycVHsaydJ17UzghHS', '1000');

-- --------------------------------------------------------

--
-- Table structure for table UserActivityLog
--

CREATE TABLE UserActivityLog (
  ActivityLogIn int NOT NULL,
  UserId varchar(50) NOT NULL,
  ActivityTypeId decimal(18,0) DEFAULT NULL,
  Remarks varchar(50) DEFAULT NULL,
  ActivityDate datetime DEFAULT NULL,
  IPAddress char(15) DEFAULT NULL,
  Browser char(10) DEFAULT NULL,
  OS char(10) DEFAULT NULL,
  Misc text DEFAULT NULL
);

--
-- Indexes for dumped tables
--
--
-- Indexes for table Users
--
ALTER TABLE Users
  ADD PRIMARY KEY (UserId);
  
--
-- Indexes for table Outlets
--
ALTER TABLE Outlets
  ADD PRIMARY KEY (OutletId);
ALTER TABLE Outlets
  ADD CONSTRAINT IX_Outlets_UserId FOREIGN KEY (UserId)
  REFERENCES Users (UserId);
--
-- Indexes for table ActivityTypes
--
ALTER TABLE ActivityTypes
  ADD PRIMARY KEY (ActivityTypeId);

--
-- Indexes for table Addresses
--
ALTER TABLE Addresses
  ADD PRIMARY KEY (AddressId);
ALTER TABLE Addresses
  ADD CONSTRAINT IX_Addresses_UserId FOREIGN KEY (UserId)
  REFERENCES Users (UserId);

--
-- Indexes for table Categories
--
ALTER TABLE Categories
  ADD PRIMARY KEY (CategoryId);
ALTER TABLE Categories
  ADD CONSTRAINT IX_Categories_OutletId FOREIGN KEY (OutletId)
  REFERENCES Outlets (OutletId);

--
-- Indexes for table Crashlog
--
ALTER TABLE Crashlog
  ADD PRIMARY KEY (CrashLogId);
  
--
-- Indexes for table EmailAddresses
--
ALTER TABLE EmailAddresses
  ADD PRIMARY KEY (EMailPk);
ALTER TABLE EmailAddresses
  ADD CONSTRAINT IX_EmailId_UserId FOREIGN KEY (UserId)
  REFERENCES Users (UserId);

--
-- Indexes for table ContactNumbers
--
ALTER TABLE ContactNumbers
  ADD CONSTRAINT IX_Numbers_UserId FOREIGN KEY (UserId)
  REFERENCES Users (UserId);
  
--
-- Indexes for table Services
--
ALTER TABLE Services
  ADD PRIMARY KEY (SubscriptionId);
--
-- Indexes for table SubscriptionLog
--
ALTER TABLE SubscriptionLog
  ADD CONSTRAINT IX_SubscriptionLog_SubscriptionId FOREIGN KEY (SubscriptionId)
  REFERENCES Services (SubscriptionId);
ALTER TABLE SubscriptionLog
  ADD CONSTRAINT IX_SubscriptionLog_UserId FOREIGN KEY (UserId)
  REFERENCES Users (UserId);

--
-- Indexes for table UserActivityLog
--
ALTER TABLE UserActivityLog
  ADD PRIMARY KEY (ActivityLogIn);
ALTER TABLE UserActivityLog
  ADD CONSTRAINT IX_UserActivityLog_ActivityTypeId FOREIGN KEY (ActivityTypeId)
  REFERENCES ActivityTypes (ActivityTypeId);
ALTER TABLE UserActivityLog
  ADD CONSTRAINT IX_UserActivityLog_UserId FOREIGN KEY (UserId)
  REFERENCES Users (UserId);

--
-- Constraints for dumped tables
--

--
-- Constraints for table Addresses
--
ALTER TABLE Addresses
  ADD CONSTRAINT FK_Addresses_User FOREIGN KEY (UserId) REFERENCES Users (UserId);

--
-- Constraints for table Categories
--
ALTER TABLE Categories
  ADD CONSTRAINT FK_Categories_Outlets FOREIGN KEY (OutletId) REFERENCES Outlets (OutletId);

--
-- Constraints for table EmailAddresses
--
ALTER TABLE EmailAddresses
  ADD CONSTRAINT FK_EmailId_User FOREIGN KEY (UserId) REFERENCES Users (UserId);

--
-- Constraints for table ContactNumbers
--
ALTER TABLE ContactNumbers
  ADD CONSTRAINT FK_Numbers_User FOREIGN KEY (UserId) REFERENCES Users (UserId);

--
-- Constraints for table Outlets
--
ALTER TABLE Outlets
  ADD CONSTRAINT FK_Outlets_User FOREIGN KEY (UserId) REFERENCES Users (UserId);

--
-- Constraints for table SubscriptionLog
--
ALTER TABLE SubscriptionLog
  ADD CONSTRAINT FK_SubscriptionLog_Subscriptions1 FOREIGN KEY (SubscriptionId) REFERENCES Services (SubscriptionId);
ALTER TABLE SubscriptionLog
  ADD CONSTRAINT FK_SubscriptionLog_User1 FOREIGN KEY (UserId) REFERENCES Users (UserId);

--
-- Constraints for table UserActivityLog
--
ALTER TABLE UserActivityLog
  ADD CONSTRAINT FK_UserActivityLog_User FOREIGN KEY (UserId) REFERENCES Users (UserId);
ALTER TABLE UserActivityLog
  ADD CONSTRAINT FK_UserActivityLog_User1 FOREIGN KEY (ActivityTypeId) REFERENCES ActivityTypes (ActivityTypeId);

  
--
-- Table structure for table ProductUnitTypes
--

CREATE TABLE ProductUnitTypes (
  UnitTypeIds int NOT NULL,
  UnitTypeNames char(200) DEFAULT NULL,
  Status char(1) DEFAULT NULL
);

--
-- Dumping data for table ProductUnitTypes
--

INSERT INTO ProductUnitTypes (UnitTypeIds, UnitTypeNames, Status) VALUES
(1, 'Piece', 'A'),
(2, 'KG (Kilo Grams)', 'A'),
(3, 'L (Litre)', 'A'),
(4, 'M (Meter)', 'A'),
(5, 'Inches', 'A');
-- --------------------------------------------------------
--
-- Indexes for table ProductUnitTypes
--
ALTER TABLE ProductUnitTypes
  ADD PRIMARY KEY (UnitTypeIds);

-- --------------------------------------------------------
COMMIT;
