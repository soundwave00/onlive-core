CREATE DATABASE ONSTAGE;

CREATE TABLE ONSTAGE.JPORTS (
	PORT INT NOT NULL,
	RUNNING BIT NOT NULL,

	PRIMARY KEY (PORT)
);

CREATE TABLE ONSTAGE.USERS (
	USERNAME VARCHAR(16) NOT NULL,
	EMAIL VARCHAR(32) NOT NULL,
	NAME VARCHAR(32) NOT NULL,
	SURNAME VARCHAR(32) NOT NULL,
	PASSWORD VARCHAR(256) NOT NULL,
	SALT VARCHAR(16) NOT NULL,
	IS_ACTIVE BIT NOT NULL, 
	DATE_CREATE DATETIME NOT NULL,
	DATE_DELETE DATETIME,
	AVATAR VARCHAR(64),
	
    UNIQUE (EMAIL),
	PRIMARY KEY (USERNAME)
);

CREATE TABLE ONSTAGE.SESSIONS (
	ID INT AUTO_INCREMENT,
	USERNAME VARCHAR(16) NOT NULL,
	COD_TOKEN VARCHAR(16) NOT NULL,
	DATE_START DATETIME NOT NULL,
	DATE_EXP DATETIME NOT NULL,
	
	PRIMARY KEY (ID),
    CONSTRAINT FK_USERS_SESSIONS FOREIGN KEY (USERNAME)
    REFERENCES ONSTAGE.USERS(USERNAME)
);

CREATE TABLE ONSTAGE.MUSIC_ROLES (
	ID INT NOT NULL,
	INSTRUMENT VARCHAR(16) NOT NULL,

	PRIMARY KEY (ID)
);

CREATE TABLE ONSTAGE.GENRES (
	ID INT NOT NULL,
	GENRE VARCHAR(32) NOT NULL,

	PRIMARY KEY (ID)
);

CREATE TABLE ONSTAGE.GROUPS_ROLES (
	ID INT NOT NULL,
	ROLES VARCHAR(16) NOT NULL,

	PRIMARY KEY (ID)
);

CREATE TABLE ONSTAGE.GROUPS (
	ID INT AUTO_INCREMENT,
	NAME VARCHAR(64) NOT NULL,
	DESCRIPTION VARCHAR(128) NOT NULL,
	AVATAR VARCHAR(64),

	PRIMARY KEY (ID)
);

CREATE TABLE ONSTAGE.EVENTS (
	ID INT AUTO_INCREMENT,
	NAME VARCHAR(64) NOT NULL,
	DESCRIPTION VARCHAR(128) NOT NULL,
	ID_GROUPS INT NOT NULL,
	DATE_SET DATETIME NOT NULL,
	DATE_START DATETIME,
	DATE_STOP DATETIME,
	PID INT,
	PORT INT,
	RUNNING BIT NOT NULL,

	PRIMARY KEY (ID),
    CONSTRAINT FK_JPORTS_EVENTS FOREIGN KEY (PORT)
    REFERENCES ONSTAGE.JPORTS(PORT),
	CONSTRAINT FK_GROUPS_EVENTS FOREIGN KEY (ID_GROUPS)
	REFERENCES ONSTAGE.GROUPS(ID)
);

CREATE TABLE ONSTAGE.FAVORITES_GROUPS (
	USERNAME VARCHAR(16) NOT NULL,
	ID_GROUPS INT NOT NULL,

	PRIMARY KEY (USERNAME, ID_GROUPS),

	CONSTRAINT FK_USERS_FAVORITES_GROUPS FOREIGN KEY (USERNAME)  
	REFERENCES ONSTAGE.USERS (USERNAME),
    CONSTRAINT FK_GROUPS_FAVORITES_GROUPS FOREIGN KEY (ID_GROUPS) 
    REFERENCES ONSTAGE.GROUPS (ID)
);

CREATE TABLE ONSTAGE.GROUPS_MEMBERS (
	USERNAME VARCHAR(16) NOT NULL,
	ID_GROUPS INT NOT NULL,
	
	PRIMARY KEY (USERNAME, ID_GROUPS),
	CONSTRAINT FK_USERS_GROUPS_MEMBERS FOREIGN KEY (USERNAME)  
	REFERENCES ONSTAGE.USERS (USERNAME),
    CONSTRAINT FK_GROUPS_GROUPS_MEMBERS FOREIGN KEY (ID_GROUPS) 
    REFERENCES ONSTAGE.GROUPS (ID)
);

CREATE TABLE ONSTAGE.GROUPS_MEMBERS_GROUPS_ROLES (
	USERNAME VARCHAR(16) NOT NULL,
	ID_GROUPS_MEMBERS INT NOT NULL,
	ID_GROUPS_ROLES INT NOT NULL,
	
	PRIMARY KEY (USERNAME, ID_GROUPS_MEMBERS, ID_GROUPS_ROLES),
	CONSTRAINT FK_GROUPS_MEMBERS_GROUPS_MEMBERS_GROUPS_ROLES FOREIGN KEY (USERNAME, ID_GROUPS_MEMBERS)  
	REFERENCES ONSTAGE.GROUPS_MEMBERS (USERNAME, ID_GROUPS),
    CONSTRAINT FK_GROUPS_ROLES_GROUPS_MEMBERS_GROUPS_ROLES FOREIGN KEY (ID_GROUPS_ROLES) 
    REFERENCES ONSTAGE.GROUPS_ROLES (ID)
);

CREATE TABLE ONSTAGE.EVENTS_GENRES(
	ID_GENRES INT NOT NULL,
	ID_EVENTS INT NOT NULL,
	
	PRIMARY KEY (ID_GENRES, ID_EVENTS),
	CONSTRAINT FK_GENRES_EVENTS_GENRES FOREIGN KEY (ID_GENRES)  
	REFERENCES ONSTAGE.GENRES (ID),
    CONSTRAINT FK_EVENTS_EVENTS_GENRES FOREIGN KEY (ID_EVENTS) 
    REFERENCES ONSTAGE.EVENTS (ID)
);

CREATE TABLE ONSTAGE.USERS_GENRES (
	USERNAME VARCHAR(16) NOT NULL,
	ID_GENRES INT NOT NULL,
	
	PRIMARY KEY (USERNAME, ID_GENRES),
	CONSTRAINT FK_USERS_USERS_GENRES FOREIGN KEY (USERNAME)  
	REFERENCES ONSTAGE.USERS (USERNAME),
    CONSTRAINT FK_GENRES_USERS_GENRES FOREIGN KEY (ID_GENRES) 
    REFERENCES ONSTAGE.GENRES (ID)
);

CREATE TABLE ONSTAGE.GROUPS_GENRES (
	ID_GROUPS INT NOT NULL,
	ID_GENRES INT NOT NULL,
	
	PRIMARY KEY (ID_GROUPS, ID_GENRES),
	CONSTRAINT FK_GROUPS_GROUPS_GENRES FOREIGN KEY (ID_GROUPS)  
	REFERENCES ONSTAGE.GROUPS (ID),
    CONSTRAINT FK_GENRES_GROUPS_GENRES FOREIGN KEY (ID_GENRES) 
    REFERENCES ONSTAGE.GENRES (ID)
);

CREATE TABLE ONSTAGE.USERS_MUSIC_ROLES (
	USERNAME VARCHAR(16) NOT NULL,
	ID_MUSIC_ROLES INT NOT NULL,
	
	PRIMARY KEY (USERNAME, ID_MUSIC_ROLES),
	CONSTRAINT FK_USERS_USERS_MUSIC_ROLES FOREIGN KEY (USERNAME)  
	REFERENCES ONSTAGE.USERS (USERNAME),
    CONSTRAINT FK_MUSIC_ROLES_USERS_MUSIC_ROLES FOREIGN KEY (ID_MUSIC_ROLES) 
    REFERENCES ONSTAGE.MUSIC_ROLES (ID)
);
